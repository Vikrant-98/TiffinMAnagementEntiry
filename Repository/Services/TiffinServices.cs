using CommonServices.ModelServices;
using Repository.ApplicationDB;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class TiffinServices : ITiffinServices
    {
        private readonly ApplicationDbContext _dbContext;
        public TiffinServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddStocks(Stocks stocks)
        {
            try
            {
                var Result = await _dbContext.Stocks.AddAsync(stocks).ConfigureAwait(false);
                _dbContext.SaveChanges();
                if (Result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> AddCustomerStocks(CustomerStocks stocks)
        {
            try
            {
                var stockDetails = _dbContext.CustomerStocks.ToList();
                var flag = stockDetails.Any(x => x.StocksId == stocks.StocksId && x.CustomerId == stocks.CustomerId);

                if (flag)
                {
                    var Entries = (from x in _dbContext.CustomerStocks
                                   where x.StocksId == stocks.StocksId && x.CustomerId == stocks.CustomerId
                                   select x).First();
                    if (Entries != null)
                    {
                        Entries.StocksId = stocks.StocksId;
                        Entries.StocksQuantity = Entries.StocksQuantity + stocks.StocksQuantity < 0 ? Entries.StocksQuantity : Entries.StocksQuantity + stocks.StocksQuantity;
                        Entries.CustomerId = stocks.CustomerId;
                        Entries.ModifiedDate = DateTime.Now;
                        _dbContext.CustomerStocks.Update(Entries);
                        _dbContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (stocks.StocksQuantity <= 0)
                    {
                        return false;
                    }
                    var Result = await _dbContext.CustomerStocks.AddAsync(stocks).ConfigureAwait(false);
                    _dbContext.SaveChanges();
                    if (Result != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public List<CustomerStocks> GetAllCustomerStocks(int CustomerId)
        {
            try
            {
                var CustomerDetails = _dbContext.CustomerStocks.Where(x => x.CustomerId == CustomerId).ToList();
                return CustomerDetails;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PortfolioDetails GetAllPortFolioDetails(int CustomerId)
        {
            try
            {
                
                PortfolioDetails portfolioDetails = new PortfolioDetails()
                {
                };

                return portfolioDetails;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Stocks> GetAllStocks()
        {
            try
            {
                return _dbContext.Stocks.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

