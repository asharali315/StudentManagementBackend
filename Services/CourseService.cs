using Studentmanagement.DTO;
using Studentmanagement.Persistance;
using Studentmanagement.Services.IServices;

namespace Studentmanagement.Services
{
    public class CourseService : ICourseService
    {
        private readonly StudentManagementDBContext _Context;
        public CourseService(StudentManagementDBContext context)
        {
            _Context = context;
        }
        public Task<CourseReviewDTO> CreateCourseAsync(CourseCommandDTO CourseDTO)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteCourseAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<CourseReviewDTO> GetCourseByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CourseReviewDTO>> GetCoursesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CourseReviewDTO> UpdateCourseAsync(int Id, CourseCommandDTO CourseDTO)
        {
            throw new NotImplementedException();
        }
    }
}
