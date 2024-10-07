using System.ComponentModel.DataAnnotations;

namespace PersonApiDAL.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set;  }
        public int Age { get; set; }
    }   
}
