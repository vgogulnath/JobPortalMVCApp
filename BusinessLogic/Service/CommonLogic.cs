using Newtonsoft.Json;
using JobPortalMVCApplication.BusinessLogic.Interface;
using JobPortalMVCApplication.Models;

namespace JobPortalMVCApplication.BusinessLogic.Service
{
    public class CommonLogic : ICommonLogic
    {
        private static Random random = new Random();
        const string jobsListJson = "\\DBJson\\jobslist.json";
        const string appliedListJson = "\\DBJson\\appliedjobslist.json";
        const string userListJson = "\\DBJson\\userlist.json";
        const string _jobsList = "JobsList";
        const string _appliedList = "AppliedJobsList";
        /// <summary>
        /// This method is used to generate the random unique id
        /// </summary>
        /// <returns></returns>
        public string GenerateUserId()
        {
            const string chars = "abcdefghijklmnopqrstuvwzyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// This method is used to add the data to Json(DB)
        /// </summary>
        /// <param name="path"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool AddDatatoJson(string type, dynamic list)
        {
            try
            {
                string path = "";
                switch(type){
                    case _jobsList:
                        path = Directory.GetCurrentDirectory() + jobsListJson;
                        break;
                    case _appliedList:
                        path = Directory.GetCurrentDirectory() + appliedListJson;
                        break;
                    default:
                        path = Directory.GetCurrentDirectory() + userListJson;
                        break;
                }
                string jobsJson = JsonConvert.SerializeObject(list, Newtonsoft.Json.Formatting.Indented);
                System.IO.File.WriteAllText(path, jobsJson);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// This method will return the dynamic object model based on the json type
        /// </summary>
        /// <param name="jsonType"></param>
        /// <returns></returns>
        public dynamic GetJsonDataModel(string jsonType)
        {
            string jsonString = GetJsonData(jsonType);
            dynamic? result = null;
            switch (jsonType)
            {
                case _jobsList:
                    List<JobsModel> jobsModel = new List<JobsModel>();
                    if (!String.IsNullOrEmpty(jsonString))
                    {
                        JsonConvert.DeserializeObject<List<JobsModel>>(jsonString).ForEach(job =>
                        {
                            jobsModel.Add(job);
                        });
                    }
                    result = jobsModel;
                    break;
                case _appliedList:
                    List<JobApplyModel> jobApplyModels = new List<JobApplyModel>();
                    if (!String.IsNullOrEmpty(jsonString))
                    {
                        JsonConvert.DeserializeObject<List<JobApplyModel>>(jsonString).ForEach(apply =>
                        {
                            jobApplyModels.Add(apply);
                        });
                    }
                    result = jobApplyModels;
                    break;
                default:
                    List<UserRegisterModel> userRegisterModels = new List<UserRegisterModel>();
                    if (!String.IsNullOrEmpty(jsonString))
                    {
                        JsonConvert.DeserializeObject<List<UserRegisterModel>>(jsonString).ForEach(user =>
                        {
                            userRegisterModels.Add(user);
                        });
                    }
                    result = userRegisterModels;
                    break;
            }
            return result;
        }

        /// <summary>
        /// This method will return json string
        /// </summary>
        /// <param name="jsonType"></param>
        /// <returns></returns>
        public string GetJsonData(string jsonType)
        {
            string jsonstring = "";
            string path = "";
            switch (jsonType)
            {
                case _jobsList:
                    path = Directory.GetCurrentDirectory() + jobsListJson;
                    break;
                case _appliedList:
                    path = Directory.GetCurrentDirectory() + appliedListJson;
                    break;
                default:
                    path = Directory.GetCurrentDirectory() + userListJson;
                    break;
            }
            jsonstring = System.IO.File.ReadAllText(path);
            return jsonstring;
        }
    }
}
