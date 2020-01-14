using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.CQRS.Standard21.Domain.Model.Company
{
	public class Contract : Entity
	{
		[ForeignKey("EmployeeId"), Required]
		public int EmployeeId { get; protected set; }

		[Required]
		public DateTimeOffset StartDate { get; protected set; }

		public DateTimeOffset? EndDate { get; protected set; }

		[Range(0, 9999999), Required, Column(TypeName = "decimal(18,2)")]
		public decimal Remuneration { get; protected set; }

		[ForeignKey("ContractType"), Required]
		public int ContractTypeId { get; protected set; }

		[ForeignKey("ContractTypeId"), Required]
		public ContractType ContractType { get; protected set; }

		[NotMapped]
		public ContractForm Type => (ContractForm)ContractTypeId;

		protected Contract() { }

		public Contract(int employeeid, DateTimeOffset startdate, DateTimeOffset? enddate, ContractForm contracttype, decimal remuneration)
		{
			EmployeeId = employeeid;
			StartDate = startdate;
			EndDate = enddate;
			ContractTypeId = (int)contracttype;
			Remuneration = remuneration;
			CreatedOn = DateTimeOffset.Now;
		}
	}
}