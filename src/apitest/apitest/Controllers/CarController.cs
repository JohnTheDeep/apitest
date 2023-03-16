using apitest.Interfaces;
using apitest.Middleware;
using apitest.Models;
using apitest.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace apitest.Controllers
{
    public class CarController : Controller
    {
        private readonly IDatabaseManager _databaseManager;
        public CarController(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }
        [Authorize]
        [HttpGet("/GetCars")]
        public async Task<IActionResult> GetCars(int from = 0, int to = 0)
        {
            return Json(await _databaseManager.GetCars(from, to));
        }
        [Authorize]
        [HttpPost("/CreateCar")]
        public async Task<IActionResult> CreateCar(int tradeMarkId, string model, BodyType bodyType, FuelType fuelType, decimal tankCapacity)
        {
            if (await _databaseManager.CreateCar(new Car(new TradeMark(tradeMarkId, ""), model, bodyType, fuelType, tankCapacity)))
                return Json("Added new car");
            else
                return Json("Something wrong !");

        }
        [Authorize]
        [HttpDelete("/RemoveCar")]
        public async Task<IActionResult> RemoveCar(int carId)
        {
            if (await _databaseManager.RemoveCar(carId))
                return Json("Removed car");
            else
                return Json("Something wrong !");

        }
        [Authorize]
        [HttpPut("/UpdateCar")]
        public async Task<IActionResult> UpdateCar(int carId, int tradeMarkId, string model, BodyType bodyType, FuelType fuelType, decimal tankCapacity)
        {
            if (await _databaseManager.UpdateCar(carId, new Car(new TradeMark(tradeMarkId, ""), model, bodyType, fuelType, tankCapacity)))
                return Json("Updated car !");
            else
                return Json("Something wrong !");

        }
    }
}
