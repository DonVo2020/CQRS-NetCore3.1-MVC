using DonVo.CQRS.Standard21.Application.Events.Employee;
using DonVo.CQRS.Standard21.Application.Requests.Employee;
using DonVo.CQRS.Standard21.Domain.Model.Company;
using DonVo.CQRS.Standard21.Domain.Repository.Interfaces;

using MediatR;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.Standard21.Application.Handlers.Employee
{
    public class InsertContractHandler : IRequestHandler<InsertContractCommand>
    {
        private readonly IMediator Mediator;
        private readonly IContractRepository ContractRepository;
        private readonly IEmployeeRepository EmployeeRepository;

        public InsertContractHandler(IMediator mediator, IContractRepository contractrepository, IEmployeeRepository employeerepository)
        {
            Mediator = mediator;
            ContractRepository = contractrepository;
            EmployeeRepository = employeerepository;
        }

        public async Task<Unit> Handle(InsertContractCommand request, CancellationToken cancellationToken)
        {
            var employee = await EmployeeRepository.Get(request.EmployeeId);
            if (employee == null) throw new ArgumentOutOfRangeException("Employee does not exist.");

            var contract = employee.InsertContract(request.Start, request.End, request.Remuneration, (ContractForm)Enum.Parse(typeof(ContractForm), request.ContractType));
            if (contract == null) throw new ArgumentOutOfRangeException("Contract does not exist.");

            await ContractRepository.Insert(contract);

            await Mediator.Publish(new ContractInsertedEvent { Id = contract.Id });

            return await Unit.Task;
        }
    }
}