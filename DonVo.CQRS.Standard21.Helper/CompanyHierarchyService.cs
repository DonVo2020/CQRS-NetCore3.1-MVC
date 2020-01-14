using DonVo.CQRS.Standard21.Domain.Model.Company;
using System.Collections.Generic;
using System.Linq;

namespace DonVo.CQRS.Standard21.Helper
{
    public class CompanyHierarchyHelper
    {
        public IEnumerable<Employee> GetSubordinates(int employeeid, IEnumerable<Employee> employees)
        {
            return employees.Where(e => e.ManagerId == employeeid);
        }

        public IEnumerable<Vacation> GetSubordinateRequests(int employeeid, IEnumerable<Employee> employees, IEnumerable<Holiday> holidays)
        {
            var requests = employees.Where(item => item.ManagerId == employeeid).SelectMany(item => item.GetVacationRequests()).ToList();
            requests.ForEach(item => item.EmployeeFullName = employees.Single(e => e.Id == item.EmployeeId).FullName);
            return requests;
        }

        public IEnumerable<Vacation> GetSubSubordinateRequests(int employeeid, IEnumerable<Employee> employees, IEnumerable<Holiday> holidays)
        {
            var requests = new List<Vacation>();
            GetRequests(employeeid);
            requests.ForEach(item => item.EmployeeFullName = employees.Single(e => e.Id == item.EmployeeId).FullName);
            return requests;

            void GetRequests(int id)
            {
                var subordinetes = employees.Where(e => e.ManagerId.GetValueOrDefault(0) == id).SelectMany(item => item.GetVacationRequests()).ToList();
                requests.AddRange(subordinetes);
                foreach (var subordinate in subordinetes) GetRequests(subordinate.EmployeeId);
            }
        }

        public bool IsHigherInHierarchy(int userid, int employeeid, IEnumerable<Employee> employees)
        {
            return GetManagerId(employeeid);

            bool GetManagerId(int id)
            {
                var person = employees.Single(e => e.Id == id);
                if (!person.ManagerId.HasValue) return false;
                if (person.ManagerId == userid) return true;
                return GetManagerId(person.ManagerId.Value);
            }
        }
    }
}