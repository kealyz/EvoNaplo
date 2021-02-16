using Microsoft.AspNetCore.Routing;
using System;

namespace EvoNaplo.Models
{
    public class Semester
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DemoDate { get; set; }

        public bool IsActive { get; set; }

        public Semester(DateTime startDate, DateTime endDate, DateTime demoDate)

        {

            StartDate = startDate;
            EndDate = endDate;
            DemoDate = demoDate;
            IsActive = true;

        }
    }
}
