using ASS_BanHang_Final.IServices;
using ASS_BanHang_Final.Models;

namespace ASS_BanHang_Final.Services
{
    public class CartService : ICartService
    {
        private ASS_BanHang_DbContext DbContext;
        public CartService()
        {
            DbContext = new ASS_BanHang_DbContext();
        }
        public bool CreateCart(Cart obj)
        {
            try
            {

                DbContext.Carts.Add(obj);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCart(Guid IdCart)
        {
            try
            {
                var obj = DbContext.Carts.ToList().FirstOrDefault(p => p.UserId == IdCart);
                DbContext.Carts.Remove(obj);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Cart> GetAllCarts()
        {
            return DbContext.Carts.ToList();
        }

        public Cart GetCartById(Guid IdCart)
        {
            return DbContext.Carts.ToList().FirstOrDefault(p => p.UserId == IdCart);
        }

        public List<Cart> GetCartsByDecs(string Decs)
        {
            return DbContext.Carts.ToList().Where(p => p.Description.Contains(Decs)).ToList();
        }

        public bool UpdateCart(Cart obj)
        {
            try
            {
                var CartUpdated = DbContext.Carts.ToList().FirstOrDefault(p => p.UserId == obj.UserId);
                CartUpdated.Description = obj.Description;
                DbContext.Carts.Update(CartUpdated);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
