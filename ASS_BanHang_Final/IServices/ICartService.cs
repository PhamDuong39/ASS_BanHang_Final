using ASS_BanHang_Final.Models;
namespace ASS_BanHang_Final.IServices
{
    public interface ICartService
    {
        public bool CreateCart(Cart obj);
        public bool UpdateCart(Cart obj);
        public bool DeleteCart(Guid IdCart);
        public List<Cart> GetAllCarts();
        public List<Cart> GetCartsByDecs(string Decs);
        public Cart GetCartById(Guid IdCart);
    }
}
