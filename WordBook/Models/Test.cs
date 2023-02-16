using System.ComponentModel.DataAnnotations;

namespace WordBook.Models
{
    public enum Score
    {
        E,
        D,
        C,
        B,
        A,
        S
    }
    public class Test
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? FinalTime { get; set; }
        public Score? Score { get; set; }
        //ref
        //public Student Author { get; set; }
        public List<Answer> Answer { get; set; }
    }
}
