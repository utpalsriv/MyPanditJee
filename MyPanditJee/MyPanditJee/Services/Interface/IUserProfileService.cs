using MyPanditJee.Models;
using System.Collections.Generic;

namespace MyPanditJee.Service.Interface
{
    interface IUserProfileService
    {
        public UserProfileModel CreateUserProfile(UserProfileModel userProfileModel);
        public UserProfileModel GetUser(string email);


    }
}
