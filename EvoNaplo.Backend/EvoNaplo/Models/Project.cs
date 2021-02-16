using System;
using System.Collections.Generic;

namespace EvoNaplo.Models
{
    public class Project
    {

        public int Id { get; set; }
        public string  Name { get; set; }
        public Uri SourceLink { get; set; }
        public string  UsedTechnologies { get; set; }
        public int SemesterId { get; set; }
        public List<UserProject> userProjects { get; set;}

        public List<AttendanceSheet> AttendanceSheets { get; set; }
        public bool IsActive { get; set; }

        public Project(string name, Uri sourceLink, string usedTechnologies, int semesterId)
        {

            Name = name;
            SourceLink = sourceLink;
            UsedTechnologies = usedTechnologies;
            SemesterId = semesterId;

            AttendanceSheets = new List<AttendanceSheet>();
            IsActive = true;
        }

        /*public void AddStudent(Role role, User student)
        {
            if (role == Role.Mentor)
            {
                Students.Add(student);
            }
        }

        public void RemoveStudent(Role role, User student)
        {
            if (role == Role.Mentor)
            {
                Students.Add(student);
            }
        }*/

        public void AddAttendanceSheet(AttendanceSheet attendanceSheet)
        {
            AttendanceSheets.Add(attendanceSheet);
        }
    }
}
