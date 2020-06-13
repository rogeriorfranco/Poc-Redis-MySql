
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Poc_NoSql_MySql.Models
{
    public class Category
    {
        [JsonIgnore]
        [Key]
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        [JsonIgnore]
        public long FuncionalityId { get; set; }
        [ForeignKey("FuncionalityId")]
        public virtual Funcionality Funcionality { get; set; }
    }
}
