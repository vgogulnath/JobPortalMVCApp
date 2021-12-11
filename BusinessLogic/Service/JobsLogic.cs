using JobPortalMVCApplication.BusinessLogic.Interface;
using JobPortalMVCApplication.Models;
using Newtonsoft.Json;

namespace JobPortalMVCApplication.BusinessLogic.Service
{
    public class JobsLogic : IJobsLogic
    {
        ICommonLogic _commonLogic;
        const string _jobsList = "JobsList";
        const string _appliedList = "AppliedJobsList";
        const string _userList = "UserList";
        /// <summary>
        /// Construction with dependency injection
        /// </summary>
        /// <param name="commonLogic"></param>
        public JobsLogic(ICommonLogic commonLogic)
        {
            _commonLogic = commonLogic;
        }

        /// <summary>
        /// This method is used to add/edit the jobs in the json
        /// </summary>
        /// <param name="jobsModel"></param>
        /// <returns></returns>
        public bool PostJob(JobsModel jobsModel)
        {
            try
            {
               
                List<JobsModel> jobsList = new List<JobsModel>();
                jobsList = _commonLogic.GetJsonDataModel(_jobsList);
                if (jobsList.Any(a => a.Id == jobsModel.Id))
                {
                    JobsModel jobsData = jobsList.FirstOrDefault(a => a.Id == jobsModel.Id);
                    if (jobsData != null)
                    {
                        jobsData.Company = jobsModel.Company;
                        jobsData.JobDescription = jobsModel.JobDescription?.Replace("\n", "<br>");
                        jobsData.JobLocation = jobsModel.JobLocation;
                        jobsData.JobNature = jobsModel.JobNature;
                        jobsData.JobTitle = jobsModel.JobTitle;
                        jobsData.Vacancy = jobsModel.Vacancy;
                        jobsData.Salary = jobsModel.Salary;
                        jobsData.PostedBy = jobsModel.PostedBy;
                        jobsData.UpdateOn = System.DateTime.Now;
                        jobsData.IsActive = true;
                    }
                }
                else
                {
                    JobsModel newuser = new JobsModel
                    {
                        Id = _commonLogic.GenerateUserId(),
                        Company = jobsModel.Company,
                        JobDescription = jobsModel.JobDescription?.Replace("\n", "<br>"),
                        JobLocation = jobsModel.JobLocation,
                        JobNature = jobsModel.JobNature,
                        JobTitle = jobsModel.JobTitle,
                        CreatedOn = System.DateTime.Now,
                        Vacancy = jobsModel.Vacancy,
                        Salary = jobsModel.Salary,
                        PostedBy = jobsModel.PostedBy,
                        UpdateOn = System.DateTime.Now,
                        IsActive = true
                    };
                    jobsList.Add(newuser);
                }

                _commonLogic.AddDatatoJson(_jobsList, jobsList);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// This method is used to delete the jobs from Json(Soft delete)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteJob(string id)
        {
            try
            {
                List<JobsModel> jobsList = _commonLogic.GetJsonDataModel(_jobsList);

                if (jobsList.Any(a => a.Id == id))
                {
                    JobsModel jobsData = jobsList.FirstOrDefault(a => a.Id == id);
                    if (jobsData != null)
                    {
                        jobsData.UpdateOn = System.DateTime.Now;
                        jobsData.IsActive = false;
                    }
                }
                _commonLogic.AddDatatoJson(_jobsList, jobsList);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// This method is used to get the jobs list based on the userid
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<JobsModel> GetJobs(string userID)
        {
            List<JobsModel> jobsList = _commonLogic.GetJsonDataModel(_jobsList);
            return jobsList?.Where(a => a.PostedBy == userID && a.IsActive)?.OrderByDescending(a => a.CreatedOn)?.ToList();
        }

        /// <summary>
        /// This method will return all the jobs list
        /// </summary>
        /// <returns></returns>
        public List<JobsModel> GetAllJobs()
        {
            List<JobsModel> jobsList = _commonLogic.GetJsonDataModel(_jobsList);
            return jobsList?.Where(a => a.IsActive).OrderByDescending(a => a.CreatedOn)?.ToList();
        }

        /// <summary>
        /// This method will return the job based on the job id
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        public JobsModel GetJob(string jobID, string userId)
        {
            List<JobsModel> jobsList = _commonLogic.GetJsonDataModel(_jobsList);

            if (userId != null)
            {
                return IsUserAppliedJob(jobsList?.Where(a => a.Id == jobID && a.IsActive)?.ToList()[0], userId);
            }
            {
                return jobsList?.Where(a => a.Id == jobID && a.IsActive)?.ToList()[0];
            }
        }

        /// <summary>
        /// This method is used to get whether user already applied for this job
        /// </summary>
        /// <param name="jobs"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private JobsModel IsUserAppliedJob(JobsModel jobs, string userId)
        {
            List<JobApplyModel> appliedList = _commonLogic.GetJsonDataModel(_appliedList);
            jobs.IsApplied = appliedList.Any(a => a.UserId == userId && a.JobId == jobs.Id);
            return jobs;
        }

        /// <summary>
        /// This method is used to apply the jobs
        /// </summary>
        /// <param name="jobApplyModel"></param>
        /// <returns></returns>
        public bool ApplyJob(JobApplyModel jobApplyModel)
        {
            try
            {
                List<JobApplyModel> appliedList = _commonLogic.GetJsonDataModel(_appliedList);
                JobApplyModel newapplication = new JobApplyModel
                {
                    UserId = jobApplyModel.UserId,
                    JobId = jobApplyModel.JobId,
                    AppliedOn = System.DateTime.Now
                };
                appliedList.Add(newapplication);
                _commonLogic.AddDatatoJson(_appliedList, appliedList);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// This method will return the applied jobs based on the userid
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<JobsModel> GetAppliedJobs(string userId)
        {
            try
            {
                List<JobApplyModel> appliedList = _commonLogic.GetJsonDataModel(_appliedList);
                List<JobsModel> jobsList = _commonLogic.GetJsonDataModel(_jobsList);
                var appliedIds = appliedList.Where(a => a.UserId == userId).ToList();
                List<JobsModel> result = new List<JobsModel>();
                foreach (var apply in appliedIds)
                {
                    result.Add(jobsList.FirstOrDefault(a => a.Id == apply.JobId && a.IsActive));
                }
                return result.OrderByDescending(a => a.CreatedOn).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// This method is return the jobs list based on the search
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<JobsModel> GetSearchJobs(string query)
        {
            List<JobsModel> jobsList = _commonLogic.GetJsonDataModel(_jobsList);
            return jobsList?.Where(a => a.JobTitle.ToLower().Contains(query.ToLower()) && a.IsActive)?.OrderByDescending(a => a.CreatedOn).ToList();
        }

        /// <summary>
        /// This method will return the user model based on the applied job id
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public List<UserRegisterModel> GetAppliedJobseekers(string jobId)
        {
            try
            {
                List<JobApplyModel> appliedList = _commonLogic.GetJsonDataModel(_appliedList);
                List<UserRegisterModel> userList = _commonLogic.GetJsonDataModel(_userList);
                var appliedIds = appliedList.Where(a => a.JobId == jobId).ToList();
                List<UserRegisterModel> result = new List<UserRegisterModel>();
                foreach (var apply in appliedIds)
                {
                    var user = userList.FirstOrDefault(a => a.Id == apply.UserId);
                    user.UpdateOn = apply.AppliedOn;
                    result.Add(user);
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
