using Entity.Model;
using StockManagement.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManagement.BusinessLayer
{
    public class BL_Stock
    {
        DL_Stock objDl = new DL_Stock();
        public ServiceResult<Stock> CreateStock(Stock stock)
        {
            var dubcheck = objDl.getList().Where(a => a.StockName == stock.StockName);
            if (!string.IsNullOrEmpty(dubcheck.ToString()))
            {
                return new ServiceResult<Stock>()
                {
                    Data = null,
                    Message = "Stock Name '" + dubcheck.ToString() + "' Already Exists...!!! ",
                    Status = ResultStatus.Failed
                };
            }
            return new ServiceResult<Stock>()
            {
                Data = null,
                Message = "Stock Name '" + dubcheck.ToString() + "' Already Exists...!!! ",
                Status = ResultStatus.Failed
            };

        }
    }
}
