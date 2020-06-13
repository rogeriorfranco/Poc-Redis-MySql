
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Poc_NoSql_MySql.Models
{
    public class Funcionality
    {
        [JsonIgnore]
        [Key]
        public long FuncionalityId { get; set; }
        public string FuncionalityName { get; set; }
    }
}
