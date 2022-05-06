using MyPanditJee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPanditJee.Services.Interface
{
   public  interface IPanditJeeProfileServices
    {
        public PanditJeeRegistrationModel createEmployerProfile(PanditJeeRegistrationModel panditProfileModel);
        public PanditJeeRegistrationModel GetPandit(string email);
    }
}
