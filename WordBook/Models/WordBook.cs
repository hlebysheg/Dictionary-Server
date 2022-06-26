using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordBook.Models
{
    [Table("Dictionary")]
    public class Dictionary
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20), MinLength(2)]
        public string language { get; set; }

        [MaxLength(20), MinLength(2)]
        public string Title { get; set; }

        //ref
        public Student Author { get; set; }
        public List<Letter> Letters { get; set; }
    }
}
