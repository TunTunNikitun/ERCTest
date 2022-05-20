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
    }
}
