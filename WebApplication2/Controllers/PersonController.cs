using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApplication2.Models;
using WebApplication2.Validators;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private string FilePath = @"C:\Users\lidashubitidze\source\repos\WebApplication2\WebApplication2\Persons.json";
        private List<Person> GetPersonsFromJson()
        {
            string json = System.IO.File.ReadAllText(FilePath);
            List<Person> list = JsonSerializer.Deserialize<List<Person>>(json)
                    ?? new List<Person>();
            return list;
        }

        [HttpPost("addPerson")]
        public IActionResult AddPerson([FromBody] Person person)
        {
            var validator = new PersonValidator();
            var result = validator.Validate(person);

            if (result.IsValid)
            {

                var list = GetPersonsFromJson();                
                list.Add(person);

                string updatedJson = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText(FilePath, updatedJson);

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
            return Ok(GetPersonsFromJson());
        }

        [HttpGet("getPersonById/{id}")]
        public IActionResult GetPersonById([FromRoute] int id)
        {
            var list = GetPersonsFromJson();
            if (id > 0 && id <= list.Count)
            {
                return Ok(list[id - 1]);
            }
            else
            {
                return NotFound("No person at that index");
            }
        }

        [HttpGet("filterPersons")]
        public IActionResult FilterPersons([FromQuery] double? salary, [FromQuery] string? city)
        {
            var list = GetPersonsFromJson();

            if (salary.HasValue)
            {
                list = list.Where(p => p.Salary >= salary.Value).ToList();
            }

            if (!string.IsNullOrEmpty(city))
            {
                list = list.Where(p => p.PersonAddress.City.ToLower() == city.ToLower()).ToList();
            }

            return Ok(list);
        }


        [HttpDelete("deletePerson/{id}")]
        public IActionResult DeletePerson([FromRoute] int id)
        {
            var list = GetPersonsFromJson();
            if (id > 0 && id <= list.Count)
            {
                list.RemoveAt(id - 1);
                string updatedJson = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText(FilePath, updatedJson);
                return Ok($"Successfully deleted the person at index {id}");
            }
            else
            {
                return NotFound("No person at that index");
            }
        }


        [HttpPut("updatePerson/{id}")]
        public IActionResult UpdatePerson([FromBody] Person person, [FromRoute] int id)
        {
            var list = GetPersonsFromJson();
            if (id > 0 && id <= list.Count)
            {
                var validator = new PersonValidator();
                var result = validator.Validate(person);
                if (result.IsValid)
                {

                list[id - 1] = person;
                string updatedJson = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText(FilePath, updatedJson);
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
