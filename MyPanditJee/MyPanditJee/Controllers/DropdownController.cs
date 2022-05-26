using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyPanditJee.Service;

namespace MyPanditJee.Controllers
{
    public class DropdownController: Controller
    {
        public readonly DropDownService _dropDownService;

        public DropdownController(DropDownService dropDownService)
        {
            _dropDownService = dropDownService;
        }


        [HttpGet]
        public JsonResult GetCountry()
        {
            var list = _dropDownService.GetCountryList();
            return Json(list);
        }

        [HttpGet]
        public JsonResult GetState()
        {
            var list = _dropDownService.GetStateList();
            return Json(list);
        }

        public JsonResult GetCities()
        {
            var list = _dropDownService.GetCityList();
            return Json(list);
        }
    }
}
