using System.Collections.Generic;

namespace MyPanditJee.Service.Interface
{
    interface IDropDownService
    {
       
        public List<string> GetCountryList();
        public List<string> GetStateList();
        public List<string> GetCityList();
    }
}
