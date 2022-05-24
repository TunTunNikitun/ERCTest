namespace ERCTestApi.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Building { get; set; }
        public int? Housing { get; set; }
        public int? Flat { get; set; }
        public Address(string city, string street, int building, int? housing, int? flat)
        {
            this.City = city;
            this.Street = street;
            this.Building = building;
            if(housing != null)
                this.Housing = housing;
            if(flat != null)
                this.Flat = flat;
        }
        public Address() { }
    }
}
