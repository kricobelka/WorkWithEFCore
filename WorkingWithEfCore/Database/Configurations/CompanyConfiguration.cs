using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkingWithEfCore.Entities;

namespace WorkingWithEfCore.Database.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(q => q.CompanyId);
            builder.Property(q => q.CompanyId).IsRequired(false);
            
            builder.HasMany(q => q.Users)
                .WithOne(u => u.Company)
                .HasForeignKey(q => q.CompanyId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
