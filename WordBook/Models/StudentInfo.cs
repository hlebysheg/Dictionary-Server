using System;
using System.ComponentModel.DataAnnotations;

namespace WordBook.Models
{
	public class StudentInfo
	{
        [Key]
        public int Id { get; set; }

        [Required]
        public string DisplayName { get; set; }

        //ref
        public int StudentLoginId { get; set; }
        public StudentLogin StudentLogin { get; set; }

        public List<Dictionary> WordBooks { get; set; }
    }
}

