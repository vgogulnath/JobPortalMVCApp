using JobPortalMVCApplication.Models;

namespace JobPortalMVCApplication.BusinessLogic.Interface
{
    public interface IJobsLogic
    {
        bool PostJob(JobsModel jobsModel);
        bool DeleteJob(string id);
        List<JobsModel> GetJobs(string userID);
        List<JobsModel> GetAllJobs();
        bool ApplyJob(JobApplyModel jobApplyModel);
        JobsModel GetJob(string jobID, string userId);
        List<JobsModel> GetAppliedJobs(string userId);
        List<UserRegisterModel> GetAppliedJobseekers(string jobId);
        List<JobsModel> GetSearchJobs(string query);
    }
}
