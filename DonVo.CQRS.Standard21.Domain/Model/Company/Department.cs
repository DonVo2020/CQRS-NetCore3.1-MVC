using System;
using System.ComponentModel.DataAnnotations;

namespace DonVo.CQRS.Standard21.Domain.Model.Company
{
	public class Department : AggregateRoot
	{
		[MaxLength(32), Required]
		public string Name { get; protected set; }

		[MaxLength(256), Required]
		public string Description { get; protected set; }

		[Range(-127, 127), Required]
		public int Level { get; protected set; }

		protected Department() { }

		public Department(string name, string description, int level)
		{
			Name = name;
			Description = description;
			Level = level;
			CreatedOn = DateTimeOffset.Now;
		}

		public void Update(string name, string description, int level, byte[] version)
		{
			Name = name;
			Description = description;
			Level = level;
			RowVersion = version;
			UpdatedOn = DateTimeOffset.Now;
		}
	}
}