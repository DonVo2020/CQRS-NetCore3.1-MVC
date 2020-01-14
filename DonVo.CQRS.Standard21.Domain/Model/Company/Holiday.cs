using System;
using System.ComponentModel.DataAnnotations;

namespace DonVo.CQRS.Standard21.Domain.Model.Company
{
	public class Holiday : AggregateRoot
	{
		[Required]
		public DateTimeOffset Date { get; protected set; }

		protected Holiday() { }

		public Holiday(DateTimeOffset date)
		{
			Date = date;
			CreatedOn = DateTimeOffset.Now;
		}
	}
}