using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyPanditJee.Common;
using MyPanditJee.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPanditJee.Controllers
{
    public class ProfileController : Controller
    {
        public readonly UserProfileService _userProfile;
        public readonly LoginService _loginService;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(UserProfileService userProfile, LoginService loginService, ILogger<ProfileController> logger)
        {
            _userProfile = userProfile;
            _loginService = loginService;
            _logger = logger;
        }

        #region User Profile

        [HttpGet]
        public ActionResult UserProfile(string encryptEmail)
        {
            _logger.LogInformation("The UserProfile GET method has been accessed");
            var email = CommonCode.base64Decode(encryptEmail);
            HttpContext.Session.SetString("email", email.ToString());

            var user = _userProfile.GetUser(email);
            var userLogin = _loginService.FindMember(email);
            HttpContext.Session.SetString("usertype", userLogin.UserType.ToString());

            return View(user);
        }
        #endregion

    }
}
          