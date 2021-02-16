using EvoNaplo.DataAccessLayer;
using EvoNaplo.Models;
using EvoNaplo.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvoNaplo.Services
{
    public class StudentService
    {
        private readonly EvoNaploContext _evoNaploContext;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly UserActionService _userActionService;

        public StudentService(EvoNaploContext EvoNaploContext, UserActionService userActionService)
        {
            _evoNaploContext = EvoNaploContext;
            _userActionService = userActionService;
        }

        //Student hozzáadása. Ha van phone number akkor úgy hívja meg a konstruktort, egyébként anélkül
        public async Task<int> AddStudent(StudentDto studentDto)
        {
            _logger.Debug($"Diák hozzáadása következik: {studentDto}");
            if (studentDto.PhoneNumber != null)
            {
                await _evoNaploContext.Users2.AddAsync(new User(studentDto.Email, studentDto.Password, studentDto.FirstName, studentDto.LastName, studentDto.PhoneNumber, studentDto.UserRole));
            }
            else
            {
                await _evoNaploContext.Users2.AddAsync(new User(studentDto.Email, studentDto.Password, studentDto.FirstName, studentDto.LastName, studentDto.UserRole));
            }
            if (await _userActionService.SignUpStudent(studentDto) == StatusCodes.Status200OK)
            {
                await _userActionService.AddUserToRole(studentDto.Email, "Student");
                _evoNaploContext.SaveChanges();
                _logger.Debug($"Diák hozzáadva");
                //var students = _evoNaploContext.Users2.Where(m => m.Role == Role.Student);
                return StatusCodes.Status200OK;
            }
            else
            {
                return StatusCodes.Status500InternalServerError;
            }
        }

        public IEnumerable<User> ListStudents()
        {
            var students = _evoNaploContext.Users2.Where(m => m.Role == Role.Student);
            return students.ToList();

        }

        public async Task<IEnumerable<User>> EditStudent(int id, StudentDto studentDto)
        {
            _logger.Debug($"{id} ID-vel rendelkező diák keresése");
            var studentToEdit = await _evoNaploContext.Users2.FindAsync(id);
            _logger.Debug($"{id} ID-vel rendelkező diák módosítása indul {studentDto} adatokra");
            studentToEdit.Email = studentDto.Email;
            //studentToEdit.SetNewPassword(studentDto.Password);
            studentToEdit.FirstName = studentDto.FirstName;
            studentToEdit.LastName = studentDto.LastName;
            studentToEdit.PhoneNumber = studentDto.PhoneNumber;
            _evoNaploContext.SaveChanges();
            _logger.Debug($"{id} ID-vel rendelkező diák módosítása kész");
            var students = _evoNaploContext.Users2.Where(m => m.Role == Role.Student);
            return students.ToList();
        }

        public async Task<IEnumerable<User>> InactivateStudent(int id)
        {
            _logger.Debug($"{id} ID-vel rendelkező diák keresése");
            var studentToDelete = await _evoNaploContext.Users2.FindAsync(id);
            _logger.Debug($"{id} ID-vel rendelkező diák inaktiválása indul");
            studentToDelete.IsActive = false;
            _evoNaploContext.SaveChanges();
            _logger.Debug($"{id} ID-vel rendelkező diák inaktiválása kész");
            var students = _evoNaploContext.Users2.Where(m => m.Role == Role.Student);
            return students.ToList();
        }

        public async Task<IEnumerable<User>> DeleteStudent(int id)
        {
            _logger.Debug($"{id} ID-vel rendelkező diák keresése");
            var studentToDelete = await _evoNaploContext.Users2.FindAsync(id);
            _logger.Debug($"{id} ID-vel rendelkező diák törlése indul");
            _evoNaploContext.Users2.Remove(studentToDelete);
            _evoNaploContext.SaveChanges();
            _logger.Debug($"{id} ID-vel rendelkező diák törlése kész");
            var students = _evoNaploContext.Users2.Where(m => m.Role == Role.Student);
            return students.ToList();
        }

        public IEnumerable<User> GetStudentsOnProject(int id)
        {
            var userProjects = _evoNaploContext.UserProjects.Where(up => up.projectId == id);
            List<User> users = new List<User>();
            foreach (var up in userProjects.ToArray())
            {
                User user = _evoNaploContext.Users2.Where(u => u.Id == up.userId).First();
                user.StudentData = null;
                users.Add(user);
            }
            if (users.Count > 0)
            {
                return users;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<User> GetStudentsOnSemesterWithoutProject()
        {
            var users = _evoNaploContext.Users2;
            List<User> uwp = new List<User>();
            foreach (var user in users.ToArray())
            {
                if(_evoNaploContext.UserProjects.Where(up => up.userId == user.Id).ToArray().Length == 0)
                {
                    user.StudentData = null;
                    uwp.Add(user);
                }
            }
            if (uwp.Count > 0)
            {
                return uwp;
            }
            else
            {
                return null;
            }
        }

        public User getCurrentUser(string id)
        {
            Guid userId = Guid.Parse(id);
            var identityUser = _evoNaploContext.Users.FirstOrDefault(user => user.Id == userId);
            var user = _evoNaploContext.Users2.FirstOrDefault(user => user.Email == identityUser.Email);
            return user;
        }
    }
}
