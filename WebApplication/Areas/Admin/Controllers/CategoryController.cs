using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductCategory category)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();
                if (dao.CheckProductCategoryName(category.Name))
                {
                    ModelState.AddModelError("", "Duplicate product category name!");
                }
                
                else
                {
                    long id = dao.Insert(category);
                    if (id > 0)
                    {
                        TempData["SuccessMessage"] = "Product Category " + category.Name + " Created Successfully!";
                        return RedirectToAction("Index", "Product");
                    }
                }
            }
            return View();
        }
    }
}