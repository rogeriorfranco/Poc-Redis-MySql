using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Poc_NoSql_MySql.Models
{
    public class Level
    {
        [Key]
        public long LevelId { get; set; }
        public string LevelName { get; set; }

    }
}
