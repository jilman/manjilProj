using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entity.Model;
using StockManagement.BusinessLayer;

namespace manjilProj.Areas.Areas.Controllers
{
    [Area("Areas")]
    public class PortfolioController : Controller
    {
        // GET: Portfolio
        public ActionResult Index()
        {
            BL_Portfolio objBl = new BL_Portfolio();
            List<PortfolioAccount> lstPortfolio = objBl.PopulateList().ToList();

            return View(lstPortfolio);
            
        }

        // GET: Portfolio/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Portfolio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Portfolio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PortfolioAccount portfolio)
        {
            ServiceResult<PortfolioAccount> result = new ServiceResult<PortfolioAccount>();
            try
            {
                BL_Portfolio objBl = new BL_Portfolio();
                // TODO: Add insert logic here
                portfolio.EntryDate = DateTime.Now;
                portfolio.EntryUserID = 1;
                if (ModelState.IsValid)
                {
                    result = objBl.CreatePortfolio(portfolio);
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

        // GET: Portfolio/Edit/5
        public ActionResult Edit(int id)
        {
            BL_Portfolio objBl = new BL_Portfolio();
            PortfolioAccount portfolio = objBl.GetListbyId(id);
            return View(portfolio);
        }

        // POST: Portfolio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PortfolioAccount portfolio)
        {
            ServiceResult<PortfolioAccount> result = new ServiceResult<PortfolioAccount>();
            try
            {
                // TODO: Add delete logic here
                BL_Portfolio objBl = new BL_Portfolio();
                if (ModelState.IsValid)
                {
                    result = objBl.EditStock(portfolio);
                    return Json(new ServiceResult<Stock>() { Data = null, Message = result.Message, Status = result.Status });
                }
                else
                {
                    return Json(new ServiceResult<Stock>() { Data = null, Message = ModelState.Values.SelectMany(x => x.Errors).Select(a => a.ErrorMessage).ToString(), Status = result.Status });
                }
            }
            catch (Exception ex)
            {
                return Json(new ServiceResult<Stock>() { Data = null, Message = ex.Message, Status = ResultStatus.Exception });
            }
        }

        // GET: Portfolio/Delete/5
        public ActionResult Delete(int id)
        {
            BL_Portfolio objBl = new BL_Portfolio();
            PortfolioAccount portfolio = objBl.GetListbyId(id);

            return View(portfolio);
        }

        // POST: Portfolio/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceResult<PortfolioAccount> result = new ServiceResult<PortfolioAccount>();
            try
            {
                // TODO: Add delete logic here
                BL_Portfolio objBl = new BL_Portfolio();
                result = objBl.DeletePortfolio(id);
                return Json(new ServiceResult<Stock>() { Data = null, Message = result.Message, Status = result.Status });
            }
            catch (Exception ex)
            {
                return Json(new ServiceResult<Stock>() { Data = null, Message = ex.Message, Status = ResultStatus.Exception });
            }
        }
    }
}