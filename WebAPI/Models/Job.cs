using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    
    public class Job
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public int Salary { get; set; }
        
        public override string ToString()
        {
            return Id + ", " + JobTitle + ", " + Salary;
        }
    }
}