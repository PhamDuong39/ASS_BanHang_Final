using ASS_BanHang_Final.IServices;
using ASS_BanHang_Final.Models;
using ASS_BanHang_Final.ViewModels;
using System.Collections.Generic;

namespace ASS_BanHang_Final.Services
{
    public class BillService : IBillService
    {
        private ASS_BanHang_DbContext DbContext;
        public BillService()
        {
            DbContext = new ASS_BanHang_DbContext();
        }

        public bool CreateBill(Bill obj)
        {
            try
            {
                Bill billCreate = new Bill();
                billCreate.Id = obj.Id;
                billCreate.CreateDate = DateTime.Now;
                billCreate.UserID = obj.UserID;
                billCreate.Status = 1;
                billCreate.Address = obj.Address;
                billCreate.SDT = obj.SDT;
                DbContext.Bills.Add(billCreate);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteBill(Guid BillId)
        {
            try
            {
                var obj = DbContext.Bills.ToList().FirstOrDefault(p => p.Id == BillId);
                DbContext.Bills.Remove(obj);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Bill> GetAllBill()
        {
            return DbContext.Bills.ToList();
        }

        public List<Bill> GetBillByDate(DateTime dateFill)
        {
            return GetAllBill().Where(p => p.CreateDate == dateFill).ToList();
        }

        public Bill GetBillById(Guid BillId)
        {
            return GetAllBill().FirstOrDefault(p => p.Id == BillId);
        }

        public bool UpdateBill(Bill obj)
        {
            try
            {
                var billUpdated = DbContext.Bills.ToList().FirstOrDefault(p => p.Id == obj.Id);
                billUpdated.CreateDate = obj.CreateDate;
                billUpdated.UserID = obj.UserID;
                billUpdated.Status = obj.Status;
                DbContext.Bills.Update(billUpdated);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Bill> GetAllBillByUserId(Guid UserId)
        {
            var bill = DbContext.Bills.ToList().Where(p => p.UserID == UserId);
            return bill.ToList();
        }

        public List<BillHistoryViewModel> GetAllBillHistory(Guid UserID)
        {
            var allBill = DbContext.Bills.ToList().Where(p => p.UserID == UserID);

            List<BillHistoryViewModel> listReturn = new List<BillHistoryViewModel>();
            foreach (var item in allBill)
            {
                BillHistoryViewModel billHistory = new BillHistoryViewModel();
                billHistory.IdBill = item.Id;
                billHistory.CreateDate = item.CreateDate;
                billHistory.Status = item.Status;
                billHistory.Address = item.Address;
                billHistory.SDT = item.SDT;
                billHistory.listBillDeatail = DbContext.BillDetailss.Where(p => p.IdHD == item.Id).ToList();
                listReturn.Add(billHistory);
            }
            return listReturn;
        }
    }
}
