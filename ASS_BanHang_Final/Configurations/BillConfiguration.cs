using ASS_BanHang_Final.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ASS_BanHang_Final.Configurations
{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("Bill"); // Đặt tên bảng
            builder.HasKey(p => p.Id); // Set khóa chính
            // Cấu hình cho thuộc tính
            builder.Property(p => p.CreateDate).HasColumnType("Date");
            builder.Property(p => p.Status).HasColumnType("int").IsRequired(); // int not null
            builder.HasOne(p => p.User).WithMany(p => p.Bills).HasForeignKey(p => p.UserID).HasConstraintName("FK_Bill_User");
        }
    }
}
