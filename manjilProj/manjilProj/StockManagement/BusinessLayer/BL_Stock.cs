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
            var isValidStock = objDl.getList().Where(a => a.StockName == stock.StockName).FirstOrDefault();
            if (!string.IsNullOrEmpty(isValidStock.StockName.ToString()))
            {
                return new ServiceResult<Stock>()
                {
                    Data = null,
                    Message = "Stock Name '" + isValidStock.StockName.ToString() + "' Already Exists...!!! ",
                    Status = ResultStatus.Failed
                };
            }
            else
            {
              var a=  objDl.SaveStock(stock);
                return new ServiceResult<Stock>()
                {
                    Data = null,
                    Message = "Stock Name '" + stock.StockName.ToString() + "' Added Successfully...!!! ",
                    Status = ResultStatus.Success
                };
            }

        }
    }
}
