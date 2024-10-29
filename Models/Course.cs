using System.ComponentModel.DataAnnotations;

namespace Studentmanagement.Models;

    public class Course
    {

    [Key]
    public int Id { get; set; }
    [Required]
    [MinLength(3)]
    public string Name { get; set; }
    
    public string SessionName { get; set; }
    }

