using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StockManagementAPI.DAL;
using StockManagementAPI.Models;
using System.Threading.Tasks;

namespace StockManagementAPI.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;
        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var car = await _carRepository.GetCar(id);
            //string carJson = JsonParseLoopHandlingIgnore(car);
            var cars = await _carRepository.GetCars();
            //string carsJson = JsonParseLoopHandlingIgnore(cars);
            ViewData["register"] = car;
            ViewData["Cars"] = cars;
            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            var cars = await _carRepository.GetCars();
            string carsJson = JsonParseLoopHandlingIgnore(cars);
            return Ok(carsJson);
        }

        [HttpPost]
        public async Task<IActionResult> GerCar(int carid)
        {
            var car = await _carRepository.GetCar(carid);
            if (car != null && car.CarId > 0)
            {
                string carJson = JsonParseLoopHandlingIgnore(car);
                return Ok(carJson);
            }
            else
                return BadRequest(ResponseCodes.CarNotFound);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(Car newcar)
        {
            /// TODO Validations Models
            var result = await _carRepository.CreateCar(newcar);
            if (result)
                return Ok(result);
            else
                return BadRequest(ResponseCodes.ErrorToCreate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCar(Car cartoupdate)
        {
            /// TODO Validations Models
            var result = await _carRepository.UpdateCar(cartoupdate);
            if (result)
                return Ok(result);
            else
                return BadRequest(ResponseCodes.ErrorToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCar(int carid)
        {
            var result = await _carRepository.DeleteCar(carid);
            if (result)
                return Ok(result);
            else
                return BadRequest(ResponseCodes.ErrorToDelete);
        }


        private string JsonParseLoopHandlingIgnore(object data)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new DefaultNamingStrategy()
            };
            var serializeSettings = new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            return JsonConvert.SerializeObject(data, serializeSettings);
        }

        private enum ResponseCodes
        {
            CarNotFound = 1,
            ErrorToCreate = 2,
            ErrorToUpdate = 3,
            ErrorToDelete = 4
        }
    }
}
