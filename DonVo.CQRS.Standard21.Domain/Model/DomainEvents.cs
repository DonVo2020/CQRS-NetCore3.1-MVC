using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DonVo.CQRS.Standard21.Domain.Model
{
	public class DomainEvents
	{
		private static List<Type> _handlers;

		public static void Init()
		{
			_handlers = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IHandler<>))).ToList();
		}

		public static void Dispatch(IDomainEvent domainEvent)
		{
			foreach (Type handlerType in _handlers)
			{
				bool canHandleEvent = handlerType.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IHandler<>) && x.GenericTypeArguments[0] == domainEvent.GetType());

				if (canHandleEvent)
				{
                    // Install NuGet:  Microsoft.CSharp
                    dynamic handler = Activator.CreateInstance(handlerType);
					handler.Handle((dynamic)domainEvent);
				}
			}
		}
	}
}