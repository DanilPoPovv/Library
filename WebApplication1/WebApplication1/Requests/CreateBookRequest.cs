namespace WebApplication1.Requests
{
    public class CreateBookRequest
    {
        public virtual string Name { get; set; }
        public virtual string Author { get; set; }
        public virtual string Genre { get; set; }
        public virtual int Year { get; set; }
    }
}
