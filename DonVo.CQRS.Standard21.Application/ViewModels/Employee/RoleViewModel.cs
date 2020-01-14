using DonVo.CQRS.Standard21.Domain.Model.Identity;

using System.Collections.Generic;
using System.Linq;

namespace DonVo.CQRS.Standard21.Application.ViewModels.Employee
{
    public class RoleViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Granted { get; set; }

        public void LoadFromDomain(ApplicationRole entity, IEnumerable<string> roles)
        {
            Id = entity.Id;
            Name = entity.Name;
            Granted = roles.Any(r => r == Name);
        }
    }
}