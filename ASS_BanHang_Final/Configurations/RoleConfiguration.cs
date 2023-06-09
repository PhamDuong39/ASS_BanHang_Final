using ASS_BanHang_Final.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ASS_BanHang_Final.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Description).HasColumnType("nvarchar(100)");
            builder.Property(p => p.RoleName).HasColumnType("nvarchar(100)");
        }
    }
}
