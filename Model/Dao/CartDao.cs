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

        public Cart ViewDetail(long id) {
            return db.Carts.Find(id);            
        }


    }
}
