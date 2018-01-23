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
            return View();
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
            return View();
        }

        // POST: Stock/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}