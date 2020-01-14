using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DonVo.CQRS.NetCore31.WebApp.Infrastructure
{
	public class LoadModelState : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			base.OnActionExecuting(context);
			if (((Controller)context.Controller).TempData["ModelState"] != null)
			{
				((Controller)context.Controller).TempData.TryGetValue("ModelState", out var errors);
				foreach (var error in errors as Dictionary<string, string>)
				{
					((Controller)context.Controller).ModelState.AddModelError(error.Key, error.Value);
				}
			}
		}
	}
}