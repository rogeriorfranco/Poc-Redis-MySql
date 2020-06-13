using Microsoft.AspNetCore.Mvc;
using Poc_NoSql_MySql.Infra.Repositories;
using StackExchange.Redis;
using System.Threading.Tasks;
using System.Collections.Generic;
using Poc_NoSql_MySql.Models;

namespace Poc_NoSql_MySql.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TariffController : ControllerBase
    {
        private readonly ITariffRepository _tariffRepository;
        public TariffController(ITariffRepository tariffRepository)
        {
            _tariffRepository = tariffRepository;
        }

        [HttpGet("listAllNEW")]
        public IActionResult listAllNEW()
        {
            var t = new TarifiiNEW();
            t.CategoryId = (long)CategoryNEW.Cash_In;
            t.TariffCurrency = 1;
            t.CompanyName = "TQI";

           

            return Ok(t);
        }

        [HttpGet("listAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var tariff = await _tariffRepository.GetAllAsync();

            return Ok(tariff);
        }
        
        [HttpGet("listCompanyName/{companyName}")]
        public async Task<IActionResult> GetByCompanyNameAsync(string companyName)
        {
            var tariff = await _tariffRepository.GetByCompanyNameAsync(companyName);

            return Ok(tariff);
        }

        [HttpGet("listContainsCompanyName/{companyName}")]
        public async Task<IActionResult> GetByContainsCompanyNameAsync(string companyName)
        {
            var tariff = await _tariffRepository.GetByContainsCompanyNameAsync(companyName);

            return Ok(tariff);
        }

        [HttpGet("listMaxCompanyName/{companyName}")]
        public async Task<IActionResult> GetMaxTarifaAsync(string companyName)
        {
            var tariff = await _tariffRepository.GetyCompanyNameMaxTariffAsync(companyName);

            return Ok(tariff);
        }

        [HttpGet("currencTariffValueRedis/{companyName}/{categoryId}")]
        public async Task<IActionResult> GetCurrencTariffValueAsyncRedis(string companyName, long categoryId)
        {
            var tariff = await _tariffRepository.GetCurrencTariffValueAsyncRedis(companyName, categoryId);

            return Ok(tariff);
        }

        [HttpGet("currencTariffValue/{companyName}/{categoryId}")]
        public async Task<IActionResult> GetCurrencTariffValueAsync(string companyName, long categoryId)
        {
            var tariff = await _tariffRepository.GetCurrencTariffValueAsync(companyName, categoryId);

            return Ok(tariff);
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> InsertAsync([FromBody]Models.Tariff data)
        {
            var tariff = await _tariffRepository.InsertAsync(data);

            return Ok(tariff);
        }
    }
}

