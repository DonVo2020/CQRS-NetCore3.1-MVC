using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.CQRS.Standard21.Domain.Model.Company
{
	public class VacationType : AggregateRoot
	{
		[MaxLength(16), Required]
		public string Name { get; protected set; }

		[Required]
		public int DefaultLeaveDays { get; protected set; }

		[Required]
		public bool IsPassing { get; protected set; }

		[ForeignKey("Pool")]
		public int? PoolId { get; protected set; }

		[ForeignKey("PoolId")]
		public VacationType Pool { get; protected set; }

		protected VacationType() { }

		public VacationType(string name, int defaultleavedays, bool ispassing, VacationType pool)
		{
			Name = name;
			DefaultLeaveDays = defaultleavedays;
			IsPassing = ispassing;
			PoolId = pool?.Id;
			Pool = pool;
			CreatedOn = DateTimeOffset.Now;
		}

		public void Update(string name, int defaultleavedays, bool ispassing, VacationType pool, byte[] version)
		{
			Name = name;
			DefaultLeaveDays = defaultleavedays;
			IsPassing = ispassing;
			PoolId = pool?.Id;
			Pool = pool;
			RowVersion = version;
			UpdatedOn = DateTimeOffset.Now;
		}

		public void UpdatePool(VacationType pool, byte[] version)
		{
			PoolId = pool?.Id;
			Pool = pool;
			RowVersion = version;
			UpdatedOn = DateTimeOffset.Now;
		}
	}
}