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
        public async Task<IActionResult> Index()
        {
            var StockReceiveMast = _context.StockReceiveMast.Include(s => s.PortfolioAccounts);
            List<StockReceiveMastVM> stockReceiveVM = await _context.StockReceiveMast.Select(a => new StockReceiveMastVM
            {
                Id = a.Id,
                EntryDate = a.EntryDate,
                EntryUserID = a.EntryUserID,
                PortfolioAcId = a.PortfolioAcId,
                PortfolioAcName = a.PortfolioAccounts.AccountName,
                Remarks = a.Remarks,
                ValueDate = a.ValueDate

            }).ToListAsync();


            return View(stockReceiveVM);
        }

        // GET: Stock/StockReceiveMasts/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Stock/StockReceiveMasts/Create
        public IActionResult Create()
        {
            ViewBag.PortfolioAcId = new SelectList(_context.PortfolioAccount, "Id", "AccountName");
            ViewBag.StockId = new SelectList(_context.Stock, "Id", "StockName");
            return View();
        }

        // POST: Stock/StockReceiveMasts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockReceiveMastVM stockReceiveMastVM)
        {
            var stockReceiveMast = new StockReceiveMast();
            stockReceiveMast.EntryDate = System.DateTime.Now.Date;
            stockReceiveMast.EntryUserID = 1;
            if (ModelState.IsValid)
            {
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

                _context.Add(stockReceiveMast);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PortfolioAcId"] = new SelectList(_context.PortfolioAccount, "Id", "AccountName", stockReceiveMast.PortfolioAcId);
            return View(stockReceiveMast);
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
                    PortfolioAcId=data.PortfolioAcId,
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
