using MyPanditJee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPanditJee.Services.Interface
{
   public  interface IPanditJeeProfileServices
    {
        public PanditProfileModel createPanditProfile(PanditProfileModel panditProfileModel);
        public PanditProfileModel GetPandit(string email);
       

    }
}
