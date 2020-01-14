using System;
using System.ComponentModel.DataAnnotations;

namespace DonVo.CQRS.Standard21.Domain.Model.Company
{
	public class Position : AggregateRoot
	{
		[MaxLength(32), Required]
		public string Name { get; protected set; }

		[MaxLength(256), Required]
		public string Description { get; protected set; }

		[Range(-127, 127), Required]
		public int Grade { get; protected set; }

		protected Position() { }

		public Position(string name, string description, int grade)
		{
			Name = name;
			Description = description;
			Grade = grade;
			CreatedOn = DateTimeOffset.Now;
		}

		public void Update(string name, string description, int grade, byte[] version)
		{
			Name = name;
			Description = description;
			Grade = grade;
			RowVersion = version;
			UpdatedOn = DateTimeOffset.Now;
		}
	}
}