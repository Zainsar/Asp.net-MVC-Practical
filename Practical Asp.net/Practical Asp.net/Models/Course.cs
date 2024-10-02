using System.ComponentModel.DataAnnotations;

namespace Practical_Asp.net.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
