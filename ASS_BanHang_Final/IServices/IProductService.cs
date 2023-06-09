using ASS_BanHang_Final.Models;
namespace ASS_BanHang_Final.IServices
{
    public interface IProductService
    {
        public bool CreateProduct(Product obj);
        public bool UpdateProduct(Product obj);
        public bool DeleteProduct(Guid Idobj);
        public List<Product> GetAllProduct();

        public Product GetProductById(Guid Idobj);
        public List<Product> GetProductByName(string Name);
    }
}
