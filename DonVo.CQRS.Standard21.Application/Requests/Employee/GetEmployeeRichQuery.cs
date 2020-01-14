using DonVo.CQRS.Standard21.Application.ViewModels.Employee;

using MediatR;

namespace DonVo.CQRS.Standard21.Application.Requests.Employee
{
    public class GetEmployeeRichQuery : IRequest<EmployeeRichViewModel>
    {
        public int? Id { get; set; }
    }
}