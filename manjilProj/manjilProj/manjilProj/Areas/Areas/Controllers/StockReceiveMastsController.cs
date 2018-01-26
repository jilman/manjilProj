using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entity.Connection;
using Entity.Model;
using Entity.ViewModel;
using StockManagement.BusinessLayer;

namespace manjilProj.Areas.Areas.Controllers
{
    [Area("Areas")]
    public class StockReceiveMastsController : Controller
    {
        private readonly ConnectionDB _context;

        public StockReceiveMastsController(ConnectionDB context)
        {
            _context = context;
        }

        // GET: Areas/StockReceiveMasts
        public IActionResult Index()
        {
            BL_StockReceive objBl = new BL_StockReceive();
            List<StockReceiveMastVM> lstStockReceive = objBl.PopulateIndexList();

            return View(lstStockReceive);
        }

        // GET: Stock/StockReceiveMasts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var stockReceiveMast = await _context.StockReceiveMast.Include(b => b.PortfolioAccounts).Include(a => a.StockReceiveDetls).ThenInclude(x => x.Stocks).Where(m => m.Id == id).FirstOrDefaultAsync();

            //StockReceiveMastVM stockModelVM = new StockReceiveMastVM
            //{
            //    Id = stockReceiveMast.Id,
            //    ValueDate = stockReceiveMast.ValueDate,
            //    PortfolioAcId = stockReceiveMast.PortfolioAcId,
            //    PortfolioAcName = stockReceiveMast.PortfolioAccounts.AccountName,
            //    Remarks = stockReceiveMast.Remarks,
            //    StockRecieveDetlVM = stockReceiveMast.StockReceiveDetls.Select(a => new StockRecieveDetlVM
            //    {
            //        MastId = a.MastId,
            //        Id = a.Id,
            //        StockId = a.StockId,
            //        Qty = a.Qty,
            //        Rate = a.Rate,
            //        StockName = a.Stocks.StockName,
            //        OwnershipDate = a.OwnershipDate

            //    }).ToList()
            //};
            //if (stockReceiveMast == null)
            //{
            //    return NotFound();
            //}
            //ViewBag.PortfolioAcId = new SelectList(_context.PortfolioAccount, "Id", "AccountName");
            //ViewBag.StockId = new SelectList(_context.Stock, "Id", "StockName");

            //return View(stockModelVM);
            BL_StockReceive objBl = new BL_StockReceive();
            StockReceiveMastVM stockReceive = await objBl.GetListbyId(id);
            return View(stockReceive);

        }

        // GET: Stock/StockReceiveMasts/Create
        public IActionResult Create()
        {

            BL_StockReceive objBl = new BL_StockReceive();
            ViewBag.PortfolioAcId = new SelectList(objBl.PopulatePortFolioList(), "Id", "AccountName");
            ViewBag.StockId = new SelectList(objBl.PopulateStockList(), "Id", "StockName");
            return View();

        }

        // POST: Stock/StockReceiveMasts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockReceiveMastVM stockReceiveMastVM)
        {

            ServiceResult<StockReceiveMastVM> result = new ServiceResult<StockReceiveMastVM>();
            try
            {
                BL_StockReceive objBl = new BL_StockReceive();

                stockReceiveMastVM.EntryDate = System.DateTime.Now.Date;
                stockReceiveMastVM.EntryUserID = 1;
                if (ModelState.IsValid)
                {
                    result = objBl.CreateStockReceive(stockReceiveMastVM);
                    return Json(new ServiceResult<Stock>() { Data = null, Message = result.Message, Status = result.Status });
                }
                else
                {
                    return Json(new ServiceResult<Stock>() { Data = null, Message = ModelState.Values.SelectMany(a => a.Errors).Select(b => b.ErrorMessage).ToString(), Status = ResultStatus.Failed });
                }


                // return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Json(new ServiceResult<Stock>() { Data = null, Message = ex.Message, Status = ResultStatus.Exception });
            }
        }

