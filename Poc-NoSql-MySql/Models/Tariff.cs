using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Poc_NoSql_MySql.Models
{
    public class Tariff
    {
        [JsonIgnore]
        [Key]
        public long TariffId { get; set; }
        public string CompanyName { get; set; }
        public string ValidationType { get; set; }
        public long CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [JsonIgnore]
        public virtual Category Category { get; set; }
        public decimal TariffCurrency { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ExpirationDate { get; set; }
    }
}
