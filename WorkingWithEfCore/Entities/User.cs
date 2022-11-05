namespace WorkingWithEfCore.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public int YearsUntilRetirement { get; set; }

        public int? CompanyId { get; set; }
        //navigation property (reference to COmpany):
        public Company Company { get; set; }


        public int ContactId { get; set; }
        public Contact Contact { get; set; }
        
        //помечаем что юзер имеет коллекцию адресов:
        public ICollection<Address> Addresses { get; set; }

    }
}
