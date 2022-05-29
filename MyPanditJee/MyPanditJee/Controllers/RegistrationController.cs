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
using MyPanditJee.Services;

namespace MyPanditJee.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly UserService _userService;
        private readonly PanditJeeServices _panditjeeService;

        private readonly ILogger<RegistrationController> _logger;

        private readonly IWebHostEnvironment _hostEnvironment;

        public RegistrationController(UserService userService, ILogger<RegistrationController> logger, PanditJeeServices panditjeeService, IWebHostEnvironment hostEnvironment)
        {
            _userService = userService;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
            _panditjeeService = panditjeeService;
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


        #region pandit registration

        [HttpGet]
        public ActionResult PanditRegistration()
        {
            _logger.LogInformation("The PanditRegistration GET method has been accessed");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PanditRegistration(PanditRegistrationModel panRegModel)
        {
            _logger.LogInformation("The UserRegistration POST method has been accessed");
            if (ModelState.IsValid)
            {
                var encryptPassword = CommonCode.base64Encode(panRegModel.Password);
                var loginModel = new LoginModel
                {
                    Email = panRegModel.Email,
                    Password = encryptPassword,
                    UserType = CommonConstants.Pandit
                };
                var panditProfileModel = new PanditProfileModel
                {
                    Name = panRegModel.Name,
                    Phone = panRegModel.Phone,
                    Email = panRegModel.Email,
                    HasProfileImage = false,

                };
                panRegModel.Created = panRegModel.LastUpdated = DateTime.Now;
                panRegModel.RegistrationType = CommonConstants.Pandit;
                panRegModel.Password = encryptPassword;
                _panditjeeService.registerPandit(panRegModel, loginModel, panditProfileModel);
                var encryptEmail = CommonCode.base64Encode(panditProfileModel.Email);

                //SendEmail(loginModel, userProfileModel.Name);
                //return RedirectToRoute(new { controller = "Profile", action = "UserProfile", encryptEmail });
            }
            return View(panRegModel);
        }


        #endregion

    }
}
