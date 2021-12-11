using JobPortalMVCApplication.BusinessLogic.Interface;
using JobPortalMVCApplication.Models;
using Newtonsoft.Json;

namespace JobPortalMVCApplication.BusinessLogic.Service
{
    public class UserLogic : IUserLogic
    {
        ICommonLogic _commonLogic;
        const string _userList = "UserList";

        /// <summary>
        /// Constructor with dependency injection
        /// </summary>
        /// <param name="commonLogic"></param>
        public UserLogic(ICommonLogic commonLogic)
        {
            _commonLogic = commonLogic;
        }

        /// <summary>
        /// This method will add the data to json while user registering
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        public bool AddUserRegistration(UserRegisterModel registerModel)
        {
            List<UserRegisterModel> userList = _commonLogic.GetJsonDataModel(_userList);
            UserRegisterModel newuser = new UserRegisterModel
            {
                Id = _commonLogic.GenerateUserId(),
                Name = registerModel.Name,
                Email = registerModel.Email,
                CurrentCompany = registerModel.CurrentCompany,
                CurrentCTC = registerModel.CurrentCTC,
                Experience = registerModel.Experience,
                Password = registerModel.Password,
                Phone = registerModel.Phone,
                SkillSet = registerModel.SkillSet,
                UpdateOn = System.DateTime.Now,
                CreatedOn = System.DateTime.Now,
                UserType = registerModel.UserType
            };
            userList.Add(newuser);
            _commonLogic.AddDatatoJson(_userList, userList);
            return true;
        }

        /// <summary>
        /// This method will return the user data based on the login model
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        public UserRegisterModel LoginUser(LoginModel loginModel)
        {
            List<UserRegisterModel> userList = _commonLogic.GetJsonDataModel(_userList);
            return userList?.First(a => a.Email == loginModel.Email && a.Password == loginModel.Password);
        }
    }
}
