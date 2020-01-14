using DonVo.CQRS.Standard21.Application.ViewModels.Department;
using MediatR;

using System.Collections.Generic;

namespace DonVo.CQRS.Standard21.Application.Requests.Department
{
    public class GetDepartmentsQuery : IRequest<IEnumerable<DepartmentViewModel>>
	{
		public int SelectedId { get; set; }
	}
}