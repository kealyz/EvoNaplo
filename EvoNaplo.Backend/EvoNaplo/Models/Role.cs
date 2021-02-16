using Microsoft.AspNetCore.Identity;
using System;

namespace EvoNaplo.Models
{
    public enum Role //: IdentityRole<Guid>
    {
        Admin,
        Mentor,
        Student
    }
}