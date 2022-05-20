namespace ERCTest.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string? Patronymic { get; set; }
        public override string ToString()
        {
            return $"{this.Surname} {this.Name} {this.Patronymic}";
        }
    }
   
}
