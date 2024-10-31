using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using Studentmanagement.DTO;
using Studentmanagement.Helper;
using Studentmanagement.Models;
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
        public async Task<CourseReviewDTO> CreateCourseAsync(CourseCommandDTO CourseDTO)
        {
            Course obj = CourseDTO.CourseDtoToCourse();
            _Context.Course.Add(obj);
            var result = await _Context.SaveChangesAsync();
            if (result > 0) {
                return obj.ToReviewDto();
            }
            return null;
        }

        public async Task<int> DeleteCourseAsync(int Id)
        {
            var course = await _Context.Course.Where(c => c.Id == Id).FirstOrDefaultAsync();
            if (course is null)
                return  0;

            _Context.Course.Remove(course);
            _Context.SaveChangesAsync();
            return 1;
        }

        public async Task<CourseReviewDTO> GetCourseByIdAsync(int Id)
        {
            return await _Context.Course.Select(c => new CourseReviewDTO
            {
                Id = Id,
                Name = c.Name,
                sessionName = c.SessionName,
            }).Where(i => i.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CourseReviewDTO>> GetCoursesAsync()
        {
            var course = await _Context.Course.Select(c=> new CourseReviewDTO { 
            Id = c.Id,
            Name = c.Name,
            sessionName = c.SessionName,
            }).ToListAsync();

            if (course == null)
            {
                return new List<CourseReviewDTO>();
            }

            return course;
        }

        public async Task<CourseReviewDTO> UpdateCourseAsync(int Id, CourseCommandDTO CourseDTO)
        {
            if (!_Context.Course.Any(i => i.Id == Id))
                return null;

            Course obj = CourseDTO.CourseDtoToCourse();

            _Context.Entry(obj).State = EntityState.Modified;

            var result = await _Context.SaveChangesAsync();

            if (result > 0)
                return obj.ToReviewDto();

            return null;
        }
    }
}
