using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authentication;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyPanditJee.Service;
using MyPanditJee.Models;
using MyPanditJee.Common;


namespace MyPanditJee.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;
        private readonly ILogger<LoginController> _logger;
        public readonly UserProfileService _userProfile;
        //public readonly EmployerProfileService _employerProfile;
        public LoginController(LoginService loginService, ILogger<LoginController> logger)
        {
            _logger = logger;
            _loginService = loginService;
            

        }

        [HttpGet]
        public IActionResult Login()
        {
            _logger.LogInformation("The Login page has been accessed");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });


        [Route("Login/{provider}")]
        public IActionResult Login(string provider, string returnUrl = null) =>
            Challenge(new AuthenticationProperties { RedirectUri = returnUrl ?? "/" }, provider);

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("The Login POST has been accessed");
                var loginResponse = _loginService.validateCredential(loginModel);
                //TODO: need to check if user has register as user and employer both, in this case we have to show option to select one
                if (loginResponse != null && loginResponse.LoginStatus != null)
                {
                    if (loginResponse.LoginStatus == true)
                    {
                        
                        HttpContext.Session.SetString("userId", loginModel.Email);
                        var encryptEmail = CommonCode.base64Encode(loginModel.Email);

                       var userName = _userProfile.GetUser(loginModel.Email);
                        if (userName != null)
                        {
                            HttpContext.Session.SetString("userName", userName.Name.ToString());
                        }

                       
                        if (loginResponse.UserType == CommonConstants.User)
                        {
                            HttpContext.Session.SetString("userType", loginResponse.UserType.ToString());
                            return RedirectToRoute(new { controller = "Profile", action = "UserProfile", encryptEmail });
                        }
                       /* else if (loginResponse.UserType == CommonConstants.Recruiter)
                        {
                            HttpContext.Session.SetString("userType", loginResponse.UserType.ToString());
                            return RedirectToRoute(new { controller = "Landing", action = "Landing", encryptEmail });
                        }
                        else if (loginResponse.UserType == CommonConstants.Admin)
                        {
                            HttpContext.Session.SetString("userType", loginResponse.UserType.ToString());
                            return RedirectToRoute(new { controller = "Admin", action = "AdminLogin", });
                        }*/
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invallid User Name or Password";
                    }
                }
                ViewBag.ErrorMessage = "User does't exits, Please register yourself!!";
            }
            //TODO: Validation message need to be shown
            return View();
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            _logger.LogInformation("The LogOut has been accessed");
            HttpContext.Session.Remove("userId");
            HttpContext.Session.Remove("userType");
            HttpContext.Session.Clear();
            return RedirectToRoute(new { controller = "Landing", action = "Landing" });
        }

        [HttpGet]
        public IActionResult LogOff()
        {
            _logger.LogInformation("The LogOut has been accessed");

            HttpContext.Session.Remove("userId");
            HttpContext.Session.Remove("userType");
            HttpContext.Session.Clear();
            
            return RedirectToRoute(new { controller = "Login", action = "Login" });
        }
    }
}
