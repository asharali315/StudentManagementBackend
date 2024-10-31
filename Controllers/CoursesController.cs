using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Studentmanagement.DTO;
using Studentmanagement.Models;
using Studentmanagement.Persistance;
using Studentmanagement.Services.IServices;

namespace Studentmanagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        public ICourseService _CourseService { get; }

        public CoursesController(ICourseService CourseService)
        {
            _CourseService = CourseService;
        }


        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult> GetCourse()
        {
            var result = await _CourseService.GetCoursesAsync();
            return Ok(new { message = "Data retrieved", data = result }); 
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _CourseService.GetCourseByIdAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(new { message = "Data retrieved", data = course});
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, CourseCommandDTO course)
        {
          var result = await _CourseService.UpdateCourseAsync(id, course);

            return Ok(new { message = "course updated", data = result });
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(CourseCommandDTO course)
        {
            var result = await _CourseService.CreateCourseAsync(course);

            return Ok(new { message = "course created",data = result });
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _CourseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }


            return Ok(new { message = "course deleted", data = course });
        }

    }
}
