using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poc_NoSql_MySql.Infra.Repositories
{
    public interface ITariffRepository
    {
        Task<IEnumerable<Models.Tariff>> GetAllAsync();
        Task<IEnumerable<Models.Tariff>> GetByCompanyNameAsync(string companyName);
        Task<IEnumerable<Models.Tariff>> GetByContainsCompanyNameAsync(string companyName);
        Task<bool> InsertAsync(Models.Tariff tariffNew);
        Task<decimal> GetyCompanyNameMaxTariffAsync(string companyName);
        Task<Models.Tariff> GetCurrencTariffValueAsyncRedis(string companyName, long categoryId);
        Task<Models.Tariff> GetCurrencTariffValueAsync(string companyName, long categoryId);
    }
}
