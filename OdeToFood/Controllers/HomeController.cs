using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;
using OdeToFood.Services;
using OdeToFood.ViewModels;

namespace OdeToFood.Controllers
{
    public class HomeController :  Controller
    {
        private IRestaurantData _restaurantData;
        private IGreeter _greeterService;

        public HomeController(IRestaurantData restaurantData,
                                IGreeter greeterService)
        {
            _restaurantData = restaurantData;
            _greeterService = greeterService;
        }
        public IActionResult Index()
        {
            var model = new HomeIndexViewModel();

            model.Restaurants = _restaurantData.GetAll();
            model.CurrentMessageOfTheDay = _greeterService.GetMessageOfTheDay();


            return View(model);
            //return Content("Hello from the HomeController Test!");
        }
    }
}
