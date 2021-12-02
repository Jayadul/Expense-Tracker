using Microsoft.AspNetCore.Mvc;
using Services.ExpenseManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class ExpenseController : Controller
    {
        private IExpenseService _expenseService;
        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddorUpdate(VmExpense dto)
        {
            try
            {
                var UserId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    var data = _expenseService.GetById(dto.Id);
                    if (data != null)
                    {
                        var result = await _expenseService.UpdateAsync(dto, UserId);
                        if (result > 0)
                            TempData["AddSuccessMessage"] = "Updated Successfully";
                    }
                    else
                    {
                        var result = await _expenseService.AddAsync(dto, UserId);
                        if (result > 0)
                            TempData["AddSuccessMessage"] = "Added Successfully";
                    }               
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<IActionResult> AjaxDataListAsync(JQueryDataTableParamModel param)
        {
            string search = HttpContext.Request.Query["search[value]"].ToString();
            int totalLength = 0;
            var result = await _expenseService.GetBetween(param.start, param.length, search, out totalLength);
            return Json(new
            {
                iTotalRecords = result.Count(),
                iTotalDisplayRecords = _expenseService.GetAll().Count(),
                data = result,
            });
        }
        [HttpGet]
        public ActionResult Get(int id)
        {
            var temp = new VmExpense();
            var data = _expenseService.GetById(id);

            //keep data from db to vm and returning as json to show in fields while editing
            temp.Id = data.Id;
            temp.Ammount = data.Ammount;
            temp.Date = data.Date.ToString("dd mm yyyy");
            temp.ExpenseCategoryId = data.ExpenseCategoryId;
            return Json(temp);
        }
        public ActionResult Delete(int id)
        {
                if (_expenseService.Delete(id))
                    return Json("success");
                else
                    return NotFound();
        }
    }
}
