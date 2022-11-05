using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkingWithEfCore.Entities;

namespace WorkingWithEfCore.Database.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasOne(q => q.User)
                .WithOne(q => q.Contact)
                .HasForeignKey<User>(q => q.ContactId);
        }
    }
}
