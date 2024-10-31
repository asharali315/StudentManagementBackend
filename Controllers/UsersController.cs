using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Studentmanagement.DTO;
using Studentmanagement.Helper;
using Studentmanagement.Models;
using Studentmanagement.Persistance;

namespace Studentmanagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly StudentManagementDBContext _context;
        private readonly IConfiguration _configuration;

        public UsersController(StudentManagementDBContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {

          var user = await _context.User.Where(i => i.Email.ToLower() == loginModel.Email.ToLower() && i.Password == loginModel.Password).FirstOrDefaultAsync();
            if (user is null) return BadRequest(new { message = "user not found/wrong credential" });

            string Token = await LoginJWT(user);

            UserDTO obj =  user.UserToDTO(Token);
            
            return Ok(new { message = "user logged in",data = obj });

        }


        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }




        private async Task<string> LoginJWT(User? user)
        {
            if (user is null) return "";
            //var RoleName = await userManager.GetRolesAsync(user);
            //if (RoleName.Count > 0)
            //{
            //    Role = await roleManager.FindByNameAsync(RoleName[0]);
            //}
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("Email", user.Email),
                    new Claim("Id",user.Id.ToString()),
                    //new Claim("PersonalNumber",  user.PersonalNumber==null ? string.Empty:user.PersonalNumber),
                    new Claim("RoleId","1"),
                    new Claim("RoleName","Admin")
            
            };


            //foreach (var userRole in userRoles)
            //{
            //    //authClaims.Add("Email", await roleManager.FindByNameAsync(RoleName[0]))
            //    var roleid = await roleManager.FindByNameAsync(userRole);
            //    authClaims.Add(new Claim("RoleId", roleid.Id));
            //    authClaims.Add(new Claim("RoleName", roleid.Name));
            //    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            //}


            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(24),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            string JWTToken = new JwtSecurityTokenHandler().WriteToken(token);
            return JWTToken;
        }
    }
}
