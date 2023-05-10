using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;


namespace WorkshopImproved.Models
{
    public class UserBooks
    {
        public int Id { get; set; }

        [Display(Name = "User")]
        [StringLength(450)]
        [Required]
        public string? AppUser { get; set; }

        public int BookId { get; set; }

        public Book? Book { get; set; }
    }
}
