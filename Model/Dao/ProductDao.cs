
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ViewModel;
using PagedList;

namespace Model.Dao
{
    public class ProductDao
    {
        OnlineShopDbContext db = null;
        public ProductDao()
        {
            db = new OnlineShopDbContext();
        }

        public IEnumerable<ProductViewModel> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<ProductViewModel> model = from a in db.Products
                                                 join b in db.ProductCategories
                                                 on a.CategoryID equals b.ID
                                                 select new ProductViewModel()
                                                 {
                                                     CateMetaTitle = b.MetaTitle,
                                                     CateName = b.Name,                                                  
                                                     Status = b.Status,
                                                     ID = a.ID,
                                                     Name = a.Name,
                                                     Code = a.Code,
                                                     Price = a.Price,
                                                     Quantity = a.Quantity
                                                 };
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.CateName.Contains(searchString));
            }

            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }

        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        public List<Product> ListByCategoryId(long categoryID, ref int totalRecord, int page = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.CategoryID == categoryID).Count();
            var model = db.Products.Where(x => x.CategoryID == categoryID);
            //var model = from a in db.Products
            //            join b in db.ProductCategories
            //            on a.CategoryID equals b.ID
            //            where a.CategoryID == categoryID
            //            select new ProductViewModel()
            //            {
            //                CateMetaTitle = b.MetaTitle,
            //                CateName = b.Name,
            //                CreatedDate = a.CreatedDate,
            //                ID = a.ID,
            //                Images = a.Image,
            //                Name = a.Name,
            //                MetaTitle = a.MetaTitle,
            //                Price = a.Price
            //            };
            model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }

        public List<Product> ListFeatureProduct(int top)
        {
            return db.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        public List<Product> ListRelatedProducts(long productId)
        {
            var product = db.Products.Find(productId);
            return db.Products.Where(x => x.ID != productId && x.CategoryID == product.CategoryID).ToList();
        }

        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }
    }
}
