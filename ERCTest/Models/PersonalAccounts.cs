namespace ERCTest.Models
{
    public class PersonalAccounts
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Person Client { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public double Square { get; set; }
        public int ResidentsNumber { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
    }
}
