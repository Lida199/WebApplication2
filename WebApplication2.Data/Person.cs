using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Data
{
    public class Person
    {
        [Key]
        public int Id { get; private set; }
        public DateTime CreateDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobPosition { get; set; }
        public double Salary { get; set; }
        public double WorkExperience { get; set; }
        public Address PersonAddress { get; set; }

        public class Address
        {
            public string Country { get; set; }
            public string City { get; set; }
            public string HomeNumber { get; set; }
        }
    }
}
