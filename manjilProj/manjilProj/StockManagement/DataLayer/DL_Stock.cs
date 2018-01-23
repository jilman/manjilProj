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
            return  _context.Stock;
        }
    }
}
