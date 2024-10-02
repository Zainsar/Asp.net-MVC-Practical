using System.ComponentModel.DataAnnotations;

namespace Practical_Asp.net.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
