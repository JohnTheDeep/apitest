using apitest.Interfaces;
using apitest.Models;
using Dapper;
using Dapper.Mapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace apitest.Services
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly IApplicationConfiguration _applicationConfiguration;
        private readonly ILogger<DatabaseManager> _logger;
        public DatabaseManager(IApplicationConfiguration applicationConfiguration, ILogger<DatabaseManager> logger)
        {
            _applicationConfiguration = applicationConfiguration;
            _logger = logger;
        }

        public async Task<bool> CreateCar(Car car)
        {
            if (car == null && car.TradeMark.Id == 0)
                return false;

            try
            {
                using (IDbConnection db = new MySqlConnection(_applicationConfiguration.DEFAULT_CONNECTION_STRING))
                {
                    await db.ExecuteAsync($"INSERT INTO {Car.TableName} (trade_mark_id,model,bodytype,fueltype,tankcapacity) " +
                        $"VALUES({car.TradeMark.Id},@model,@bodytype,@fueltype,@tankcapacity)", car);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Failed to create trade mark");
            }
            return false;
        }

        public async Task<bool> CreateTradeMark(TradeMark tradeMark)
        {
            if (tradeMark?.Name == null)
                return false;

            try
            {
                using (IDbConnection db = new MySqlConnection(_applicationConfiguration.DEFAULT_CONNECTION_STRING))
                {
                    await db.QueryAsync($"INSERT INTO {TradeMark.TableName} (Name) VALUES(@Name)", tradeMark);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Failed to create trade mark");
            }
            return false;
        }

        public async Task<IEnumerable<Car>> GetCars(int from, int to)
        {
            using (IDbConnection db = new MySqlConnection(_applicationConfiguration.DEFAULT_CONNECTION_STRING))
            {
                if (from == 0 && to == 0)
                {
                    return (await db.QueryAsync<Car, TradeMark>($"select {Car.ColumnNames},t2.Id,t2.Name from {Car.TableName} as t1 " +
                        $"inner join trade_marks as t2 on t2.Id = t1.trade_mark_id"));
                }
                else
                {
                    return (await db.QueryAsync<Car, TradeMark>($"select {Car.ColumnNames},t2.Id,t2.Name from {Car.TableName} as t1 " +
                        $"inner join trade_marks as t2 on t2.Id = t1.trade_mark_id where t1.Id between {from} and {to}"));
                }
            }
            return null;
        }

        public async Task<IEnumerable<TradeMark>> GetTradeMarks(int from, int to)
        {
            using (IDbConnection db = new MySqlConnection(_applicationConfiguration.DEFAULT_CONNECTION_STRING))
            {
                if (from == 0 && to == 0)
                {
                    return (await db.QueryAsync<TradeMark>($"select {TradeMark.ColumnNames} from {TradeMark.TableName}"));
                }
                else
                {
                    return (await db.QueryAsync<TradeMark>($"select {TradeMark.ColumnNames} from {TradeMark.TableName} where id between {from} and {to}"));
                }
            }
        }

        public async Task<bool> RemoveCar(int id)
        {
            if (id == 0)
                return false;

            try
            {
                using (IDbConnection db = new MySqlConnection(_applicationConfiguration.DEFAULT_CONNECTION_STRING))
                {
                    await db.QueryAsync($"delete from {Car.TableName} where id = {id}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Failed to create trade mark");
            }
            return false;
        }

        public async Task<bool> RemoveTradeMark(int id)
        {
            if (id == 0)
                return false;

            try
            {
                using (IDbConnection db = new MySqlConnection(_applicationConfiguration.DEFAULT_CONNECTION_STRING))
                {
                    await db.QueryAsync($"delete from {TradeMark.TableName} where id = {id}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Failed to create trade mark");
            }
            return false;
        }

        public async Task<bool> UpdateTradeMark(int id, TradeMark tradeMark)
        {
            if (id == 0)
                return false;

            try
            {
                using (IDbConnection db = new MySqlConnection(_applicationConfiguration.DEFAULT_CONNECTION_STRING))
                {
                    await db.QueryAsync($"UPDATE {TradeMark.TableName} SET Name = @Name WHERE Id = @Id", tradeMark);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Failed to create trade mark");
            }
            return false;
        }
        public async Task<bool> UpdateCar(int id, Car car)
        {
            if (id == 0)
                return false;

            try
            {
                using (IDbConnection db = new MySqlConnection(_applicationConfiguration.DEFAULT_CONNECTION_STRING))
                {
                    await db.QueryAsync($"UPDATE {Car.TableName} " +
                        $"SET trade_mark_id = {car.TradeMark.Id},model = @model," +
                        $"bodytype=@bodytype,fueltype=@fueltype,tankcapacity=@tankcapacity WHERE Id = {id}", car);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Failed to create trade mark");
            }
            return false;
        }
    }
}
