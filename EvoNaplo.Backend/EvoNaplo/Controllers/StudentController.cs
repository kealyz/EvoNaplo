using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using EvoNaplo.Models;
using EvoNaplo.Models.DTO;
using EvoNaplo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvoNaplo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;
        private readonly HttpContextAccessor _httpContextAccessor;

        public StudentController(StudentService StudentService, HttpContextAccessor httpContextAccessor)
        {
            _studentService = StudentService;
            _httpContextAccessor = httpContextAccessor;
        }

        // Add
        // api/Student + studentDto megadása Postman body-ban
        [HttpPost]
        public async Task<int> PostAddStudent(StudentDto studentDto)
        {
            await _studentService.AddStudent(studentDto);

            return StatusCodes.Status200OK;
        }

        // Get
        // api/Student
        [HttpGet]
        [Authorize(Roles = "Student")]
        public IEnumerable<User> GetStudent()
        {
            return _studentService.ListStudents();
        }

        //PUT
        // api/Student/edit jsonben paramból id és bodyból studentDto
        [HttpPut("edit")]
        public async Task<int> PutEditStudent(int id, StudentDto studentDto)
        {
            await _studentService.EditStudent(id, studentDto);
            return StatusCodes.Status200OK;
        }

        //PUT
        // api/Student/inactivate jsonben paramból id
        [HttpPut("inactivate")]
        public async Task<int> PutInactivateStudent(int id)
        {
            await _studentService.InactivateStudent(id);
            return StatusCodes.Status200OK;
        }

        //Delete
        // api/Student/delete jsonben paramból id
        [HttpDelete("delete")]
        public async Task<int> DeleteStudent(int id)
        {
            await _studentService.DeleteStudent(id);
            return StatusCodes.Status200OK;
        }

        [HttpGet("getStudentsOnProject")]
        public IEnumerable<User> GetStudentsOnProject(int id)
        {
            return _studentService.GetStudentsOnProject(id);
        }

        [HttpGet("getStudentsOnSemesterWithoutProject")]
        public IEnumerable<User> GetStudentsOnSemesterWithoutProject()
        {
            return _studentService.GetStudentsOnSemesterWithoutProject();
        }

        [HttpGet("currentUser")]
        public User GetCurrentUser()
        {
            string header = HttpContext.Request.Headers["Authorization"];
            string[] jwtParts = header.Split(' ');
            string jwt = jwtParts[1];
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var jwtDecoded = handler.ReadJwtToken(jwt) as JwtSecurityToken;
            var payloadDecoded = jwtDecoded.Payload;
            string userId = (string)payloadDecoded[ClaimTypes.NameIdentifier];
            return _studentService.getCurrentUser(userId);
        }
    }
}