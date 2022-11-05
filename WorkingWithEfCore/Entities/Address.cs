namespace WorkingWithEfCore.Entities
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int UserId { get; set; }

        //creatim чтобы EF понял как связать юзера и адреса:
        public User Users { get; set; }
    }
}
