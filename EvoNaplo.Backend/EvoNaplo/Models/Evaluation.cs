

namespace EvoNaplo.Models
{
    public class Evaluation
    {

        public int Id { get; set; }
        public Semester Semester { get; set; }
        public string MentorsReview { get; set; }
        public string InterviewReview { get; set; }
        public bool IsActive { get; set; }

        public Evaluation(string mentorsReview)
        {

            MentorsReview = mentorsReview;
            IsActive = true;
        }                
    }
}
