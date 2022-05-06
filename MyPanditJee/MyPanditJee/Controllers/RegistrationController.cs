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


        #region Employer registration

        [HttpGet]
        public ActionResult PanditJeeRegistration()
        {
            _logger.LogInformation("The EmployerRegistration GET method has been accessed");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PanditJeeRegistration(PanditJeeRegistrationModel panditJeemodel)
        {
            _logger.LogInformation("The EmployerRegistration POST method has been accessed");
            if (ModelState.IsValid)
            {
                var encryptPassword = CommonCode.base64Encode(panditJeemodel.Password);
                var loginModel = new LoginModel
                {
                    Email = panditJeemodel.Email,
                    Password = encryptPassword,
                    UserType = CommonConstants.Recruiter
                };
                var pandieJeeProfileModel = new PanditJeeProfileModel
                {
                    PanditJeeName = panditJeemodel.Name,
                    PhoneNo = panditJeemodel.Phone,
                    Email = panditJeemodel.Email,
                   
                };
                panditJeemodel.Created = panditJeemodel.LastUpdated = DateTime.Now;
                panditJeemodel.RegistrationType = CommonConstants.User;
                panditJeemodel.Password = encryptPassword;
                _panditjeeService.registerPandit(panditJeemodel, loginModel, pandieJeeProfileModel);
                var encryptEmail = CommonCode.base64Encode(pandieJeeProfileModel.Email);
                HttpContext.Session.SetString("userId", pandieJeeProfileModel.Email);
                //SendEmail(loginModel, employerProfileModel.EmployeerName);
               // return RedirectToRoute(new { controller = "Profile", action = "EmployerProfile", encryptEmail });
            }
            return View(panditJeemodel);
        }

        #endregion

    }
}
