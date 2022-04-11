using MyPanditJee.Models;
using Microsoft.AspNetCore.Mvc;
using MyPanditJee.Service;
using System;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using MongoDB.Bson;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using MongoDB.Driver;
using MyPanditJee.Common;

namespace MyPanditJee.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly UserService _userService;

        private readonly ILogger<RegistrationController> _logger;

        private readonly IWebHostEnvironment _hostEnvironment;

        public RegistrationController(UserService userService, ILogger<RegistrationController> logger, IWebHostEnvironment hostEnvironment)
        {
            _userService = userService;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }




        #region User registration

        [HttpGet]
        public ActionResult UserRegistration()
        {
            _logger.LogInformation("The UserRegistration GET method has been accessed");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserRegistration(UserRegistrationModel userRegModel)
        {
            _logger.LogInformation("The UserRegistration POST method has been accessed");
            if (ModelState.IsValid)
            {
                var encryptPassword = CommonCode.base64Encode(userRegModel.Password);
                var loginModel = new LoginModel
                {
                    Email = userRegModel.Email,
                    Password = encryptPassword,
                    UserType = CommonConstants.User
                };
                var userProfileModel = new UserProfileModel
                {
                    Name = userRegModel.Name,
                    Phone = userRegModel.Phone,
                    Email = userRegModel.Email,
                    HasProfileImage = false,
                    Followers = 0,
                    Following = 0,
                    TotalUploadedVideo = 0,
                    TotalUploadedPicture = 0,
                    TotalConnectionReceived = 0
                };
                userRegModel.Created = userRegModel.LastUpdated = DateTime.Now;
                userRegModel.RegistrationType = CommonConstants.User;
                userRegModel.Password = encryptPassword;
                _userService.RegisterUser(userRegModel, loginModel, userProfileModel);
                var encryptEmail = CommonCode.base64Encode(userProfileModel.Email);
               
                //SendEmail(loginModel, userProfileModel.Name);
                //return RedirectToRoute(new { controller = "Profile", action = "UserProfile", encryptEmail });
            }
            return View(userRegModel);
        }

        #endregion


    }
}
