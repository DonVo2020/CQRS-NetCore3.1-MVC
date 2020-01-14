using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DonVo.CQRS.NetCore31.WebApp.Infrastructure
{
	public class SaveModelState : ActionFilterAttribute
	{
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			base.OnActionExecuted(context);
			Dictionary<string, string> errors = new Dictionary<string, string>();
			foreach (var error in ((Controller)context.Controller).ModelState)
			{
				if (error.Value.Errors.Count > 0)
					errors.Add(error.Key, error.Value.Errors[0].ErrorMessage);
			}
			((Controller)context.Controller).TempData.Add("ModelState", errors);
		}
	}
}