using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class BookCopy
    {
        public virtual int Id { get; set; }
        public virtual int BookId { get; set; }
        public virtual Status Status { get; set; }
        [JsonIgnore]

        public virtual Book Book { get; set; }
    }
}
