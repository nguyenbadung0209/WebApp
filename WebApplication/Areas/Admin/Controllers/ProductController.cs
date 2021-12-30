using Model.Dao;
using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ProductViewModel cate = new ProductViewModel();
            var dao = new CategoryDao();
            cate.CateCollection = dao.ListAll();
            return View(cate);
        }
        [HttpPost]
        public ActionResult Create(ProductViewModel data)
        {
            var Catedao = new CategoryDao();
            data.CateCollection = Catedao.ListAll();
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                if (dao.CheckProductName(data.Name))
                {
                    ModelState.AddModelError("", "Duplicate product name!");
                }
                else if (dao.CheckProductCode(data.Code))
                {
                    ModelState.AddModelError("", "Duplicate product code!");
                }
                else if (data.Price == 0)
                {
                    ModelState.AddModelError("", "Please enter the price!");
                }
                else if (data.Quantity == 0)
                {
                    ModelState.AddModelError("", "Please enter the quantity!");
                }
                else
                {
                    var product = new Product();
                    product.Name = data.Name;
                    product.Code = data.Code;
                    product.Description = data.Description;
                    product.Price = data.Price;
                    product.Quantity = data.Quantity;
                    product.Status = data.Status;
                    product.CategoryID = data.CategoryID;
                    long id = dao.Insert(product);
                    if (id > 0)
                    {
                        TempData["SuccessMessage"] = "Product " + product.Name + " Created Successfully!";
                        return RedirectToAction("Index", "Product");
                    }
                }
            }
            return View(data);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            ProductViewModel cate = new ProductViewModel();
            cate.CategoryID = product.CategoryID;
            cate.Name = product.Name;
            cate.Code = product.Code;
            cate.Description = product.Description;
            cate.Price = product.Price;
            cate.Quantity = product.Quantity;
            cate.Status = product.Status;
            var dao = new CategoryDao();
            cate.CateCollection = dao.ListAll();
            return View(cate);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel data)
        {
            var Catedao = new CategoryDao();
            data.CateCollection = Catedao.ListAll();
            if (ModelState.IsValid)
            {                
                if (data.Price == 0)
                {
                    ModelState.AddModelError("", "Please enter the price!");
                }
                else if (data.Quantity == 0)
                {
                    ModelState.AddModelError("", "Please enter the quantity!");
                }
                else
                {
                    var product = new Product();
                    product.ID = data.ID;
                    product.Name = data.Name;
                    product.Code = data.Code;
                    product.Description = data.Description;
                    product.Price = data.Price;
                    product.Quantity = data.Quantity;
                    product.Status = data.Status;
                    product.CategoryID = data.CategoryID;
                    var dao = new ProductDao();
                    var result = dao.Update(product);
                    if (result) {
                        //SetAlert("Saved Successfully", "success");
                        TempData["SuccessMessage"] = "Product " + product.Name + " Saved Successfully";
                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Update product fail");
                    }
                }
            }
            return View(data);
        }

        [HttpPost]
        public JsonResult DeleteSelectedCheckbox(string[] data)
        {
            if (data != null)
            {
                foreach (var id in data)
                {
                    new ProductDao().Delete(Convert.ToInt32(id));
                }
            }
            else { return Json(new { status = false }); }

            return Json(new { status = true });
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductDao().ChangeStatus(id);
            return Json(new { status = result });
        }
    }
}