        // GET: Stock/StockReceiveMasts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockReceiveMast = await _context.StockReceiveMast.Include(b => b.PortfolioAccounts).Include(a => a.StockReceiveDetls).ThenInclude(x => x.Stocks).Where(m => m.Id == id).FirstOrDefaultAsync();

            StockReceiveMastVM stockModelVM = new StockReceiveMastVM
            {
                Id = stockReceiveMast.Id,
                ValueDate = stockReceiveMast.ValueDate,
                PortfolioAcId = stockReceiveMast.PortfolioAcId,
                PortfolioAcName = stockReceiveMast.PortfolioAccounts.AccountName,
                Remarks = stockReceiveMast.Remarks,
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
            if (stockReceiveMast == null)
            {
                return NotFound();
            }
            ViewBag.PortfolioAcId = new SelectList(_context.PortfolioAccount, "Id", "AccountName");
            ViewBag.StockId = new SelectList(_context.Stock, "Id", "StockName");

            return View(stockModelVM);
        }

        // POST: Stock/StockReceiveMasts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StockReceiveMastVM data)
        {
            if (id != data.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                StockReceiveMast stockReceiveMast = new StockReceiveMast
                {
                    Id = data.Id,
                    EntryDate = DateTime.Now,
                    EntryUserID = 1,
                    Remarks = data.Remarks,
                    ValueDate = data.ValueDate,
                    PortfolioAcId = data.PortfolioAcId,
                    StockReceiveDetls = data.StockRecieveDetlVM.Where(a => a.Flag == Flag.New).Select(a => new StockReceiveDetl
                    {
                        Id = a.Id,
                        MastId = a.MastId,
                        OwnershipDate = a.OwnershipDate,
                        Qty = a.Qty,
                        Rate = a.Rate,
                        StockId = a.StockId
                    }).ToList()

                };

                List<StockReceiveDetl> lstStockDetl = data.StockRecieveDetlVM.Where(a => a.Flag == Flag.Deleted).Select(a => new StockReceiveDetl
                {
                    Id = a.Id,
                    MastId = a.MastId,
                    OwnershipDate = a.OwnershipDate,
                    Qty = a.Qty,
                    Rate = a.Rate,
                    StockId = a.StockId
                }).ToList();

                _context.RemoveRange(lstStockDetl);
                await _context.SaveChangesAsync();
                _context.Update(stockReceiveMast);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewBag.PortfolioAcId = new SelectList(_context.PortfolioAccount, "Id", "AccountName", stockReceiveMastVM.PortfolioAcId);
            return RedirectToAction(nameof(Index));
        }

        // GET: Stock/StockReceiveMasts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockReceiveMast = await _context.StockReceiveMast.Include(b => b.PortfolioAccounts).Include(a => a.StockReceiveDetls).ThenInclude(x => x.Stocks).Where(m => m.Id == id).FirstOrDefaultAsync();

            StockReceiveMastVM stockModelVM = new StockReceiveMastVM
            {
                Id = stockReceiveMast.Id,
                ValueDate = stockReceiveMast.ValueDate,
                PortfolioAcId = stockReceiveMast.PortfolioAcId,
                PortfolioAcName = stockReceiveMast.PortfolioAccounts.AccountName,
                Remarks = stockReceiveMast.Remarks,
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
            if (stockReceiveMast == null)
            {
                return NotFound();
            }
            ViewBag.PortfolioAcId = new SelectList(_context.PortfolioAccount, "Id", "AccountName");
            ViewBag.StockId = new SelectList(_context.Stock, "Id", "StockName");

            return View(stockModelVM);
        }

        // POST: Stock/StockReceiveMasts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockReceiveMast = await _context.StockReceiveMast.SingleOrDefaultAsync(m => m.Id == id);
            _context.StockReceiveMast.Remove(stockReceiveMast);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockReceiveMastExists(int id)
        {
            return _context.StockReceiveMast.Any(e => e.Id == id);
        }
    }
}
