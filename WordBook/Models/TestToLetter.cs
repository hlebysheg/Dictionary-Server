namespace WordBook.Models
{
    public class TestToLetter
    {
        public int Id { get; set; }
        //ref test
        public int TestId { get; set; }
        public Test Test { get; set; }
        //ref letter
        public int LetterId { get; set; }
        public Letter Letter { get; set; }
        //ans
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
        //author
    }
}
