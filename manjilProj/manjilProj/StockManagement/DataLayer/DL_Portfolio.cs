using Entity.Connection;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManagement.DataLayer
{
   public class DL_Portfolio
    {
       
            private readonly ConnectionDB _context;

            public DL_Portfolio()
            {
                _context = new ConnectionDB();
            }


            public IQueryable<PortfolioAccount> getList()
            {
                return _context.PortfolioAccount;
            }

            public ServiceResult<PortfolioAccount> SavePortfolio(PortfolioAccount portfolioAccount)
            {
                using (var dbContextTransaction = _context.Database.BeginTransaction())
                {
                    _context.Add(portfolioAccount);
                    _context.SaveChanges();

                    dbContextTransaction.Commit();
                }

                return new ServiceResult<PortfolioAccount>() { Data = null, Message = "Success", Status = ResultStatus.Success };
            }

            public ServiceResult<PortfolioAccount> DeletePortfolio(int id)
            {
                using (var dbContextTransaction = _context.Database.BeginTransaction())
                {
                    var portfolioAccount = _context.PortfolioAccount.SingleOrDefault(m => m.Id == id);
                    _context.PortfolioAccount.Remove(portfolioAccount);
                    _context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                return new ServiceResult<PortfolioAccount>() { Data = null, Message = "Deleted Successfully", Status = ResultStatus.Success };
            }

            public ServiceResult<PortfolioAccount> UpdateStock(PortfolioAccount portfolioAccount)
            {
                using (var dbContextTransaction = _context.Database.BeginTransaction())
                {
                    _context.Update(portfolioAccount);
                    _context.SaveChanges();
                    dbContextTransaction.Commit();
                }

                return new ServiceResult<PortfolioAccount>() { Data = null, Message = "Edited Successfully", Status = ResultStatus.Success };
            }
        
    }
}
