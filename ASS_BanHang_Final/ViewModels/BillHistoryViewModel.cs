using ASS_BanHang_Final.Models;

namespace ASS_BanHang_Final.ViewModels
{
    public class BillHistoryViewModel
    {
        public Guid IdBill { get; set; }
        public DateTime CreateDate { get; set; }
        
        public int Status { get; set; }
        public string Address { get; set; }
        public string SDT { get; set; }

        public List<BillDetail> listBillDeatail { get; set; }

    }
}
