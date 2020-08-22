using Newtonsoft.Json;
using StockManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagementAPI.DAL
{
    public class CarRepository : ICarRepository
    {
        public async Task<int> CreateCar(Car newCar)
        {
            int carid = 0;
            try
            {
                List<Car> cars = ReadCars().ToList();

                if (cars.Count > 0)
                {
                    int lastid = cars.Max(I => I.CarId);
                    newCar.CarId = lastid + 1;
                    
                    // get namefile
                    if(newCar.Image != null && newCar.Image.Length > 0)
                    {
                        var ext = System.IO.Path.GetExtension(newCar.Image.FileName);
                        string filename = newCar.CarId + "." + ext;
                        newCar.ImageName = filename;
                    }

                    cars.Add(newCar);
                    await WriteCars(cars);
                    carid = newCar.CarId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return carid;
        }

        public async Task<bool> DeleteCar(int carId)
        {
            bool response = false;
            try
            {
                List<Car> cars = ReadCars().ToList();

                if (cars.Count > 0)
                {
                    var car = cars.FirstOrDefault(C => C.CarId == carId);
                    if (car != null)
                    {
                        cars.Remove(car);
                        await WriteCars(cars);
                        response = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return response;
        }

        public async Task<Car> GetCar(int carId)
        {
            Car car = null;
            try
            {
                List<Car> cars = ReadCars().ToList();

                if (cars.Count > 0)
                {
                    car = cars.FirstOrDefault(C => C.CarId == carId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return car;
        }

        public async Task<IEnumerable<Car>> GetCars()
        {
            List<Car> cars = new List<Car>();
            try
            {
                cars = ReadCars().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
            }
            return cars;
        }

        public async Task<int> UpdateCar(Car carToUpdate)
        {
            int carid = 0;
            try
            {
                List<Car> cars = ReadCars().ToList();

                if (cars.Count > 0)
                {
                    var car = cars.FirstOrDefault(C => C.CarId == carToUpdate.CarId);
                    if (car != null)
                    {
                        /// delete object to list
                        cars.Remove(car);

                        // update object
                        car.CarId = carToUpdate.CarId;
                        car.Brand = carToUpdate.Brand;
                        car.Model = carToUpdate.Model;
                        car.Description = carToUpdate.Description;
                        car.Year = carToUpdate.Year;
                        car.Kilometers = carToUpdate.Kilometers;
                        car.Price = carToUpdate.Price;

                        // get namefile
                        if (carToUpdate.Image != null && carToUpdate.Image.Length > 0)
                        {
                            var ext = System.IO.Path.GetExtension(carToUpdate.Image.FileName);
                            string filename = carToUpdate.CarId + "." + ext;
                            car.ImageName = filename;
                        }

                        // add updated object
                        cars.Add(car);

                        // write changes to store
                        await WriteCars(cars);

                        carid = car.CarId;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return carid;
        }


        private IEnumerable<Car> ReadCars()
        {
            List<Car> cars = new List<Car>();
            string pathJsonData = new Settings().PathJsonData;
            using (StreamReader jsonStream = File.OpenText(pathJsonData))
            {
                var json = jsonStream.ReadToEnd();
                cars = JsonConvert.DeserializeObject<IEnumerable<Car>>(json).ToList();
            }
            return cars;
        }

        private async Task WriteCars(List<Car> cars)
        {
            string carsJson = JsonConvert.SerializeObject(cars);
            string pathJsonData = new Settings().PathJsonData;
            if (File.Exists(pathJsonData))
            {
                System.IO.File.WriteAllText(pathJsonData, carsJson);
            }
        }

    }
}
