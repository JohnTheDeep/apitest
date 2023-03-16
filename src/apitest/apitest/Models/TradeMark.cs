using System.ComponentModel.DataAnnotations;

namespace apitest.Models
{
    public class TradeMark
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public TradeMark() { }
        public TradeMark(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public TradeMark(string name)
        {
            Name = name;
        }
        public static readonly string TableName = "trade_marks";
        public static readonly string ColumnNames = string.Join(",", "id", "name");
    }
}
