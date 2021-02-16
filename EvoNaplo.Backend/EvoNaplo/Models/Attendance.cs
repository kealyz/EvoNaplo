namespace EvoNaplo.Models
{
    public class Attendance
    {

        public int Id { get; set; }
        public bool Attended { get; set; }
        public User Student { get; set; }
        public bool IsActive { get; set; }

        public Attendance()
        {
            IsActive = true;
        }

        public Attendance(bool attended, User student)
        {

            Attended = attended;
            Student = student;
            IsActive = true;

        }
    }
}