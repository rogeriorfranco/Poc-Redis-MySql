using System;
using System.Text.Json.Serialization;

namespace Poc_NoSql_MySql.Models
{
    public class TarifiiNEW
    {
        [JsonIgnore]
        public long TariffId { get; set; }
        public string CompanyName { get; set; }
        public string ValidationType { get; set; }
        public long CategoryId { get; set; }
        public decimal TariffCurrency { get; set; }
        public CategoryNEW categoryNEW { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ExpirationDate { get; set; }
    }
}
