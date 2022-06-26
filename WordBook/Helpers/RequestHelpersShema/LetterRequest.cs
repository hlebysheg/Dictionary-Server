namespace WordBook.Helpers.RequestHelpersShema
{
    public class LetterRequest
    {
        public string Word { get; set; }
        public string Translate { get; set; }
        public string Anotation { get; set; }
        public int Id { get; set; }
        public int DictId { get; set; }
    }
}
