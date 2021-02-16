using EvoNaplo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace EvoNaplo.DataAccessLayer
{
    public class EvoNaploContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public DbSet<User> Users2 { get; set; }
        public DbSet<StudentData> StudentDatas { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<AttendanceSheet> AttendanceSheets { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        public EvoNaploContext(DbContextOptions<EvoNaploContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.ApplyConfiguration(new User2Configuration());
        }
    }
}
