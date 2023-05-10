using Microsoft.AspNetCore.Mvc.Rendering;
using WorkshopImproved.Models;

namespace WorkshopImproved.ViewModels
{
    public class BookGenresEditViewModel
    {
            
        public IFormFile url { get; set; }

        public IFormFile slika { get; set; }

        public Book? Book { get; set; }
        public IEnumerable<int>? SelectedGenres { get; set; }
        public IEnumerable<SelectListItem>? GenreList { get; set; }


    }
}
