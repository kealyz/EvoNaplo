using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvoNaplo.DataAccessLayer;
using EvoNaplo.Models;
using EvoNaplo.Models.DTO;
using EvoNaplo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EvoNaplo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterController : ControllerBase
    {
        private readonly SemesterService _semesterService;

        public SemesterController(SemesterService SemesterService)
        {
            _semesterService = SemesterService;
        }

        // Add
        // POST /api/Semester Postman body részébe az adatok
        [HttpPost]
        public async Task<int> /*IEnumerable<Semester>*/ PostAddSemester(SemesterDto semesterDto)
        {
            await _semesterService.PostAddSemester(semesterDto);
            return StatusCodes.Status200OK; 
        }

        // List
        // GET /api/Semester
        [HttpGet]
        public IEnumerable<Semester> GetSemesters()
        {
            
            return _semesterService.GetSemesters();
        }


        //Delete (is-active falsera)
        // PUT /api/Semester Postman param részébe az adatok
        [HttpPut]
        public async Task<int> PutDeleteSemester(int id)
        {
            await _semesterService.PutDeleteSemester(id);
            return StatusCodes.Status200OK;
        }

        //Edit
        // PUT /api/Semester/edit Postman params és body részébe az adatok
        [HttpPut("edit")]
        public async Task<int> PutEditSemester(int id, SemesterDto semesterDto)
        {
            await _semesterService.PutEditSemester(id, semesterDto);
            return StatusCodes.Status200OK;
        }
    }
}