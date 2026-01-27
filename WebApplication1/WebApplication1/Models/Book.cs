using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Book
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Author { get; set; }
        public virtual string Genre { get; set; }
        public virtual int Year { get; set; }
        [JsonIgnore]
        public virtual List<BookCopy> Copies { get; set; }
    }
}
