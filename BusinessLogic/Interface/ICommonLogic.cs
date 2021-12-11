namespace JobPortalMVCApplication.BusinessLogic.Interface
{
    public interface ICommonLogic
    {
        string GenerateUserId();
        bool AddDatatoJson(string type, dynamic list);
        dynamic GetJsonDataModel(string jsonType);
    }
}
