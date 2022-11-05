using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using WorkingWithEfCore.Entities;
using WorkingWithEfCore.Database.Configurations;

namespace WorkingWithEfCore.Database
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {
            
        }
        //dobavili sushnast'v dbcontext:
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (optionsBuilder.IsConfigured == false)
            {
                var connectionString = "Server = localhost; Database = WorkingWithEfCore; Trusted_Connection = True";

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        //применяем конфигурацию, переопределяем, т.к определили ее ранее в  AddressCOnfiguration
        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

        }
    }
}
