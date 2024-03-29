﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyPanditJee.Common;
using MyPanditJee.Models;
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
        public readonly PanditJeeServices _panditService;

        private readonly ILogger<ProfileController> _logger;

        public ProfileController(UserProfileService userProfile, LoginService loginService, ILogger<ProfileController> logger, PanditJeeProfileServices panditProfile, PanditJeeServices panditService)
        {
            _userProfile = userProfile;
            _loginService = loginService;
            _logger = logger;
            _panditProfile = panditProfile;
            _panditService = panditService;
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
        [HttpGet]
        public ActionResult BookPandit()
        {
           
                _logger.LogInformation("The UploadFile GET method has been accessed");
                var model = new BookPanditModel();
                


                return PartialView("View", model);

        
        }

        [HttpPost]
        public ActionResult BookPandit(BookPanditModel bookPanditModel)
        {

            _logger.LogInformation("The UploadFile Post method has been accessed");
            _userProfile.BookPandit(bookPanditModel);



            return PartialView("View");


        }


        [HttpGet]
        public ActionResult getAllPandit()
        {
           
                
                var pan = _panditService.GetAllPandit();
         
            return View(pan);
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

        [HttpGet]
        public ActionResult PanditProfileEdit(string encryptEmail)
        {
            _logger.LogInformation("The UserProfileEdit GET method has been accessed");
            var email = CommonCode.base64Decode(encryptEmail);
            var user = _panditService.GetPandit(email);
            return View(user);
        }

        [HttpPost]
        public ActionResult PanditProfileEdit(PanditProfileModel panditProfileModel)
        {
            _logger.LogInformation("The UserProfileEdit POST method has been accessed");
            if (ModelState.IsValid)
            {
                _panditService.Update(panditProfileModel);
            }
            var encryptEmail = CommonCode.base64Encode(panditProfileModel.Email);
            return RedirectToRoute(new { controller = "Profile", action = "PanditProfile", encryptEmail });
        }

    }
}
          