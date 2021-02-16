using EvoNaplo.Interfaces;
using EvoNaplo.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EvoNaplo.Models
{
    public class User //: IdentityUser<Guid>, IUser
    {
        PasswordService passwordService = new PasswordService();

        //fields
        private string _firstName;
        private string _lastName;
        private static readonly List<Semester> _semesters = new List<Semester>();
        private static readonly List<Project> _projects = new List<Project>();
        
        //PROPS
        public int Id { get; set; }
        public string Email { get; set; }
        private string password;
        //public List<UserProject> userProjects { get; set; }

        public string Password
        {
            get { return password; }
            private set { password = passwordService.HashPassword(value); }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                RefreshWholeName();
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                RefreshWholeName();
            }
        }
        public string Name { get; private set; }
        public string PhoneNumber { get; set; }
        public Role Role { get; private set; }
        public StudentData StudentData { get; set; }
        public bool IsActive { get; set; }

        //with phone number
        public User(string email, string password, string firstName, string lastName, string phoneNumber, Role role)
        {
            /*if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email is null or empty", nameof(email));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password is null or empty", nameof(password));
            }

            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentException("FirstName is null or empty", nameof(firstName));
            }

            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException("LastName is null or empty", nameof(lastName));
            }

            if (string.IsNullOrEmpty(phoneNumber))
            {
                throw new ArgumentException("PhoneNumber is null or empty", nameof(phoneNumber));
            }*/

            if (role == Role.Mentor || role == Role.Admin)
            {   //for mentor and admin
                Email = email;
                Password = password;
                FirstName = firstName;
                LastName = lastName;
                PhoneNumber = phoneNumber;
                Role = role;
                IsActive = true;
            }
            else //for students
            {
                Email = email;
                Password = password;
                FirstName = firstName;
                LastName = lastName;
                PhoneNumber = phoneNumber;
                Role = role;
                StudentData = new StudentData();
                IsActive = true;
            }
        }

        //without phone number
        //For some reason if validation is there it throws error when trying to add new mentor, but still creates new mentor.
        public User(string email, string password, string firstName, string lastName, Role role)
        {
            /*if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email is null or empty", nameof(email));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password is null or empty", nameof(password));
            }

            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentException("FirstName is null or empty", nameof(firstName));
            }

            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException("LastName is null or empty", nameof(lastName));
            }*/

            if (role == Role.Mentor || role == Role.Admin)
            {
                Email = email;
                Password = password;
                FirstName = firstName;
                LastName = lastName;
                Role = role;
                IsActive = true;
            }
            else
            {
                Email = email;
                Password = password;
                FirstName = firstName;
                LastName = lastName;
                Role = role;
                StudentData = new StudentData();
                IsActive = true;
            }
        }
        
        private void RefreshWholeName()
        {
            Name = _firstName + " " + _lastName;
        }

        //Start a new Semester by admin rights
        public void StartNewSemester(DateTime newSemestersStartingTime, DateTime newSemesterEndTime, DateTime newSemesterDemoTime)
        {
            if (Role == Role.Admin)
            {
                _semesters.Add(new Semester(newSemestersStartingTime, newSemesterEndTime, newSemesterDemoTime));
            }
        }
        //Admin can fill in the SemesterData
        public void SetSemesterEndDate(int semesterId, DateTime endDate)
        {
            if (Role == Role.Admin)
            {
                var semester = _semesters.Find(x => x.Id == semesterId);
                semester.EndDate = endDate;
            }
        }
        public void SetSemesterDemoDate(int semesterId, DateTime demoDate)
        {
            if (Role == Role.Admin)
            {
                var semester = _semesters.Find(x => x.Id == semesterId);
                semester.DemoDate = demoDate;
            }
        }

        //Create a new project, it could be admin and a mentor right as well
        public void CreateProject(string projectName, Uri sourceLink, string usedTechnologies, int semesterId)
        {
            if (Role != Role.Student)
            {
                _projects.Add(new Project(projectName, sourceLink, usedTechnologies, semesterId));
            }
        }

        //Mentor can fill in the project details
        public void SetProjectSoruceUri(string projectName, Uri sourceUri)
        {
            if (Role == Role.Mentor)
            {
            var project = _projects.Find(x => x.Name == projectName);
            project.SourceLink = sourceUri;
            }
        }
        public void SetProjectTechnologies(string projectName, string techs)
        {
            if (Role == Role.Mentor)
            {
                var project = _projects.Find(x => x.Name == projectName);
                project.UsedTechnologies = techs;
            }
        }
        /*public void AddStudentToProject(string projectName, User student)
        {
            if (Role == Role.Mentor)
            {
                var project = _projects.Find(x => x.Name == projectName);
                project.AddStudent(Role, student);
            }
        }
        public void RemoveStudentToProject(string projectName, User student)
        {
            if (Role == Role.Mentor)
            {
                var project = _projects.Find(x => x.Name == projectName);
                project.RemoveStudent(Role, student);
            }
        }*/
        public void AddAttendanceSheetToProject(string projectName, DateTime meetingDate)
        {
            if (Role == Role.Mentor)
            {
                var project = _projects.Find(x => x.Name == projectName);
                project.AddAttendanceSheet(new AttendanceSheet(meetingDate));
            }
        }
        public void AddAttendanceToAttendanceSheet(string projectName, DateTime meetingDate, bool attended, User student)
        {
            if (Role == Role.Mentor)
            {                
                    var project = _projects.Find(x => x.Name == projectName);
                    var attendanceSheet = project.AttendanceSheets.Find(x => x.MeetingDate == meetingDate);
                    attendanceSheet.AddAttendance(new Attendance(attended, student));       
            }
        }

        //An admin has the rights to grant any grants, and it's he/she's Job
        public void GrantGrant(User student, DateTime scholarshipTimestamp)
        {
            if (Role == Role.Admin && student.Role == Role.Student)
            {
                student.StudentData.ScholarshipTimestamp = scholarshipTimestamp;
            }
        }

        //Submitting reviews
        public void SubmitMentorsReview(User student, string mentorsReview)
        {         

            if (Role == Role.Mentor && student.Role == Role.Student)
            {
            student.StudentData.Evaluations.Add(new Evaluation(mentorsReview));
            }
        }
        public void SubmitInterviewReview(User student, int semesterId, string interviewReview)
        {
            var studentsEvaluationAtThatSemester = student.StudentData.Evaluations.Find(x => x.Semester.Id == semesterId);
            studentsEvaluationAtThatSemester.InterviewReview = interviewReview;
        }

        public void SetNewPassword(string newPassword)
        {
            Password = newPassword;
        }
    }
}
