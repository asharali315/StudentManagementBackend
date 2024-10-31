using System.Diagnostics.CodeAnalysis;

namespace Studentmanagement.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Cnic { get; set; }
        public string GuardianName { get; set; }
        public string Image { get; set; }
        public int ContactNumber { get; set; }

        public string RoleName { get; set; }
        public string Token { get; set; }

    }
}
