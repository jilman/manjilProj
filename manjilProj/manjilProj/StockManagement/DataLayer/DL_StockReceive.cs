using Entity.Connection;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity.ViewModel;
using System.Threading.Tasks;

namespace StockManagement.DataLayer
{
    public class DL_StockReceive
    {
        private readonly ConnectionDB _context;

        public DL_StockReceive()
        {
            _context = new ConnectionDB();
        }

        public async Task<IQueryable<StockReceiveMast>> getList()
        {
            return _context.StockReceiveMast;
        }

        public IQueryable<PortfolioAccount> PopulatePortFolioList()
        {
            return _context.PortfolioAccount;
        }

        public IQueryable<Stock> PopulateStockList()
        {
            return _context.Stock;
        }

        public IQueryable<StockReceiveMast> PopulateIndexList()
        {
            return _context.StockReceiveMast;
        }

        public ServiceResult<StockReceiveMast> CreateStockReceive(StockReceiveMast stockReceiveMast)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                _context.Add(stockReceiveMast);
                _context.SaveChanges();

                dbContextTransaction.Commit();
            }

            return new ServiceResult<StockReceiveMast>() { Data = null, Message = "Successfully Saved", Status = ResultStatus.Success };
        }
    }
}
