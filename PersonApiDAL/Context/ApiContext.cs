
using Microsoft.EntityFrameworkCore;
using PersonApiDAL.Models;

namespace PersonApiDAL.Context
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
    } 
}
