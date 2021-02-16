using System;
using System.Collections.Generic;

namespace EvoNaplo.Models
{
    public class AttendanceSheet
    {

        public int Id { get; set; }
        public DateTime MeetingDate { get; set; }
        public List<Attendance> AttendancesList { get; set; }
        public bool IsActive { get; set; }

        public AttendanceSheet(DateTime meetingDate)
        {

            MeetingDate = meetingDate;

            AttendancesList = new List<Attendance>();
            IsActive = true;
        }

        public void AddAttendance(Attendance attendance)
        {
            AttendancesList.Add(attendance);
        }
    }
}
