using ASS_BanHang_Final.IServices;
using ASS_BanHang_Final.Models;

namespace ASS_BanHang_Final.Services
{
    public class ProductService : IProductService
    {
        private ASS_BanHang_DbContext DbContext;
        public ProductService()
        {
            DbContext = new ASS_BanHang_DbContext();
        }

        public bool CreateProduct(Product obj)
        {
            try
            {
                var allProduct = DbContext.Products.ToList().Where(p => p.Name == obj.Name);
                if (allProduct.Any())
                {
                    return false;
                }
                else
                {
                    DbContext.Products.Add(obj);
                    DbContext.SaveChanges();
                    return true;
                }
                
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteProduct(Guid Idobj)
        {
            try
            {
                var obj = DbContext.Products.ToList().FirstOrDefault(p => p.Id == Idobj);
                // var obj = DbContext.Products.Find(IdObj);
                DbContext.Products.Remove(obj);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Product> GetAllProduct()
        {
            return DbContext.Products.ToList();
        }

        public Product GetProductById(Guid Idobj)
        {
            var obj = DbContext.Products.Find(Idobj);
            return obj;
        }

        public List<Product> GetProductByName(string Name)
        {
            var obj = DbContext.Products.ToList().Where(p => p.Name.ToLower().Contains(Name.ToLower())).ToList();
            return obj;
        }

        public bool UpdateProduct(Product obj)
        {
            try
            {
                var NewProduct = DbContext.Products.Find(obj.Id);
                NewProduct.Name = obj.Name;
                NewProduct.Description = obj.Description;
                NewProduct.Price = obj.Price;
                NewProduct.Supplier = obj.Supplier;
                NewProduct.AvailableQuantity = obj.AvailableQuantity;
                NewProduct.Status = obj.Status;

                var allProduct = DbContext.Products.ToList().Where(p => p.Name == obj.Name);
                if (NewProduct.AvailableQuantity < 0 || NewProduct.Description.Trim() == "")
                {
                    return false;
                }
                else if (allProduct.Any())
                {
                    return false;
                }
                else
                {
                    DbContext.Products.Update(NewProduct);
                    DbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
