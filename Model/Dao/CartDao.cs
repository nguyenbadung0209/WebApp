using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CartDao
    {
        OnlineShopDbContext db = null;

        public CartDao()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(Cart entity)
        {
            db.Carts.Add(entity);
            db.SaveChanges();
            return entity.CustomerID;
        }

        public List<Cart> ListByCustomerId(long id)
        {
            return db.Carts.Where(x => x.CustomerID == id).OrderByDescending(x => x.ID).ToList();
        }
        public bool UpdateQuantity(Cart entity)
        {
            try
            {
                var cart = db.Carts.SingleOrDefault(x => x.CustomerID == entity.CustomerID && x.ProductID == entity.ProductID);
                cart.Quantity = entity.Quantity;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Delete(Cart entity)
        {
            try
            {
                var cart = db.Carts.SingleOrDefault(x => x.CustomerID == entity.CustomerID && x.ProductID == entity.ProductID);
                db.Carts.Remove(cart);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteAll(List<Cart> entity)
        {
            try
            {
                foreach (var item in entity)
                {
                    var cart = db.Carts.Find(item.ID);
                    db.Carts.Remove(cart);                   
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
