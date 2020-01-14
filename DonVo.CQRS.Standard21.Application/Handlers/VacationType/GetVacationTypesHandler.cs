using DonVo.CQRS.Standard21.Application.Requests.VacationType;
using DonVo.CQRS.Standard21.Application.ViewModels.VacationType;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;
using MediatR;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.VacationType
{
    public class GetVacationTypesHandler : IRequestHandler<GetVacationTypesQuery, IEnumerable<VacationTypeViewModel>>
    {
        private readonly IMediator Mediator;
        private readonly IVacationTypeRepository VacationTypeRepository;

        public GetVacationTypesHandler(IMediator mediator, IVacationTypeRepository vacationtyperepository)
        {
            Mediator = mediator;
            VacationTypeRepository = vacationtyperepository;
        }

        public async Task<IEnumerable<VacationTypeViewModel>> Handle(GetVacationTypesQuery request, CancellationToken cancellationToken)
        {
            var vacationtypes = await VacationTypeRepository.Select();
            var viewmodel = new List<VacationTypeViewModel>();
            foreach (var item in vacationtypes)
            {
                var vm = new VacationTypeViewModel();
                vm.LoadFromDomain(item);
                vm.IsSelected = item.Id == request.SelectedId;
                viewmodel.Add(vm);
            }
            foreach (var item in viewmodel)
            {
                item.VacationTypes = viewmodel.Where(i => i.Id != item.Id && !i.PoolId.HasValue).ToList();
            }
            return viewmodel.OrderBy(item => item.Id).ToList();
        }
    }
}