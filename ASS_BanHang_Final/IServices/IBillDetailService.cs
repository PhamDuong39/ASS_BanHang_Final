using ASS_BanHang_Final.Models;

namespace ASS_BanHang_Final.IServices
{
    public interface IBillDetailService
    {
        public bool CreateBillDetail(BillDetail obj);
        public bool DeleteBillDetail(Guid BillDetailId);
        public bool UpdateBillDetail(BillDetail obj);
        public List<BillDetail> GetAllBillDetail();
        public BillDetail GetBillDetailById(Guid BillDetailId);
        public List<BillDetail> GetBillDetailByIdBill(Guid IdBill);

        public List<BillDetail> GetBillDetailsByIdHD(Guid IdHD);
    }
}
