using DonVo.CQRS.Standard21.Application.Events.Employee;
using DonVo.CQRS.Standard21.Application.Requests.Employee;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;
using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.Employee
{
    public class DeleteContractHandler : IRequestHandler<DeleteContractCommand>
    {
        private readonly IMediator Mediator;
        private readonly IContractRepository ContractRepository;
        private readonly IEmployeeRepository EmployeeRepository;

        public DeleteContractHandler(IMediator mediator, IContractRepository contractrepository, IEmployeeRepository employeerepository)
        {
            Mediator = mediator;
            ContractRepository = contractrepository;
            EmployeeRepository = employeerepository;
        }

        public async Task<Unit> Handle(DeleteContractCommand request, CancellationToken cancellationToken)
        {
            var contract = await ContractRepository.Get(request.Id);
            if (contract == null) throw new ArgumentOutOfRangeException("Contract does not exist.");

            await ContractRepository.Delete(contract);

            await Mediator.Publish(new ContractDeletedEvent { Id = contract.Id });

            return await Unit.Task;
        }
    }
}