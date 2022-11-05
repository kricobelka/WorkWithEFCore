namespace WorkingWithEfCore.Entities
{
    public class Contact
    {
        public int ContactId { get; set; }
       
        public string PhoneNumber { get; set;}
        public string Email { get; set; }

        public int ?UserId { get; set; }
        public User User { get; set; }
    }
}
