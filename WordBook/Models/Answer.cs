using System.ComponentModel.DataAnnotations;

namespace WordBook.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Word { get; set; }
        [Required]
        public string Translate { get; set; }
        public bool Correct { get; set; }

        //ref
    }
}
