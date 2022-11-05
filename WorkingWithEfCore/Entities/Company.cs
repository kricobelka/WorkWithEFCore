namespace WorkingWithEfCore.Entities
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
