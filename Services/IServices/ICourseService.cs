using Studentmanagement.DTO;
using Studentmanagement.Models;

namespace Studentmanagement.Services.IServices
{
    public interface ICourseService
    {

        public Task<CourseReviewDTO> GetCourseByIdAsync(int Id);
        public Task<IEnumerable<CourseReviewDTO>> GetCoursesAsync();
        public Task<CourseReviewDTO> CreateCourseAsync(CourseCommandDTO CourseDTO);
        public Task<CourseReviewDTO> UpdateCourseAsync(int Id,CourseCommandDTO CourseDTO);
        public Task<int> DeleteCourseAsync(int Id);
    }
}
