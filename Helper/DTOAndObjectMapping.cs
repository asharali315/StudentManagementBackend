using Studentmanagement.DTO;
using Studentmanagement.Models;
using System.Drawing;

namespace Studentmanagement.Helper
{
    public static class DTOAndObjectMapping
    {
        public static Course CourseDtoToCourse(this CourseCommandDTO CourseDTO) {

            Course Course = new Course();
            
            Course.Id = CourseDTO.Id;
            Course.Name = CourseDTO.Name;
            Course.SessionName = CourseDTO.sessionName;

            return Course;
        }


        public static CourseCommandDTO ToDto(this Course course)
        {
            if (course == null) return null;

            return new CourseCommandDTO
            {
                Id = course.Id,
                Name = course.Name,
                sessionName = course.SessionName
            };
        }


        public static CourseReviewDTO ToReviewDto(this Course course)
        {
            if (course == null) return null;

            return new CourseReviewDTO
            {
                Id = course.Id,
                Name = course.Name,
                sessionName = course.SessionName
            };
        }


        public static Course ToEntityFromReview(this CourseReviewDTO course)
        {
            if (course == null) return null;

            return new Course
            {
                Id = course.Id,
                Name = course.Name,
                SessionName = course.sessionName
            };
        }


        public static UserDTO UserToDTO(this User user, string token)
        {

            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Cnic = user.Cnic,
                ContactNumber = user.ContactNumber,
                Email = user.Email,
                GuardianName = user.GuardianName,
                Image = user.Image,
                RoleName = "Admin",
                Token = token
            };
        }

    }
}
