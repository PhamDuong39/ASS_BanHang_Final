using ASS_BanHang_Final.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ASS_BanHang_Final.Configurations
{
    public class BillDetailConfiguration : IEntityTypeConfiguration<BillDetail>
    {
        public void Configure(EntityTypeBuilder<BillDetail> builder)
        {
            //builder.ToTable("HoaDon"); // Đặt tên bảng
            builder.HasKey(p => p.Id); // Set khóa chính
            // Cấu hình cho thuộc tính
            builder.Property(p => p.Quantity).HasColumnType("int");
            builder.Property(p => p.Price).HasColumnType("int").IsRequired(); // int not null
            builder.HasOne(p => p.Bill).WithMany(c => c.BillDetails).HasForeignKey(l => l.IdHD);
            builder.HasOne(p => p.Product).WithMany(c => c.BillDetails).HasForeignKey(p => p.IdSP);
        }
    }
}
