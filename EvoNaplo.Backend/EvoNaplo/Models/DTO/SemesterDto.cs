using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvoNaplo.Models.DTO
{
    public class SemesterDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DemoDate { get; set; }



        public override string ToString()
        {
            return ($"Szemeszter kezdete: {StartDate} Szemeszter vége: {EndDate} Demó ideje: {DemoDate}");
        }
    }
}
