using DonVo.CQRS.Standard21.Application.Requests.Position;
using DonVo.CQRS.Standard21.Application.ViewModels.Position;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;
using MediatR;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.Position
{
    public class GetPositionsHandler : IRequestHandler<GetPositionsQuery, IEnumerable<PositionViewModel>>
    {
        private readonly IMediator Mediator;
        private readonly IPositionRepository PositionRepository;

        public GetPositionsHandler(IMediator mediator, IPositionRepository positionrepository)
        {
            Mediator = mediator;
            PositionRepository = positionrepository;
        }

        public async Task<IEnumerable<PositionViewModel>> Handle(GetPositionsQuery request, CancellationToken cancellationToken)
        {
            var positions = await PositionRepository.Select();
            var viewmodel = new List<PositionViewModel>();
            foreach (var item in positions)
            {
                var vm = new PositionViewModel();
                vm.LoadFromDomain(item);
                vm.IsSelected = item.Id == request.SelectedId;
                viewmodel.Add(vm);
            }
            return viewmodel.OrderByDescending(item => item.Grade).ThenBy(item => item.Name).ToList();
        }
    }
}