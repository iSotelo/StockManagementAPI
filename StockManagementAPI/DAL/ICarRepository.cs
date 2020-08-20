using StockManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockManagementAPI.DAL
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetCars();
        Task<Car> GetCar(int carId);
        Task<bool> CreateCar(Car newCar);
        Task<bool> UpdateCar(Car oldCar);
        Task<bool> DeleteCar(int carId);
    }
}
