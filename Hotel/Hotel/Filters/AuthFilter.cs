using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Hotel.Models;
using System.Text.Json;

namespace Hotel.Filters
{
	public class AuthFilter : ActionFilterAttribute
	{
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			if (context.HttpContext.Session.GetString("loggedUser") == null)
			{
				context.Result = new RedirectResult("/Home");
			}
		}

	}

	public class RoleFilter : ActionFilterAttribute
	{
		public override void OnActionExecuted(ActionExecutedContext context)
		{
			string jsonData = context.HttpContext.Session.GetString("loggedUser");
			if(jsonData != null)
			{
				User user =  JsonSerializer.Deserialize<User>(jsonData);
				if(user.Role != Role.Admin)
				{
					context.Result = new RedirectResult("/Floor");
				}
			}
		}
	}


}
