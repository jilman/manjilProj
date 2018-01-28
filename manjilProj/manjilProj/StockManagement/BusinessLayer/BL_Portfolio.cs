using Entity.Model;
using StockManagement.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockManagement.BusinessLayer
{
   public class BL_Portfolio
    {
        DL_Portfolio objDl = new DL_Portfolio();
        public ServiceResult<PortfolioAccount> CreatePortfolio(PortfolioAccount portfolio)
        {
            var isValidStock = objDl.getList().Where(a => a.AccountName == portfolio.AccountName &&  a.AccountNumber== portfolio.AccountNumber).FirstOrDefault();
            if (isValidStock != null)
            {
                return new ServiceResult<PortfolioAccount>()
                {
                    Data = null,
                    Message = "Portfolio " + isValidStock.AccountName.ToString() + " Already Exists...!!! ",
                    Status = ResultStatus.Failed
                };
            }
            else
            {
                var a = objDl.SavePortfolio(portfolio);
                return new ServiceResult<PortfolioAccount>()
                {
                    Data = null,
                    Message = "Portfolio '" + portfolio.AccountName.ToString() + "' Added Successfully...!!! ",
                    Status = ResultStatus.Success
                };
            }

        }


        public IQueryable<PortfolioAccount> PopulateList()
        {
            return objDl.getList();
        }

        public PortfolioAccount GetListbyId(int id)
        {
            return objDl.getList().Where(a => a.Id == id).FirstOrDefault();
        }

        public ServiceResult<PortfolioAccount> DeletePortfolio(int id)
        {
            var isValidId = objDl.getList().Where(a => a.Id == id).FirstOrDefault();
            if (isValidId != null)
            {
                var a = objDl.DeletePortfolio(id);
                return new ServiceResult<PortfolioAccount>()
                {
                    Data = null,
                    Message = a.Message,
                    Status = a.Status
                };
            }
            else
            {

                return new ServiceResult<PortfolioAccount>()
                {
                    Data = null,
                    Message = "Stock Not found for the Deletions...!!! ",
                    Status = ResultStatus.Failed
                };
            }
        }

        //public ServiceResult<PortfolioAccount> EditStock(PortfolioAccount stock)
        //{
        //    PortfolioAccount model = objDl.getList().Where(a => a.StockName == stock.StockName).FirstOrDefault();
        //    if (model != null)
        //    {
        //        return new ServiceResult<PortfolioAccount>()
        //        {
        //            Data = null,
        //            Message = "Stock Name " + model.StockName.ToString() + " Already Exists...!!! ",
        //            Status = ResultStatus.Failed
        //        };
        //    }
        //    else
        //    {
        //        model = objDl.getList().Where(c => c.Id == stock.Id).FirstOrDefault();
        //        if (model == null)
        //        {
        //            return new ServiceResult<PortfolioAccount>()
        //            {
        //                Data = null,
        //                Message = "No Data Found For Edit...!!! ",
        //                Status = ResultStatus.Failed
        //            };
        //        }
        //        else
        //        {
        //            model.StockName = stock.StockName;
        //            var a = objDl.UpdateStock(model);

        //            return new ServiceResult<Stock>()
        //            {
        //                Data = null,
        //                Message = "Stock Name '" + stock.StockName.ToString() + "' Edited Successfully...!!! ",
        //                Status = ResultStatus.Success
        //            };
        //        }
        //    }
        //}
    }
}
