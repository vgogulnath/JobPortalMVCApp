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
    public class JobsController : Controller
    {
        IJobsLogic _jobsLogic;
        const string UserName = "UserName";
        const string UserId = "UserId";
        const string UserType = "UserType";
        public JobsController(IJobsLogic jobsLogic)
        {
            _jobsLogic = jobsLogic;
        }

        [SessionFilter]
        public IActionResult Index()
        {
            return View();
        }

        [SessionFilter]
        public IActionResult ViewJob(string id)
        {
            if (id == null || id == "")
            {
                return RedirectToAction("ViewJobs");
            }
            else
            {
                JobsModel jobsModel = _jobsLogic.GetJob(id, HttpContext.Session.GetString(UserId));
                if (jobsModel != null)
                {
                    return View(jobsModel);
                }
                else
                {
                    return RedirectToAction("ViewJobs");
                }
            }
        }

        [SessionFilter]
        public IActionResult PostJobs(string id)
        {
            if (id == null || id == "")
            {
                return View();
            }
            else
            {
                JobsModel jobsModel = _jobsLogic.GetJob(id, null);
                jobsModel.JobDescription = jobsModel.JobDescription.Replace("<br>", "\n");
                if (jobsModel != null)
                {
                    return View(jobsModel);
                }
                else
                {
                    return View();
                }
            }
        }

        [SessionFilter]
        public IActionResult DeleteJob(string id)
        {
            ViewBag.Username = HttpContext.Session.GetString(UserName);
            ViewBag.UserId = HttpContext.Session.GetString(UserId);
            ViewBag.UserType = HttpContext.Session.GetString(UserType);
            if (id == null || id == "")
            {
                return RedirectToAction("ViewJobs");
            }
            else
            {
                if (_jobsLogic.DeleteJob(id))
                {
                    return RedirectToAction("ViewJobs", new { result = "Success" });
                }
                else
                {
                    return RedirectToAction("ViewJobs", new { result = "Error" });
                }
            }
        }

        [SessionFilter]
        public IActionResult ViewJobs()
        {
            ViewBag.Username = HttpContext.Session.GetString(UserName);
            ViewBag.UserId = HttpContext.Session.GetString(UserId);
            ViewBag.UserType = HttpContext.Session.GetString(UserType);
            List<JobsModel> jobsModelList = new List<JobsModel>();
            if (ViewBag.UserType == "J")
            {
                jobsModelList = _jobsLogic.GetAllJobs();
            }
            else
            {
                jobsModelList = _jobsLogic.GetJobs(HttpContext.Session.GetString(UserId).ToString());
            }
            if (jobsModelList != null)
            {
                return View(jobsModelList);
            }
            else
            {
                return View();
            }
        }

        [SessionFilter]
        [HttpPost]
        public IActionResult PostJobs(JobsModel jobsModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            jobsModel.PostedBy = HttpContext.Session.GetString(UserId);
            if (jobsModel != null)
            {
                if (_jobsLogic.PostJob(jobsModel))
                {
                    return RedirectToAction("PostJobs", new { result = "Success" });
                }
                else
                {
                    return RedirectToAction("PostJobs", new { result = "Error" });
                }
            }
            else
            {
                return RedirectToAction("PostJobs", new { result = "Error" });
            }
        }

        [SessionFilter]
        public IActionResult ApplyJob(string id)
        {
            if (id != null)
            {
                JobApplyModel jobApplyModel = new JobApplyModel
                {
                    UserId = HttpContext.Session.GetString(UserId),
                    JobId = id,
                    AppliedOn = System.DateTime.Now
                };
                if (_jobsLogic.ApplyJob(jobApplyModel))
                {
                    return RedirectToAction("ViewJobs", new { result = "Success" });
                }
                else
                {
                    return RedirectToAction("ViewJobs", new { result = "Error" });
                }
            }
            else
            {
                return RedirectToAction("ViewJobs", new { result = "Error" });
            }
        }

        [SessionFilter]
        public IActionResult AppliedJobs()
        {
            ViewBag.Username = HttpContext.Session.GetString(UserName);
            ViewBag.UserId = HttpContext.Session.GetString(UserId);
            ViewBag.UserType = HttpContext.Session.GetString(UserType);
            List<JobsModel> jobsModel = _jobsLogic.GetAppliedJobs(HttpContext.Session.GetString(UserId));
            if(jobsModel != null)
            {
                return View(jobsModel);
            }
            else
            {
                return View();
            }
        }

        [SessionFilter]
        public IActionResult CheckJobSeekers(string id)
        {
            ViewBag.Username = HttpContext.Session.GetString(UserName);
            ViewBag.UserId = HttpContext.Session.GetString(UserId);
            ViewBag.UserType = HttpContext.Session.GetString(UserType);
            List<UserRegisterModel> userModel = _jobsLogic.GetAppliedJobseekers(id);
            if (userModel != null)
            {
                return View(userModel);
            }
            else
            {
                return View();
            }
        }
    }
}

