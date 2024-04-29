using ShowWork.DAL_MSSQL.Models;
using ShowWork.ViewModels;

namespace ShowWork.ViewMapper
{
    public static class ProfileMapper
    {
        public static UserModel MapProfileVMToUserModel(ProfileViewModel model)
        {
            return new UserModel()
            {
                Email = model.Email!,
                Login = model.Login!,
                FirstName = model.FirstName!,
                SecondName = model.LastName!,
                Description = model.Description!,
                Specialization = model.Specialization!,
                ProfileImage = model.ImagePath!
            };
        }

        public static ProfileViewModel MapUserModelToProfileViewModel(UserModel model)
        {
            return new ProfileViewModel()
            {
                UserId = model.UserId!,
                Email = model.Email!,
                Login = model.Login!,
                FirstName = model.FirstName!,
                LastName = model.SecondName!,
                Description = model.Description!,
                Specialization = model.Specialization!,
                ImagePath = model.ProfileImage!
            };
        }
    }
}
