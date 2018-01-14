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

        public IActionResult Details(int id)
        {
            var model =  _restaurantData.Get(id);

            if(model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RestaurantEditModel model)
        {
            if (ModelState.IsValid)
            {


                var newRestaurant = new Restaurant();
                newRestaurant.Name = model.Name;
                newRestaurant.Cuisine = model.Cuisine;

                newRestaurant = _restaurantData.Add(newRestaurant);

                return RedirectToAction(nameof(Details), new { id = newRestaurant.Id });
            }
            else
            {
                return View();
            }
        }
    }
}
