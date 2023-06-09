using ASS_BanHang_Final.Models;
using ASS_BanHang_Final.ViewModels;

namespace ASS_BanHang_Final.IServices
{
    public interface IBillService
    {
        public bool CreateBill(Bill obj);
        public bool UpdateBill(Bill obj);
        public bool DeleteBill(Guid BillId);
        public List<Bill> GetAllBill();
        public Bill GetBillById(Guid BillId);
        public List<Bill> GetBillByDate(DateTime dateFill);

        

        public List<Bill> GetAllBillByUserId(Guid UserId);

        public List<BillHistoryViewModel> GetAllBillHistory(Guid UserID);


    }
}
