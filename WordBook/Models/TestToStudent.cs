﻿namespace WordBook.Models
{
    public class TestToStudent
    {
        public int Id { get; set; }

        public int StudentId { get; set;}
        public StudentLogin StudentLogin { get; set; }

        public int TestId { get; set; }
        public Test Test { get; set; }

    }
}
