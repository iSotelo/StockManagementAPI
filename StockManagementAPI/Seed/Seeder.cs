using Newtonsoft.Json;
using StockManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StockManagementAPI.Seed
{
    public static class Seeder
    {
        public static void WriteSeed()
        {
            try
            {
                List<Car> cars = new List<Car>();

                Car car1 = new Car()
                {
                    CarId = 1,
                    Brand = "Ford",
                    Model = "Focus",
                    Year = 2010,
                    Kilometers = 10000,
                    Price = 100000,
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s"
                };
                Car car2 = new Car()
                {
                    CarId = 2,
                    Brand = "Chevrolet",
                    Model = "Impala",
                    Year = 2011,
                    Kilometers = 100000,
                    Price = 150000,
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s"
                };
                Car car3 = new Car()
                {
                    CarId = 3,
                    Brand = "Nissan",
                    Model = "GTR",
                    Year = 2015,
                    Kilometers = 5000,
                    Price = 1000000,
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s"
                };
                Car car4 = new Car()
                {
                    CarId = 4,
                    Brand = "Volkswagen",
                    Model = "Jetta",
                    Year = 2009,
                    Kilometers = 120000,
                    Price = 110000,
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s"
                };
                Car car5 = new Car()
                {
                    CarId = 5,
                    Brand = "Toyota",
                    Model = "RAV-4",
                    Year = 2015,
                    Kilometers = 50000,
                    Price = 200000,
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s"
                };

                cars.Add(car1);
                cars.Add(car2);
                cars.Add(car3);
                cars.Add(car4);
                cars.Add(car5);

                // write to dataset
                string carsJson = JsonConvert.SerializeObject(cars);
                string pathJsonData = new Settings().PathJsonData;
                if (!File.Exists(pathJsonData))
                {
                    System.IO.File.WriteAllText(pathJsonData, carsJson);
                }
                else
                {
                    /// Have data
                    List<Car> carsInFile = new List<Car>();
                    using (StreamReader jsonStream = File.OpenText(pathJsonData))
                    {
                        var json = jsonStream.ReadToEnd();
                        carsInFile = JsonConvert.DeserializeObject<IEnumerable<Car>>(json).ToList();
                    }
                    if (carsInFile.Count <= 0)
                    {
                        System.IO.File.WriteAllText(pathJsonData, carsJson);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
