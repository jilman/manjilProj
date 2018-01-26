using Entity.Connection;
using Entity.Model;
using StockManagement.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement.DataLayer
{
    public class DL_Stock
    {
        private readonly ConnectionDB _context;

        public DL_Stock()
        {
            _context = new ConnectionDB();
        }


        public IQueryable<Stock> getList()
        {
            return _context.Stock;
        }

        public ServiceResult<Stock> SaveStock(Stock stock)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                _context.Add(stock);
                _context.SaveChanges();
                
                dbContextTransaction.Commit();
            }

            return new ServiceResult<Stock>() { Data = null, Message = "Success", Status = ResultStatus.Success };
        }

        public ServiceResult<Stock> DeleteStock(int id)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                var stock = _context.Stock.SingleOrDefault(m => m.Id == id);
                _context.Stock.Remove(stock);
                _context.SaveChanges();
                dbContextTransaction.Commit();
            }
            return new ServiceResult<Stock>() { Data = null, Message = "Deleted Successfully", Status = ResultStatus.Success };
        }

        public ServiceResult<Stock> UpdateStock(Stock stock)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                _context.Update(stock);
                _context.SaveChanges();
                dbContextTransaction.Commit();
            }

            return new ServiceResult<Stock>() { Data = null, Message = "Edited Successfully", Status = ResultStatus.Success };
        }
    }
}
