using Microsoft.AspNetCore.Mvc;
using Services.ExpenseCategoryManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class ExpenseCategoryController : Controller
    {
        private IExpenseCategoryService _expenseCategoryService;
        public ExpenseCategoryController(IExpenseCategoryService expenseCategoryService)
        {
            _expenseCategoryService = expenseCategoryService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddorUpdate(VmExpenseCategory dto)
        {
            try
            {
                var UserId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (!_expenseCategoryService.IsUnique(dto.Name))
                {
                    var data = _expenseCategoryService.GetById(dto.Id);
                    if (data != null)
                    {
                        var result = await _expenseCategoryService.UpdateAsync(dto, UserId);
                        if (result > 0)
                            TempData["AddSuccessMessage"] = "Updated Successfully";
                    }
                    else
                    {
                        var result = await _expenseCategoryService.AddAsync(dto, UserId);
                        if (result > 0)
                            TempData["AddSuccessMessage"] = "Added Successfully";
                    }
                }
                else
                {
                    TempData["NotUniqueMessage"] = "Already Exists";
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
            var result = await _expenseCategoryService.GetBetween(param.start, param.length, search, out totalLength);

            return Json(new
            {
                iTotalRecords = result.Count(),
                iTotalDisplayRecords = _expenseCategoryService.GetAll().Count(),
                data = result,
            });
        }
        [HttpGet]
        public ActionResult Get(int id)
        {
            var temp = new VmExpenseCategory();
            var data = _expenseCategoryService.GetById(id);

            //keep data from db to vm and returning as json to show in fields while editing
            temp.Id = data.Id;
            temp.Name = data.Name;
            return Json(temp);
        }
        public ActionResult Delete(int id)
        {
            //if (!_expenseCategoryService.HasChild(id))
            //{

            //}
            //else
            //    return NotFound();
            if (_expenseCategoryService.Delete(id))
                return Json("success");
            else
                return NotFound();
        }
    }
}
