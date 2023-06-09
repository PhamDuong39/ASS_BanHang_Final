namespace ASS_BanHang_Final.Models
{
    public class CartDetail
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid IdSp { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
        public virtual Cart Cart { get; set; }

        public int Price { get; set; }

        public CartDetail()
        {
            
        }

        public CartDetail(Product product)
        {
            Id = Guid.NewGuid();
            IdSp = product.Id;
            Price = product.Price;
            Quantity = 1;
        }

        public CartDetail(Product product, int soLuongThem)
        {
            Id = Guid.NewGuid();
            IdSp = product.Id;
            Price = product.Price;
            Quantity = soLuongThem;
        }
    }
}
