using Microsoft.AspNetCore.Mvc.Rendering;
using WorkshopImproved.Models;

namespace WorkshopImproved.ViewModels
{
    public class BookGenresCreateViewModel
    {

        public Book Book { get; set; }
        public IEnumerable<int>? SelectedGenres { get; set; }
        public IEnumerable<SelectListItem>? GenreList { get; set; }
    }
}
