using JobPortalMVCApplication.Models;
namespace JobPortalMVCApplication.BusinessLogic.Interface
{
    public interface IUserLogic
    {
        bool AddUserRegistration(UserRegisterModel registerModel);
        UserRegisterModel LoginUser(LoginModel loginModel);
    }
}
