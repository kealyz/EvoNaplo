using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvoNaplo.Models.ViewModel
{
    public class SemesterStarterAdminProjektViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserViewModel> Students { get; set; }
    }
}
