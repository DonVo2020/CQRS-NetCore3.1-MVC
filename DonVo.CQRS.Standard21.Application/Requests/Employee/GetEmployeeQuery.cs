using DonVo.CQRS.Standard21.Application.ViewModels.Employee;

using MediatR;

namespace DonVo.CQRS.Standard21.Application.Requests.Employee
{
    public class GetEmployeeQuery : IRequest<EmployeeViewModel>
    {
        public int Id { get; set; }
        public int? Year { get; set; }
        public bool MoreVacationRequests { get; set; }
    }
}