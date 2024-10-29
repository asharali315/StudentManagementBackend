using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Studentmanagement.Models;

    public class User
    {
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Cnic { get; set; }
    [Required]
    public string GuardianName { get; set; }
    [AllowNull]
    public string Image { get; set; }
    [Required]
    public int ContactNumber { get; set; }
}

