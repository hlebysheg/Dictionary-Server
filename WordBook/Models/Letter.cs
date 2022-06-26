using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordBook.Models
{
    [Table("Letter")]
    public class Letter
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Word { get; set; }

        [Required]
        public string Translate {get; set; }

        public string Anotation { get; set; }

        //ref
        public int DictionaryId { get; set; }
        public Dictionary Dictionary { get; set; }
    }
}
