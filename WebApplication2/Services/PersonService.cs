using Microsoft.AspNetCore.Identity.Data;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public interface IPersonService
    {
        User Login(User model);
        User GetById(int id);

    }
    public class PersonService : IPersonService
    {

        private List<User> _people = new List<User>
        {
            new User { Id = 1, FirstName = "George", LastName = "Gvatua", Password = "passWord123",UserName="George123", Role = Role.Admin },
            new User { Id = 2, FirstName = "Neli", LastName = "Gvaramia", Password = "passWord678", UserName="Neli123",Role = Role.User },
        };

        public User Login(User model)
        {
            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
            {
                return null;
            }

            var person = _people.SingleOrDefault(x=> x.UserName == model.UserName && x.Password == model.Password);
            if(person == null)
            {
                return null;
            }
            return person;
        }

        public User GetById(int id)
        {
            return _people.FirstOrDefault(person => person.Id == id);

        }
    }
}
