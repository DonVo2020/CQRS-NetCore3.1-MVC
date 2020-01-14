using DonVo.CQRS.Standard21.Application.ViewModels.Employee;

using MediatR;

using System.Collections.Generic;

namespace DonVo.CQRS.Standard21.Application.Requests.Employee
{
    public class GetEmployeesQuery : IRequest<IEnumerable<EmployeeLiteViewModel>>
    {

    }
}