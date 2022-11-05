using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkingWithEfCore.Entities;

namespace WorkingWithEfCore.Database.Configurations
{
    //вместо прописывания конфигураций в OnModelCreating d dbcontext создаем отдельно класс в 
    //фолдер configurations унаследованный от IEntityTypeConfiguration<T Entity>

    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            //настраиваем связть один ко многим (адреса к юзерам, у юзера один адрес)
            builder.HasOne(q => q.Users)
                //Address имеет ссылку на энтити тайп ЮЗЕРС с помощью HasOne
                .WithMany(q => q.Addresses)

                .HasForeignKey(q => q.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            //v HasForeignKey передается объхект класса Addresses

            //connectimsya to table Address:
            //(если перед создание миграции забыли добавить dbSet as we did in this project by accident)
            //если бы подключили dbset Addresses то не нужно было этого делать:
            builder.ToTable("Address");
        }
    }
}