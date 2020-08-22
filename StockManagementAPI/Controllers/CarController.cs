using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StockManagementAPI.DAL;
using StockManagementAPI.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace StockManagementAPI.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;
        private IHostingEnvironment _hostingEnvironment;
        public CarController(ICarRepository carRepository, IHostingEnvironment environment)
        {
            _carRepository = carRepository;
            _hostingEnvironment = environment;
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
            foreach (var car in cars)
                car.ImageUrl = GetImageUrl(car);
            string carsJson = JsonParseLoopHandlingIgnore(cars);
            return Ok(carsJson);
        }

        [HttpPost]
        public async Task<IActionResult> GerCar(int carid)
        {
            var car = await _carRepository.GetCar(carid);
            car.ImageUrl = GetImageUrl(car);
            if (car != null && car.CarId > 0)
            {
                string carJson = JsonParseLoopHandlingIgnore(car);
                return Ok(carJson);
            }
            else
                return BadRequest(ResponseCodes.CarNotFound);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar([FromForm]Car newcar)
        {
            /// TODO Validations Models
            // Get file url
            var result = await _carRepository.CreateCar(newcar);
            if (result > 0)
            {
                // write image in wwwroot/images
                try
                {
                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    if (newcar.Image.Length > 0)
                    {
                        var ext = System.IO.Path.GetExtension(newcar.Image.FileName);
                        string filename = result.ToString() + "." + ext; // result is the card id
                        var filePath = Path.Combine(uploads, filename);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await newcar.Image.CopyToAsync(fileStream);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.ToString());
                }
                return Ok(result);
            }
            else
                return BadRequest(ResponseCodes.ErrorToCreate);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateCar([FromForm]Car cartoupdate)
        {
            /// TODO Validations Models

            var result = await _carRepository.UpdateCar(cartoupdate);
            if (result > 0)
            {
                // write image in wwwroot/images
                try
                {
                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    if (cartoupdate.Image.Length > 0)
                    {
                        var ext = System.IO.Path.GetExtension(cartoupdate.Image.FileName);
                        string filename = cartoupdate.CarId.ToString() + "." + ext;
                        var filePath = Path.Combine(uploads, filename);
                        using (var fileStream = new FileStream(filePath, FileMode.Append))
                        {
                            await cartoupdate.Image.CopyToAsync(fileStream);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.ToString());
                }
                return Ok(result);
            }
            else
                return BadRequest(ResponseCodes.ErrorToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCar(int carid)
        {
            var oldcar = await _carRepository.GetCar(carid);
            var result = await _carRepository.DeleteCar(carid);
            if (result)
            {
                // delete image
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                var filePath = Path.Combine(uploads, oldcar.ImageName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                return Ok(result);
            }
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

        private string GetImageUrl(Car car)
        {
            var filePath = $"/Images/" + car.ImageName;
            return filePath;
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
