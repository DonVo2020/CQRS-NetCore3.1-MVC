using System;
using System.ComponentModel.DataAnnotations;

namespace DonVo.CQRS.Standard21.Domain.Model.Company
{
	public class ContractType : AggregateRoot
	{
		[MaxLength(16), Required]
		public string Name { get; protected set; }

		protected ContractType() { }

		public ContractType(string name)
		{
			Name = name;
			CreatedOn = DateTimeOffset.Now;
		}
	}
}