using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace JobPortalMVCApplication.Filter
{
    public class SessionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Controller? controller = context.Controller as Controller;
            controller.ViewBag.Username = context.HttpContext.Session.GetString("UserName");
            controller.ViewBag.UserId = context.HttpContext.Session.GetString("UserId");
            controller.ViewBag.UserType = context.HttpContext.Session.GetString("UserType");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Controller? controller = context.Controller as Controller;
            controller.ViewBag.Username = context.HttpContext.Session.GetString("UserName");
            controller.ViewBag.UserId = context.HttpContext.Session.GetString("UserId");
            controller.ViewBag.UserType = context.HttpContext.Session.GetString("UserType");
        }
    }
}
