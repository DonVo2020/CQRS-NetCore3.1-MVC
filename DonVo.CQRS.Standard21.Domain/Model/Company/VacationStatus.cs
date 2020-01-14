using System;
using System.ComponentModel.DataAnnotations;

namespace DonVo.CQRS.Standard21.Domain.Model.Company
{
	public class VacationStatus : AggregateRoot
	{
		[MaxLength(16), Required]
		public string Name { get; protected set; }

		protected VacationStatus() { }

		public VacationStatus(string name)
		{
			Name = name;
			CreatedOn = DateTimeOffset.Now;
		}
	}
}