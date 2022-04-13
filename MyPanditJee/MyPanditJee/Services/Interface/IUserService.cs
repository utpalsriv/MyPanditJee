using System.Collections.Generic;
using MyPanditJee.Models;

namespace MyPanditJee.Service.Interface
{
    public interface IUserService
    {
        public UserRegistrationModel RegisterUser(UserRegistrationModel userRegistrationModel, LoginModel loginModel);

        public IList<UserRegistrationModel> GetAllUser();

        public UserRegistrationModel GetUser(string email);

        public UserRegistrationModel SearchUser(LoginModel loginModel);

        public void Update(UserRegistrationModel userRegistrationModel, LoginModel loginModel);

        public void Delete(string id);

    }
}
