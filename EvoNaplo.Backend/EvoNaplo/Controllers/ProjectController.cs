using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvoNaplo.Models;
using EvoNaplo.Models.DTO;
using EvoNaplo.Models.ViewModel;
using EvoNaplo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvoNaplo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;

        public ProjectController(ProjectService ProjectService)
        {
            _projectService = ProjectService;
        }

        // Add
        // api/Projects
        [HttpPost]
        public async Task<int> PostAddProject(ProjectDTO projectDTO)
        {
            await _projectService.AddProject(projectDTO);
            return StatusCodes.Status200OK;
        }

        // Get
        // api/Projects
        [HttpGet]
        public IEnumerable<Project> GetProjects()
        {
            return _projectService.ListProjects();
        }


        //PUT
        // api/Mentor/edit jsonben paramból id és bodyból mentorDto
        [HttpPut("edit")]
        public async Task<int> PutEditProject(int id, ProjectDTO projectDTO)
        {
            await _projectService.EditProject(id, projectDTO);
            return StatusCodes.Status200OK;
        }


        //PUT
        // api/Mentor/inactivate jsonben paramból id
        /*[HttpPut("inactivate")]
        public async Task<int> PutInactivateMentor(int id)
        {
            await _projectService.InactivateMentor(id);
            return StatusCodes.Status200OK;
        }*/


        //Gets a projects by its id
        [HttpGet("id")]
        public Project GetProjectById(int id)
        {
            return _projectService.GetProjectById(id);
        }

        [HttpGet("getSemesterProjectById")]
        public SemesterStarterAdminProjektViewModel GetSemesterProjectById(int id)
        {
            return _projectService.GetSemesterProjectById(id);
        }

        [HttpGet("semesterProjects")]
        public IEnumerable<Project> GetProjectBySemesterId(int id)
        {
            return _projectService.GetProjectBySemesterId(id);
        }

        [HttpGet("adminSemesterProjects")]
        public IEnumerable<SemesterStarterAdminProjektViewModel> GetSemesterStarterAdminProjekts(int id)
        {
            return _projectService.GetSemesterAdminProjects(id);
        }


        [HttpPost("assignUserToProject")]
        public async Task<int> AssignUserToProject(string email, int id)
        {
            await _projectService.AssignUserToProject(email, id);
            return StatusCodes.Status200OK;
        }

        [HttpGet("getMyProjectForTheSemester")]
        public Project GetMyProjectForTheSemester(string email)
        {
            return _projectService.GetMyProjectForTheSemester(email);
        }

        [HttpDelete("deleteProjectSubscription")]
        public async Task<int> DeleteStudent(int id)
        {
            await _projectService.DeleteUserProject(id);
            return StatusCodes.Status200OK;
        }
        [HttpGet("getSubscribedProject")]
        public UserProject GetSubscribedProjectId(string email)
        {
            return _projectService.GetSubscribedProject(email);
        }
    }
}