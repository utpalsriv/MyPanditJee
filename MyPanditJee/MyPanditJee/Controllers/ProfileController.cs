﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyPanditJee.Common;
using MyPanditJee.Service;
using MyPanditJee.Services;
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
        public readonly PanditJeeProfileServices _panditProfile;

        private readonly ILogger<ProfileController> _logger;

        public ProfileController(UserProfileService userProfile, LoginService loginService, ILogger<ProfileController> logger, PanditJeeProfileServices panditProfile)
        {
            _userProfile = userProfile;
            _loginService = loginService;
            _logger = logger;
            _panditProfile = panditProfile;
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

        [HttpGet]
        public ActionResult PanditProfile(string encryptEmail)
        {
            _logger.LogInformation("The UserProfile GET method has been accessed");
            var email = CommonCode.base64Decode(encryptEmail);
            HttpContext.Session.SetString("email", email.ToString());

            var user = _panditProfile.GetPandit(email);
            var userLogin = _loginService.FindMember(email);
            HttpContext.Session.SetString("usertype", userLogin.UserType.ToString());

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PanditProfile()
        {
            _logger.LogInformation("The EmployerProfile POST method has been accessed");
            return View();
        }

    }
}
          