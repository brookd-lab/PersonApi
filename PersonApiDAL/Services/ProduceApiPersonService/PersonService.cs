using Microsoft.EntityFrameworkCore;
using PersonApiDAL.Context;
using PersonApiDAL.Models;

namespace PersonApiDAL.Services.ProduceApiPersonService
{
    public class PersonService : IPersonService
    {
        private readonly ApiContext _personContext;

        public PersonService(ApiContext personContext)
        {
            _personContext = personContext;
        }

        public async Task<IEnumerable<Person>> GetAllPeople()
        {
            var people = await _personContext.Persons.ToListAsync();
            return people;
        }

        public async Task<Person> GetPerson(int id)
        {
            Person? person = await _personContext.Persons.FindAsync(id);
            return person!;
        }

        public async Task CreatePerson(Person person)
        {
            await _personContext.Persons.AddAsync(person);
            await _personContext.SaveChangesAsync();
        }

        public async Task UpdatePerson(Person person)
        {
            _personContext.Update(person);
            await _personContext.SaveChangesAsync();
        }

        public async Task DeletePerson(Person person)
        {
            _personContext.Remove(person);
            await _personContext.SaveChangesAsync();
        }
    }
}
