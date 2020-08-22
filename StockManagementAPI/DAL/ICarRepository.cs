using StockManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockManagementAPI.DAL
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetCars();
        Task<Car> GetCar(int carId);
        Task<int> CreateCar(Car newCar);
        Task<int> UpdateCar(Car oldCar);
        Task<bool> DeleteCar(int carId);
    }
}
