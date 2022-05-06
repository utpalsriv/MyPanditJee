using MyPanditJee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPanditJee.Services.Interface
{
   public  interface IPanditJeeServices
    {
        public PanditJeeRegistrationModel registerPandit(PanditJeeRegistrationModel panditRegistrationModel, LoginModel loginModel, PanditJeeProfileModel panditProfileModel);
    }
}
