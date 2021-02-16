using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvoNaplo.Models.DTO
{
    public class ProjectDTO
    {
        public string ProjectName { get; set; }
        public Uri SourceLink { get; set; }
        public string UsedTechnologies { get; set; }
        public bool IsActive { get; set; }
        public int SemesterId { get; set; }

        public ProjectDTO()
        {

        }

        public ProjectDTO(string projectName,Uri sourceLink,string usedTechnologies,int semesterId)
        {
            ProjectName = projectName;
            SourceLink = sourceLink;
            UsedTechnologies = usedTechnologies;
            SemesterId = semesterId;
        }

        public override string ToString()
        {
            return ($"{ProjectName}");
        }
    }
}
