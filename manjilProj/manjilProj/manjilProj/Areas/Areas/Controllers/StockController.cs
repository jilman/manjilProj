using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entity.Model;
using Entity.Connection;
using StockManagement.BusinessLayer;

namespace manjilProj.Areas.Areas.Controllers
{
    [Area("Areas")]
    public class StockController : Controller
    {
        private readonly ConnectionDB _Context;
        public StockController(ConnectionDB context)
        {
            _Context = context;
        }
        // GET: Stock
        public ActionResult Index()
        {
            //var StockReceiveMast = _context.StockReceiveMast.Include(s => s.PortfolioAccounts);
            //List<StockReceiveMastVM> stockReceiveVM = await _context.StockReceiveMast.Select(a => new StockReceiveMastVM
            //{
            //    Id = a.Id,
            //    EntryDate = a.EntryDate,
            //    EntryUserID = a.EntryUserID,
            //    PortfolioAcId = a.PortfolioAcId,
            //    PortfolioAcName = a.PortfolioAccounts.AccountName,
            //    Remarks = a.Remarks,
            //    ValueDate = a.ValueDate

            ////}).ToListAsync();
            BL_Stock objBl = new BL_Stock();
            List<Stock> lstStock = objBl.PopulateList().ToList();

            return View(lstStock);
        }

        // GET: Stock/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Stock/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stock/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Stock stock)
        {
            ServiceResult<Stock> result = new ServiceResult<Stock>(); 
            try
            {
                BL_Stock objBl = new BL_Stock();
                // TODO: Add insert logic here
                stock.EntryDate = DateTime.Now;
                stock.EntryUserID = 1;
                if (ModelState.IsValid)
                {
                    result = objBl.CreateStock(stock);
                        return Json(new ServiceResult<Stock>() { Data = null, Message = result.Message, Status = result.Status });
                }
                else
                {
                    return Json(new ServiceResult<Stock>() { Data = null, Message =ModelState.Values.SelectMany(a=>a.Errors).Select(b=>b.ErrorMessage).ToString() , Status = ResultStatus.Failed });
                }

                
               // return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return Json(new ServiceResult<Stock>() { Data = null, Message =ex.Message, Status = ResultStatus.Exception });
            }
        }

        // GET: Stock/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Stock/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Stock/Delete/5
        public ActionResult Delete(int id)
        {
            BL_Stock objBl = new BL_Stock();
            Stock stock = objBl.GetListbyId(id);

            return View(stock);
            
        }

        // POST: Stock/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceResult<Stock> result = new ServiceResult<Stock>();
            try
            {
                // TODO: Add delete logic here
                BL_Stock objBl = new BL_Stock();
                result = objBl.DeleteStock(id);
                return Json(new ServiceResult<Stock>() { Data = null, Message = result.Message, Status = result.Status });
            }
            catch(Exception ex)
            {
                return Json(new ServiceResult<Stock>() { Data = null, Message = ex.Message, Status = ResultStatus.Exception });
            }
        }
    }
}