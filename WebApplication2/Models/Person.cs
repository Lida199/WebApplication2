namespace WebApplication2.Models
{
    public class Person
    {
        public DateTime CreateDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobPosition { get; set; }
        public double Salary { get; set; }
        public double WorkExperience { get; set; }
        public Address PersonAddress { get; set; }


        public Person(Person other)
        {
            CreateDate = other.CreateDate;
            FirstName = other.FirstName;
            LastName = other.LastName;
            JobPosition = other.JobPosition;
            Salary = other.Salary;
            WorkExperience = other.WorkExperience;
            PersonAddress = new Address
            {
                Country = other.PersonAddress.Country,
                City = other.PersonAddress.City,
                HomeNumber = other.PersonAddress.HomeNumber
            };
        }

        public Person() { }
    }
}
