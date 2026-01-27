using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Loan
    {
        public virtual string Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual int BookCopyId { get; set; }
        public virtual DateTime LoanDate { get; set; }
        public virtual DateTime? ReturnDate { get; set; }
        [JsonIgnore]
        public virtual BookCopy BookCopy { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
