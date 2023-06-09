using ASS_BanHang_Final.Models;

namespace ASS_BanHang_Final.ViewModels
{
    public class CartDetailViewModel
    {
        //public Guid ProductId { get; set; }
        //public string ProductName { get; set; }
        //public int Quantity { get; set; }

        public List<CartDetail> cartItems { get; set; }
        
        // Gia hien thi o cuoi gio hang - gia chuan sau Sale,VAT
        public int GrandTotal { get; set; }
    }
}
