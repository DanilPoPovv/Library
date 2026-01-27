namespace WebApplication1.Requests
{
    public class BookUpdateRequest
    {
        public string Name { get; set; }
        public virtual string Title { get; set; }
        public virtual string Author { get; set; }
        public virtual string Genre { get; set; }
        public virtual int Year { get; set; }
    }
}
