namespace DonVo.CQRS.Standard21.Domain.Model
{
	public interface IHandler<T> where T : IDomainEvent
	{
		void Handle(T domainEvent);
	}
}