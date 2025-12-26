using Microsoft.AspNetCore.Mvc;
using WebApplication2.Domain;
using WebApplication2.Validators;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly PersonContext _context;

        public PersonController(PersonContext context)
        {
            _context = context;

        }

        [HttpPost("addPerson")]
        public IActionResult AddPerson([FromBody] Data.Person person)
        {
            var validator = new PersonValidator();
            var result = validator.Validate(person);

            if (result.IsValid)
            {
                _context.Persons.Add(person);
                _context.SaveChanges();

                return Ok("Person added successfully!");
            }
            else
            {
                var errors = result.Errors.Select(error => error.ErrorMessage).ToList();
                return BadRequest(errors);
            }
        }


        [HttpGet("getList")]
        public IActionResult GetPersonList()
        {
            var data = _context.Persons.ToList();
            return Ok(data);
        }

        [HttpGet("getPersonById/{id}")]
        public IActionResult GetPersonById([FromRoute] int id)
        {
            var person = _context.Persons.Find(id); 
            if (person == null)
                return NotFound("Person not found");
            return Ok(person);
        }

        [HttpGet("filterPersons")]
        public IActionResult FilterPersons([FromQuery] double? salary, [FromQuery] string? city)
        {
            var data = _context.Persons.ToList();

            if (salary.HasValue)
            {
                data = _context.Persons.Where(p => p.Salary >= salary.Value).ToList();
                _context.SaveChanges();
            }

            if (!string.IsNullOrEmpty(city))
            {
                data = _context.Persons.Where(p => p.PersonAddress.City.ToLower() == city.ToLower()).ToList();
                _context.SaveChanges();

            }

            return Ok(data);
        }


        [HttpDelete("deletePerson/{id}")]
        public IActionResult DeletePerson([FromRoute] int id)
        {
            var person = _context.Persons.Find(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
                _context.SaveChanges();
                return Ok($"Successfully deleted the person at index {id}");
            }
            else
            {
                return NotFound("No person at that index");
            }
        }


        [HttpPut("updatePerson/{id}")]
        public IActionResult UpdatePerson([FromBody] Data.Person person, [FromRoute] int id)
        {
            Data.Person personAtIndex = _context.Persons.Find(id);
            if (personAtIndex != null)
            {
                var validator = new PersonValidator();
                var result = validator.Validate(person);
                if (result.IsValid)
                {
                    personAtIndex.FirstName = person.FirstName;
                    personAtIndex.LastName = person.LastName;
                    personAtIndex.JobPosition = person.JobPosition;
                    personAtIndex.Salary = person.Salary;
                    personAtIndex.WorkExperience = person.WorkExperience;
                    personAtIndex.CreateDate = person.CreateDate;
                    personAtIndex.PersonAddress.Country = person.PersonAddress.Country;
                    personAtIndex.PersonAddress.City = person.PersonAddress.City;
                    personAtIndex.PersonAddress.HomeNumber = person.PersonAddress.HomeNumber;
                    _context.SaveChanges();
                    return Ok($"Successfully updated the person at index {id}");
                }
                else
                {
                    var errors = result.Errors.Select(error => error.ErrorMessage).ToList();
                    return BadRequest(errors);
                }
            }
            else
            {
                return NotFound("No person at that index");
            }
        }
    }
}
