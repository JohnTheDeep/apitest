using apitest.Models.Enums;

namespace apitest.Models
{
    public class Car
    {
        public int Id { get; set; }
        public TradeMark TradeMark { get; set; }
        public string Model { get; set; }
        public BodyType BodyType { get; set; }
        public FuelType FuelType { get; set; }
        public decimal TankCapacity { get; set; }

        public static readonly string TableName = "cars";
        public static readonly string ColumnNames = string.Join(",", "t1.id",
            "model", "bodytype", "fueltype", "tankcapacity");
        public Car() { }
        public Car(TradeMark tradeMark, string model, BodyType bodyType, FuelType fuelType, decimal tankCapacity)
        {
            TradeMark = tradeMark;
            Model = model;
            BodyType = bodyType;
            FuelType = fuelType;
            TankCapacity = tankCapacity;
        }
    }

}
