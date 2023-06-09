using ASS_BanHang_Final.IServices;
using ASS_BanHang_Final.Models;

namespace ASS_BanHang_Final.Services
{
    public class BillDetailService : IBillDetailService
    {
        private ASS_BanHang_DbContext DbContext;
        public BillDetailService()
        {
            DbContext = new ASS_BanHang_DbContext();
        }
        public bool CreateBillDetail(BillDetail obj)
        {
            try
            {
                BillDetail billDTCreate = new BillDetail();
                billDTCreate.Id = obj.Id;
                billDTCreate.IdHD = obj.IdHD;
                billDTCreate.IdSP = obj.IdSP;
                billDTCreate.Quantity = obj.Quantity;
                billDTCreate.Price = obj.Price;

                DbContext.BillDetailss.Add(billDTCreate);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteBillDetail(Guid BillDetailId)
        {
            try
            {
                var obj = DbContext.BillDetailss.FirstOrDefault(p => p.Id == BillDetailId);
                DbContext.BillDetailss.Remove(obj);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<BillDetail> GetAllBillDetail()
        {
            return DbContext.BillDetailss.ToList();
        }

        public BillDetail GetBillDetailById(Guid BillDetailId)
        {
            return GetAllBillDetail().FirstOrDefault(p => p.Id == BillDetailId);
        }

        public List<BillDetail> GetBillDetailByIdBill(Guid IdBill)
        {
            return GetAllBillDetail().Where(p => p.IdHD == IdBill).ToList();
        }

        public bool UpdateBillDetail(BillDetail obj)
        {
            try
            {
                var BillDetailUpdated = DbContext.BillDetailss.FirstOrDefault(p => p.Id == obj.Id);
                BillDetailUpdated.IdHD = obj.IdHD;
                BillDetailUpdated.IdSP = obj.IdSP;
                BillDetailUpdated.Quantity = obj.Quantity;
                BillDetailUpdated.Price = obj.Price;
                DbContext.BillDetailss.Update(BillDetailUpdated);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<BillDetail> GetBillDetailsByIdHD(Guid IdHD)
        {
            var lst = DbContext.BillDetailss.Where(p => p.IdHD == IdHD).ToList();
            return lst;
        }
    }
}
