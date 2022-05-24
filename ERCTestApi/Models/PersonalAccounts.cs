namespace ERCTestApi.Models
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

        public PersonalAccounts(string name, string surname, string? patronymic, string city, string street,
                int building, int? housing, int? flat, double square, int residentsNumber)
        {
            Person person = new Person(surname, name, patronymic);
            this.Client = person;

            Address address = new Address(city, street, building, housing, flat);
            this.Address = address;

            this.Square = square;
            this.ResidentsNumber = residentsNumber;
            this.OpenDate = DateTime.Now;
        }
        public PersonalAccounts() { }
    }


}
