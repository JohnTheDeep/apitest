using apitest.Models;

namespace apitest.Interfaces
{
    public interface IDatabaseManager
    {
        Task<IEnumerable<TradeMark>> GetTradeMarks(int from, int to);
        Task<bool> CreateTradeMark(TradeMark tradeMark);
        Task<bool> RemoveTradeMark(int id);
        Task<bool> UpdateTradeMark(int id, TradeMark tradeMark);
        Task<IEnumerable<Car>> GetCars(int from, int to);
        Task<bool> CreateCar(Car car);
        Task<bool> RemoveCar(int id);
        Task<bool> UpdateCar(int id, Car car);
    }
}
