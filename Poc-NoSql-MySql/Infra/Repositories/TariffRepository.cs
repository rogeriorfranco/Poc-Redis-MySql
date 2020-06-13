using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Poc_NoSql_MySql.Infra.Context;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poc_NoSql_MySql.Infra.Repositories
{
    public class TariffRepository : ITariffRepository
    {
        private readonly TariffContext _context;
        private readonly IDatabase _database;

        public TariffRepository(TariffContext context, IDatabase database)
        {
            _context = context;
            _database = database;
        }

        public async Task<IEnumerable<Models.Tariff>> GetAllAsync()
        {
            return await _context.Tariffs.ToListAsync();
        }
        public async Task<IEnumerable<Models.Tariff>> GetByCompanyNameAsync(string companyName)
        {
            return await _context.Tariffs.Where(x => x.CompanyName == companyName).ToListAsync();
        }
        public async Task<IEnumerable<Models.Tariff>> GetByContainsCompanyNameAsync(string companyName)
        {
            return await _context.Tariffs.Where(x => x.CompanyName.Contains(companyName)).ToListAsync();
        }
        public async Task<decimal> GetyCompanyNameMaxTariffAsync(string companyName)
        {
            return await Task.Run(() => _context.Tariffs.Where(x => x.CompanyName == companyName).Max(x => x.TariffCurrency));
        }
        public async Task<Models.Tariff> GetCurrencTariffValueAsyncRedis(string companyName, long categoryId)
        {
            var tariff = await CacheQuery(companyName, categoryId);
            if (tariff != null)
            {
                return tariff;
            }

            tariff = await _context.Tariffs.Where(x => x.ExpirationDate == null &&
            x.CompanyName == companyName && x.CategoryId == categoryId).FirstOrDefaultAsync();

            await SetStoreCacheValue(tariff);

            return tariff;
        }
        public async Task<Models.Tariff> GetCurrencTariffValueAsync(string companyName, long categoryId)
        {
          return await _context.Tariffs.Where(x => x.ExpirationDate == null &&
            x.CompanyName == companyName && x.CategoryId == categoryId).FirstOrDefaultAsync();
        }
        public async Task<bool> InsertAsync(Models.Tariff tariffNew)
        {
            var tariffOld = await GetCurrencTariffValueAsync(tariffNew.CompanyName, tariffNew.CategoryId);

            if (tariffOld != null)
            {
                tariffOld.ExpirationDate = DateTime.UtcNow;
                _context.Entry(tariffOld).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            await _context.Tariffs.AddAsync(tariffNew);
            var inserted = await _context.SaveChangesAsync();

            return inserted > 0 ? true : false;
        }
        private static string TariffCacheKey = "poc-nosql-mysql:tariff:{0}{1}";

        private async Task<Models.Tariff> CacheQuery(string companyName, long categoryId)
        {
            string checkTariffExists = await
                _database.StringGetAsync(string.Format(TariffCacheKey, companyName, categoryId));
            if (checkTariffExists != null)
                return JsonConvert.DeserializeObject<Models.Tariff>(checkTariffExists);

            return null;
        }

        private async Task SetStoreCacheValue(Models.Tariff tariffNew)
        {
            if (tariffNew == null)
                return;

            var optionsCache = TimeSpan.FromDays(1);

            await _database.
                StringSetAsync(string.Format(TariffCacheKey, tariffNew.CompanyName, tariffNew.CategoryId), JsonConvert.SerializeObject(tariffNew), optionsCache);
        }
    }
}
