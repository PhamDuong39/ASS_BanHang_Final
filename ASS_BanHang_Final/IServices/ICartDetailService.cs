using ASS_BanHang_Final.Models;
namespace ASS_BanHang_Final.IServices
{
    public interface ICartDetailService
    {
        public bool CreateCartDetail(CartDetail obj);
        public bool UpdateCartDetail(CartDetail obj);
        public bool DeleteCartDetail(Guid IdCartDetail);
        public List<CartDetail> GetAllCartDetail();
        public List<CartDetail> GetCartDetailByName(string Name);
        public CartDetail GetCartDetailById(Guid Id);
    }
}
