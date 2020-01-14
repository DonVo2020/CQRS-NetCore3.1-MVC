using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonVo.CQRS.Standard21.Domain.Model
{
    public abstract class Entity
    {
        // Install NuGet:  Microsoft.AspNetCore.Identity
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; protected set; }

        public DateTimeOffset? CreatedOn { get; protected set; }

        public string CreatedBy { get; protected set; }

        public DateTimeOffset? UpdatedOn { get; protected set; }

        public string UpdatedBy { get; protected set; }

        [Timestamp]
        public byte[] RowVersion { get; protected set; }

        public override bool Equals(object obj)
        {
            var other = obj as Entity;
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != obj.GetType()) return false;
            if (Id == 0 || other.Id == 0) return false;
            return Id == other.Id;
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}