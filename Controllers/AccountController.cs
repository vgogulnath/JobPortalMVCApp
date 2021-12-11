using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobPortalMVCApplication.Models;
using JobPortalMVCApplication.BusinessLogic.Service;
using JobPortalMVCApplication.BusinessLogic.Interface;
using JobPortalMVCApplication.Filter;

namespace JobPortalMVCApplication.Controllers
{
    public class AccountController : Controller
    {
        IUserLogic _userLogic;
        const string UserName = "UserName";
        const string UserId = "UserId";
        const string UserType = "UserType";

        /// <summary>
        /// Constructor with dependency injection
        /// </summary>
        /// <param name="userLogic"></param>
        public AccountController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [SessionFilter]
        public IActionResult Index()
        {
            return View();
        }

        [SessionFilter]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegisterModel registerModel)
        {
            ModelState.Remove("Id");
            if (!ModelState.IsValid)
            {
                return View();
            }

            if(registerModel != null)
            {
                if(_userLogic.AddUserRegistration(registerModel))
                {
                    return RedirectToAction("Register", new { result = "Success" });
                }
                else
                {
                    return RedirectToAction("Register", new { result = "Error" });
                }
            }
            else
            {
                return RedirectToAction("Register", new { result = "Error" });
            }
        }
    }
}
