using ASS_BanHang_Final.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASS_BanHang_Final.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(p => p.UserId);
            builder.Property(p => p.Description).HasMaxLength(1000).IsFixedLength().IsUnicode();// nvarchar(1000)

        }
    }
}
