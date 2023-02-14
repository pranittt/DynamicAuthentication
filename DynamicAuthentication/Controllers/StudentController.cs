using DynamicAuthentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace DynamicAuthentication.Controllers
{


    [Route("api/[controller]")]
    [ApiController]



    public class StudentController : ControllerBase
    {
        private readonly StudentContext _context;
        public StudentController(StudentContext dbcontext)
        {
            _context = dbcontext;
        }

        [HttpPost]
        public async Task<IActionResult> Register(Detail detail)
        {
            var hmc = new HMACSHA256();

            Student st = new Student();

            st.Email = detail.Email;
            st.Name = detail.Name;
            var pass = hmc.ComputeHash(Encoding.ASCII.GetBytes(detail.Password));
            st.Password = pass;
            st.PasswordSalt = hmc.Key;



            _context.students.Add(st);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("Api/login")]
        public async Task<IActionResult> fetchData(Login login)
        {
            var user = _context.students.FirstOrDefault(x => x.Name == login.Name);
                if(user == null)
            {
                return BadRequest();

            }
            var hmc = new HMACSHA256(user.PasswordSalt);
            var hmcdata = hmc.ComputeHash(Encoding.ASCII.GetBytes(login.Password));
            if (hmcdata.SequenceEqual(user.Password))
            {
                return Ok("found");
            }
            else
            {
                return NotFound("not found");
            }

        }
        




    }
}

