using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using JobPortalMVCApplication.Models;
using JobPortalMVCApplication.BusinessLogic.Service;
using JobPortalMVCApplication.BusinessLogic.Interface;
using JobPortalMVCApplication.Filter;
namespace JobPortalMVCApplication.Controllers
{
    public class HomeController : Controller
    {
        IUserLogic _userLogic;
        IJobsLogic _jobsLogic;
        const string UserName = "UserName";
        const string UserId = "UserId";
        const string UserType = "UserType";
        public HomeController(IUserLogic userLogic, IJobsLogic jobsLogic)
        {
            _userLogic = userLogic;
            _jobsLogic = jobsLogic;
        }

        [SessionFilter]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [SessionFilter]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("~/");
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (loginModel != null)
            {
                UserRegisterModel userModel = _userLogic.LoginUser(loginModel);
                if (userModel != null)
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    HttpContext.Session.SetString(UserName, userModel.Name);
                    HttpContext.Session.SetString(UserId, userModel.Id);
                    HttpContext.Session.SetString(UserType, userModel.UserType);
#pragma warning restore CS8604 // Possible null reference argument.
                    //return View("Home",new { result = "Success" });
                    return Redirect("~/");
                    //return RedirectToAction("/", new { result = "Success" });
                }
                else
                {
                    return RedirectToAction("Login", new { result = "Error" });
                }
            }
            else
            {
                return RedirectToAction("Login", new { result = "Error" });
            }
        }

        [SessionFilter]
        public IActionResult SearchJobs(string query)
        {
            if (query != null)
            {
                return View(_jobsLogic.GetSearchJobs(query));
            }
            else
            {
                return Redirect("~/");
            }
        }
    }
}
