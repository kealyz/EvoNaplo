using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvoNaplo.Models;
using EvoNaplo.Models.DTO;
using EvoNaplo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvoNaplo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentorController : ControllerBase
    {
        private readonly MentorService _mentorService;

        public MentorController(MentorService MentorService)
        {
            _mentorService = MentorService;
        }

        [HttpPost("signInMentor")]
        public async Task<string> PostSignInMentor(LoginDTO loginDto)
        {
            return await _mentorService.SigninMentor(loginDto);
        }

        // Add
        // api/Mentor + mentorDto megadása Postman body-ban
        [HttpPost]
        public async Task<int> PostAddMentor(MentorDto mentorDto)
        {
            await _mentorService.AddMentor(mentorDto);
            return StatusCodes.Status200OK;
        }

        // Get
        // api/Mentor
        [HttpGet]
        [Authorize(Roles = "Mentor")]
        public IEnumerable<User> GetMentors()
        {
            return _mentorService.ListMentors();
        }

        //PUT
        // api/Mentor/edit jsonben paramból id és bodyból mentorDto
        [HttpPut("edit")]
        public async Task<int> PutEditMentor(int id, MentorDto mentorDto)
        {
            await  _mentorService.EditMentor(id, mentorDto);
            return StatusCodes.Status200OK;
        }

        //PUT
        // api/Mentor/inactivate jsonben paramból id
        [HttpPut("inactivate")]
        public async Task<int> PutInactivateMentor(int id)
        {
            await _mentorService.InactivateMentor(id);
            return StatusCodes.Status200OK;
        }

        //Delete
        // api/Mentor/delete jsonben paramból id
        [HttpDelete("delete")]
        public async Task<int> DeleteMentor(int id)
        {
            await _mentorService.DeleteMentor(id);
            return StatusCodes.Status200OK;
        }
    }
}