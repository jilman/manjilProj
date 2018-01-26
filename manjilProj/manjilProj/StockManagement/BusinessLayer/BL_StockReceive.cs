﻿using System;
using System.Collections.Generic;
using System.Text;
using Entity;
using StockManagement.DataLayer;
using System.Linq;
using Entity.Model;
using Entity.ViewModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StockManagement.BusinessLayer
{
    public class BL_StockReceive
    {

        DL_StockReceive objDl = new DL_StockReceive();

        public IQueryable<PortfolioAccount> PopulatePortFolioList()
        {
            return objDl.PopulatePortFolioList();
        }

        public IQueryable<Stock> PopulateStockList()
        {
            return objDl.PopulateStockList();
        }

        public ServiceResult<StockReceiveMastVM> CreateStockReceive(StockReceiveMastVM stockReceiveMastVM)
        {
            StockReceiveMast stockReceiveMast = new StockReceiveMast();
            try
            {
                var isDate = DateTime.Parse(stockReceiveMastVM.ValueDate.ToString());
            }
            catch
            {
                return new ServiceResult<StockReceiveMastVM>()
                {
                    Data = null,
                    Message = "Warning -! Invalid OwnerShip Date..!!!",
                    Status = ResultStatus.Failed
                };
            }


            stockReceiveMast.EntryDate = System.DateTime.Now.Date;
            stockReceiveMast.EntryUserID = 1;
            stockReceiveMast.PortfolioAcId = stockReceiveMastVM.PortfolioAcId;
            stockReceiveMast.ValueDate = stockReceiveMastVM.ValueDate;
            stockReceiveMast.Remarks = stockReceiveMastVM.Remarks;
            stockReceiveMast.StockReceiveDetls = stockReceiveMastVM.StockRecieveDetlVM.Select(a => new StockReceiveDetl
            {
                StockId = a.StockId,
                OwnershipDate = a.OwnershipDate,
                Qty = a.Qty,
                Rate = a.Rate
            }).ToList();

            var x = objDl.CreateStockReceive(stockReceiveMast);
            return new ServiceResult<StockReceiveMastVM>()
            {
                Data = null,
                Message = x.Message,
                Status = x.Status
            };
        }

        public List<StockReceiveMastVM> PopulateIndexList()
        {
            List<StockReceiveMastVM> stockReceiveVM = objDl.getList().Result.Select(a => new StockReceiveMastVM
            {
                Id = a.Id,
                EntryDate = a.EntryDate,
                EntryUserID = a.EntryUserID,
                PortfolioAcId = a.PortfolioAcId,
                PortfolioAcName = a.PortfolioAccounts.AccountName,
                Remarks = a.Remarks,
                ValueDate = a.ValueDate,
                //StockRecieveDetlVM = a.StockReceiveDetls.Select(a => new StockRecieveDetlVM {
                //    Id=
                //});


            }).ToList();
            return stockReceiveVM;
        }

        public async Task<StockReceiveMastVM> GetListbyId(int? id)
        {
            StockReceiveMast stockReceiveMast = await objDl.getList().Result.Include(b => b.PortfolioAccounts).Include(a => a.StockReceiveDetls).ThenInclude(x => x.Stocks).Where(m => m.Id == id).FirstOrDefaultAsync();
            StockReceiveMastVM stockReceiveVM = new StockReceiveMastVM
            {
                Id = stockReceiveMast.Id,
                EntryDate = stockReceiveMast.EntryDate,
                EntryUserID = stockReceiveMast.EntryUserID,
                PortfolioAcId = stockReceiveMast.PortfolioAcId,
                PortfolioAcName = stockReceiveMast.PortfolioAccounts.AccountName,
                Remarks = stockReceiveMast.Remarks,
                ValueDate = stockReceiveMast.ValueDate,
                StockRecieveDetlVM = stockReceiveMast.StockReceiveDetls.Select(a => new StockRecieveDetlVM
                {
                    MastId = a.MastId,
                    Id = a.Id,
                    StockId = a.StockId,
                    Qty = a.Qty,
                    Rate = a.Rate,
                    StockName = a.Stocks.StockName,
                    OwnershipDate = a.OwnershipDate
                }).ToList()
            };
            return stockReceiveVM;
        }
    }
}