using Business.Interface;
using CommonServices.ModelServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TiffinManagementAPI.Controllers
{
    public class TiffinController : Controller
    {
        private readonly ITiffinBusiness _TiffinBusiness;

        public TiffinController(ITiffinBusiness TiffinBusiness) 
        {
            _TiffinBusiness = TiffinBusiness;
        }

        [HttpPost("AddStocks")]
        public async Task<bool> AddStocks([FromBody] AddStocks userStocks)
        {
            var Result = await _TiffinBusiness.AddStocks(userStocks).ConfigureAwait(false);
            return Result;
        }

        [HttpGet("GetAllStocks")]
        public List<Stocks> GetAllStocks()
        {
            var Result = _TiffinBusiness.GetAllStocks();
            return Result;
        }

        [HttpGet("GetAllCustomerStocks")]

        public List<CustomerStocks> GetAllCustomerStocks(int CustomerId)
        {
            var Result = _TiffinBusiness.GetAllCustomerStocks(CustomerId);
            return Result;
        }

        [HttpPost("AddCustomerStocks")]
        [Authorize(Roles = "Admin")]
        public async Task<bool> AddCustomerStocks([FromBody] AddCustomerStocks stocks)
        {
            var Result = await _TiffinBusiness.AddCustomerStocks(stocks).ConfigureAwait(false);
           return Result;
        }

        [HttpPost("RemoveCustomerStocks")]
        [Authorize(Roles = "Admin")]
        public async Task<bool> RemoveCustomerStocks([FromBody] AddCustomerStocks stocks)
        {
            stocks.StocksQuantity = 0 - stocks.StocksQuantity;
            var Result = await _TiffinBusiness.AddCustomerStocks(stocks).ConfigureAwait(false);
            return Result;
        }
    }
}
