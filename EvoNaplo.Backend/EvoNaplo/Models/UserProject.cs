using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvoNaplo.Models
{
    public class UserProject
    {
        public int id { get; set; }
        public int projectId { get; set; }
        public int userId { get; set; }

        public User user { get; set; }
        public Project project { get; set; }

        public UserProject(int userId,int projectId)
        {
            this.projectId = projectId;
            this.userId = userId;
        }
    }
}
