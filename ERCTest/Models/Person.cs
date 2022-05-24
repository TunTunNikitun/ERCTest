namespace ERCTest.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string? Patronymic { get; set; }
        public Person(string surname, string name, string? patronymic)
        {
            this.Surname = surname;
            this.Name = name;
            if (patronymic != null)
                this.Patronymic = patronymic;
        }
        public Person() { }

        public override string ToString()
        {
            return $"{this.Surname} {this.Name} {this.Patronymic}";
        }
    }
   
}
