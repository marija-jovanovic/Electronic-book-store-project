using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkshopImproved.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Title { get; set; }

        [Display(Name = "Year Published")]
        public int YearPublished { get; set; }

        [Display(Name = "Number of pages")]
        public int NumPages { get; set; }

        public string? Description { get; set; }

        [StringLength(50)]
        public string? Publisher { get; set; }

        // [Display(Name = "Front Page")]
        //public string? FrontPage { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        [Display(Name = "Front Page")]
        public string? FrontPage { get; set; }


        [Display(Name = "Download Url")]
        public string? DownloadUrl { get; set; }
       // [NotMapped]
        //[Required(ErrorMessage = "The {0} field is required")]
        //[Display(Name = "Download Url")]
        //[DataType(DataType.Upload)]
      //  public string? DownloadUrl { get; set; }

        [Display(Name = "Author")]
        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        public ICollection<BookGenre>? Genres { get; set; }

        public ICollection<Review>? Reviews { get; set; }

        public ICollection<UserBooks>? Buyers { get; set; }

        [NotMapped]

        [Display(Name = "Average Rating")]
        public double Prosek
        {
            get
            {
                if (Reviews == null)
                    return 0;

                double average = 0;
                int i = 0;
                if (Reviews != null)
                {
                    foreach (var review in Reviews)
                    {
                        average += review.Rating;
                        i++;
                    }

                  return  Math.Round(average/i, 2);
                }
                return 0;
            }
        }

       // [NotMapped]
      //  public virtual ICollection<int> SelectedGenreList { get; set; }

        /*   public List<string> zanrovi
           {
               get
               {
                   List<string> optionList = new List<string>();
                   foreach(var zanr in Genres)
                   {
                       optionList.Add(zanr.Genre.GenreName);
                   }
                   return optionList;
               }
           } */
    }
}
