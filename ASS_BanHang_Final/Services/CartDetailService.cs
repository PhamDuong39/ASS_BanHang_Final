using ASS_BanHang_Final.IServices;
using ASS_BanHang_Final.Models;

namespace ASS_BanHang_Final.Services
{
    public class CartDetailService : ICartDetailService
    {
        private ASS_BanHang_DbContext DbContext;
        public CartDetailService()
        {
            DbContext = new ASS_BanHang_DbContext();
        }
        public bool CreateCartDetail(CartDetail obj)
        {
            try
            {
              
                DbContext.CartDetails.Add(obj);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCartDetail(Guid IdCartDetail)
        {
            try
            {
                var obj = DbContext.CartDetails.FirstOrDefault(p => p.Id == IdCartDetail);
                DbContext.CartDetails.Remove(obj);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<CartDetail> GetAllCartDetail()
        {
            return DbContext.CartDetails.ToList();
        }

        public CartDetail GetCartDetailById(Guid Id)
        {
            return GetAllCartDetail().FirstOrDefault(p => p.Id == Id);
        }

        public List<CartDetail> GetCartDetailByName(string Name)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCartDetail(CartDetail obj)
        {
            try
            {
                var CartDetailUpdated = DbContext.CartDetails.ToList().FirstOrDefault(p => p.Id == obj.Id);
                CartDetailUpdated.UserId = obj.UserId;
                CartDetailUpdated.IdSp = obj.IdSp;
                CartDetailUpdated.Quantity = obj.Quantity;
                DbContext.CartDetails.Update(CartDetailUpdated);
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
