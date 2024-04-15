using ShowWork.DAL_MSSQL.Models;
using ShowWork.ViewModels;

namespace ShowWork.ViewMapper
{
    public class AuthMapper
    {
        public static UserModel MapRegisterVMToUserModel(RegisterViewModel model) 
        {
            return new UserModel()
            {
                Email = model.Email,
                Password = model.Password,
                FirstName = model.FirstName,
                SecondName = model.SecondName
            };
        }
    }
}
