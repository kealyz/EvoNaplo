using EvoNaplo.DataAccessLayer;
using EvoNaplo.Models;
using EvoNaplo.Models.DTO;
using EvoNaplo.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EvoNaplo.Services
{
    public class ProjectService
    {
        private readonly EvoNaploContext _evoNaploContext;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ProjectService(EvoNaploContext EvoNaploContext)
        {
            _evoNaploContext = EvoNaploContext;
        }

        //Projekt
        public async Task<IEnumerable<Project>> AddProject(ProjectDTO projectDTO)
        {
            _logger.Debug($"Projekt hozzáadása következik: {projectDTO}");

            await _evoNaploContext.Projects.AddAsync(new Project(projectDTO.ProjectName, projectDTO.SourceLink, projectDTO.UsedTechnologies, projectDTO.SemesterId));

            _evoNaploContext.SaveChanges();
            _logger.Debug($"Projekt hozzáadva");

            var projects = _evoNaploContext.Projects;
            return projects.ToList();
        }

        public IEnumerable<Project> ListProjects()
        {
            var projects = _evoNaploContext.Projects;
            return projects.ToList();

        }



        public async Task<IEnumerable<Project>> EditProject(int id, ProjectDTO projectDTO)
        {
            _logger.Debug($"{id} ID-vel rendelkező mentor keresése");
            var projectToEdit = await _evoNaploContext.Projects.FindAsync(id);
            _logger.Debug($"{id} ID-vel rendelkező mentor módosítása indul {projectDTO} adatokra");
            projectToEdit.Name = projectDTO.ProjectName;
            projectToEdit.SourceLink = projectDTO.SourceLink;
            projectToEdit.UsedTechnologies = projectDTO.UsedTechnologies;
            projectToEdit.SemesterId = projectDTO.SemesterId;
            _evoNaploContext.SaveChanges();
            _logger.Debug($"{id} ID-vel rendelkező projekt módosítása kész");
            var projects = _evoNaploContext.Projects;
            return projects.ToList();
        }

        /*public async Task<IEnumerable<Project>> InactiveProject(int id)
        {
            _logger.Debug($"{id} ID-vel rendelkező mentor keresése");
            var mentorToDelete = await _evoNaploContext.Users.FindAsync(id);
            _logger.Debug($"{id} ID-vel rendelkező mentor inaktiválása indul");
            mentorToDelete.IsActive = false;
            _evoNaploContext.SaveChanges();
            _logger.Debug($"{id} ID-vel rendelkező mentor inaktiválása kész");
            var mentors = _evoNaploContext.Users.Where(m => m.Role == Role.Mentor);
            return mentors.ToList();
        }*/

        public Project GetProjectById(int id)
        {

            foreach (var project in _evoNaploContext.Projects)
            {
                if (project.Id == id)
                {
                    return project;
                }
            }
            return null;
        }

        public IEnumerable<Project> GetProjectBySemesterId(int id)
        {
            var projectList = _evoNaploContext.Projects.Where(p => p.SemesterId == id);
            return projectList;
        }

        public SemesterStarterAdminProjektViewModel GetSemesterProjectById(int id)
        {
            var project = GetProjectById(id);
            SemesterStarterAdminProjektViewModel semesterStarterAdminProjektViewModel = new SemesterStarterAdminProjektViewModel();
            semesterStarterAdminProjektViewModel.Id = project.Id;
            semesterStarterAdminProjektViewModel.Name = project.Name;

            List<int> userIds = new List<int>();
            _evoNaploContext.UserProjects.Where(up => up.projectId == project.Id)
                .ToList()
                .ForEach(o => userIds.Add(o.userId));


            List<UserViewModel> uvm = new List<UserViewModel>();
            foreach (var uid in userIds)
            {
                User a = _evoNaploContext.Users2.First(u => u.Id == uid);
                string name = $"{a.FirstName} {a.LastName}";
                uvm.Add(new UserViewModel { Id = a.Id, Name = name });
            }

            semesterStarterAdminProjektViewModel.Students = uvm;


            return semesterStarterAdminProjektViewModel;
        }

        public IEnumerable<SemesterStarterAdminProjektViewModel> GetSemesterAdminProjects(int id)
        {
            var projectList = _evoNaploContext.Projects.Where(p => p.SemesterId == id);
            List<SemesterStarterAdminProjektViewModel> semesterStarterAdminProjektViewModels = new List<SemesterStarterAdminProjektViewModel>();
            foreach (var project in projectList)
            {
                SemesterStarterAdminProjektViewModel model = new SemesterStarterAdminProjektViewModel();
                model.Id = project.Id;
                model.Name = project.Name;
                semesterStarterAdminProjektViewModels.Add(model);
            }

            foreach (var project in semesterStarterAdminProjektViewModels)
            {
                List<int> userIds = new List<int>();
                _evoNaploContext.UserProjects.Where(up => up.projectId == project.Id)
                    .ToList()
                    .ForEach(o => userIds.Add(o.userId));


                List<UserViewModel> uvm = new List<UserViewModel>();
                foreach (var uid in userIds)
                {
                    User a = _evoNaploContext.Users2.First(u => u.Id == uid);
                    string name = $"{a.FirstName} {a.LastName}";
                    uvm.Add(new UserViewModel { Id = a.Id, Name = name });
                }

                project.Students = uvm;
            }


            return semesterStarterAdminProjektViewModels;
        }


        //Projekt
        public async Task<int> AssignUserToProject(string email, int id)
        {
            var user = _evoNaploContext.Users2.FirstOrDefault(u => u.Email == email);
            int userId = user.Id;
            await _evoNaploContext.UserProjects.AddAsync(new UserProject(userId, id));

            if (_evoNaploContext.SaveChanges() == 1)
            {
                return StatusCodes.Status200OK;
            }
            else
            {
                return StatusCodes.Status400BadRequest;
            }
        }

        public int GetSemesterId()
        {
            var semesterId = _evoNaploContext.Semesters.Max(s => s.Id);
            return semesterId;
        }

        public Project GetMyProjectForTheSemester(string email)
        {
            var user = _evoNaploContext.Users2.FirstOrDefault(u => u.Email == email);
            int userId = user.Id;
            var userProject = _evoNaploContext.UserProjects.Where(p => p.userId == userId);
            List<Project> projectList = new List<Project>();
            foreach (var up in userProject.ToList())
            {
                var project = _evoNaploContext.Projects.Where(p => p.Id == up.projectId);
                projectList.Add(project.First());
            }
            var proj = projectList.FindAll(p => p.SemesterId == GetSemesterId());
            if (proj.Count > 0)
            {
                proj.First().userProjects = null;
                return proj.First();
            }
            else
            {
                return null;
            }
        }

        public UserProject GetSubscribedProject(string email)
        {
            var user = _evoNaploContext.Users2.FirstOrDefault(u => u.Email == email);

            if (user != null)
            {
                int userId = user.Id;
                var userProject = _evoNaploContext.UserProjects.Where(p => p.userId == userId);
                var userProjects = userProject.ToList().ToArray();
                if (userProjects.Length > 0)
                {
                    UserProject up = new UserProject(userProjects[userProjects.Length - 1].userId, userProjects[userProjects.Length - 1].projectId);
                    up.id = userProjects[userProjects.Length - 1].id;
                    return up;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        public async Task<IEnumerable<Project>> DeleteUserProject(int id)
        {
            var projectToDelete = await _evoNaploContext.UserProjects.FindAsync(id);
            _evoNaploContext.UserProjects.Remove(projectToDelete);
            _evoNaploContext.SaveChanges();
            var projects = GetProjectBySemesterId(GetSemesterId());
            return projects;
        }
    }
}
