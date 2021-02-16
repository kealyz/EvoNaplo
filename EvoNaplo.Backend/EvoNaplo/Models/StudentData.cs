using System;
using System.Collections.Generic;

namespace EvoNaplo.Models
{
    public class StudentData
    {

        public int Id { get; set; }
        public DateTime ScholarshipTimestamp { get; set; }
        public List<Evaluation> Evaluations { get; set; }

        public StudentData()
        {
            Evaluations = new List<Evaluation>();
        }
    }
}