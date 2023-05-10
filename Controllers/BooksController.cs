using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkshopImproved.Data;
using WorkshopImproved.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using WorkshopImproved.ViewModels;
using static System.Reflection.Metadata.BlobBuilder;
using Azure;
using Microsoft.Extensions.Hosting;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using NuGet.DependencyResolver;
using System.Collections.ObjectModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.IO;
using System.Drawing;
using System.Security.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WorkshopImproved.Controllers
{
    public class BooksController : Controller
    {
        private readonly WorkshopImprovedContext _context;
    
        private readonly IWebHostEnvironment webHostEnvironment;

        public BooksController(WorkshopImprovedContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
           
            webHostEnvironment = hostEnvironment;
        }

        // GET: Books
      
        public async Task<IActionResult> Index(string searchString, string searchGenre, string searchName, string searchSurname)
        {

            var workshopImprovedContext = _context.Book.Include(b => b.Author).Include(b => b.Reviews).Include(b => b.Genres).ThenInclude(b => b.Genre);
            var zanroviContext = _context.BookGenre.Include(b => b.Genre).Include(b => b.Book);
            var books = from m in workshopImprovedContext
                         select m;
            var zanrovis = from n in zanroviContext select n;

            if (!String.IsNullOrEmpty(searchString))
            {
              books = books.Where(s => s.Title!.Contains(searchString));
  
            }

            if (!String.IsNullOrEmpty(searchGenre))
            {
                zanrovis = zanrovis.Where(s => s.Genre.GenreName!.Contains(searchGenre));
                var innerJoinQuery =
                 from m in books
                 join n in zanrovis on m.Id equals n.BookId select new { m };
                ArrayList lista = new ArrayList();

                 foreach (var bookAndGenre in innerJoinQuery)
                {
 
                    lista.Add(bookAndGenre.m);
                }

                ViewBag.lista = lista;
            }

            if (!string.IsNullOrEmpty(searchName))
            {
                books = books.Where(s => s.Author.FirstName.Contains(searchName));
            }

            if (!string.IsNullOrEmpty(searchSurname))
            {
                books = books.Where(x => x.Author.LastName.Contains(searchSurname));
            }



            return View(await books.ToListAsync());
        }

        // GET: Books/Details/5
      //  [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author).Include(b => b.Reviews).Include(b => b.Genres).ThenInclude(b => b.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync(Book book)
        {
            
          
            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "FirstName", book.AuthorId);
           // ViewData["i"] = 0;
            //ViewBag.GenreList = _context.Set<Genre>();
            ViewData["Genres"] = new MultiSelectList(_context.Set<Genre>(), "Id", "GenreName");
            //product.Categories = new MultiSelectList(list, "ID", "Name", cat.CategorySelected.Select(c => c.ID).ToArray());

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] BookViewModel model)
        {

            if (ModelState.IsValid)
            {
               
                string uniqueFileName = UploadedFile(model);
                string uniqueFileNamee = UploadedPdf(model);

                Book book = new Book
                {
                    Title = model.Title,
                    YearPublished = model.YearPublished,
                    NumPages = model.NumPages,
                    Description = model.Description,
                    Publisher = model.Publisher,
                    FrontPage = uniqueFileName,
                    DownloadUrl = uniqueFileNamee,
                    AuthorId = model.AuthorId
                };
                _context.Add(book);


                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "FirstName", model.AuthorId);
           // ViewData["i"] = 0;
            return View();
        }

        //   [HttpPost]
        // public ActionResult GetPdf(string url)
        //{
        //  string filePath = "~/pdfs/" + url;
        // Response.Headers.Add("Content-Disposition", "inline; filename=");
        //return File(filePath, "application/pdf");
        //}



        public async Task<IActionResult> GetPdf(string url)
        {
            var path = Path.Combine(
            Directory.GetCurrentDirectory(), "wwwroot/pdfs/" + url);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/pdf", "Demo.pdf");
        }




        private string UploadedFile(BookViewModel model)
        {
            string uniqueFileName = null;
            string uniqueFileNamee = null;

            if (model.FrontPagee != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FrontPagee.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.FrontPagee.CopyTo(fileStream);
                    // = Image.FromStream(fileStream);
                  //  var image = Image.FromStream(fileStream);
                    
                }
            }

           
            return uniqueFileName;
        }



        private string UploadedPdf(BookViewModel model)
        {
            
            string uniqueFileNamee = null;

            if (model.DownloadUrll != null)
            {
                string uploadsFolderr = Path.Combine(webHostEnvironment.WebRootPath, "pdfs");
                uniqueFileNamee = Guid.NewGuid().ToString() + "_" + model.DownloadUrll.FileName;
                string filePathh = Path.Combine(uploadsFolderr, uniqueFileNamee);
                using (var fileStreamm = new FileStream(filePathh, FileMode.Create))
                {
                    model.DownloadUrll.CopyTo(fileStreamm);
                }
            }

            return uniqueFileNamee;
        }


        // GET: Books/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            // var book = await _context.Book.FindAsync(id);
            var book = _context.Book.Where(m => m.Id == id).Include(m => m.Genres).First();

            if (book == null)
            {
                return NotFound();
            }

            var genres = _context.Genre.AsEnumerable();
            genres = genres.OrderBy(s => s.GenreName);

            BookGenresEditViewModel viewmodel = new BookGenresEditViewModel
            {
                Book = book,
                GenreList = new MultiSelectList(genres, "Id", "GenreName"),
                SelectedGenres = book.Genres.Select(sa => sa.GenreId)
            };


            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "FirstName", book.AuthorId);
            //ViewData["i"] = 1;
            return View(viewmodel);
            // return View(book);
        }


        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id,BookGenresEditViewModel viewmodel)
        {
            if (id != viewmodel.Book.Id) { return NotFound(); }

            //  string uniqueFileName = null;
           // viewmodel.Book.FrontPage = "70ce3980-ef41-44a1-bac0-9784f2e554b5_kniga.jpg";


            if (ModelState.IsValid)
            {
                try
                {

                    string uniqueFileName = null;  //to contain the filename
                    if (viewmodel.slika != null)  //handle iformfile
                    {
                        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                        uniqueFileName = viewmodel.slika.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            viewmodel.slika.CopyTo(fileStream);
                        }
                    }

                    viewmodel.Book.FrontPage = uniqueFileName; //fill the image property


                    string uniqueFileNamee = null;

                    if (viewmodel.url != null)
                    {
                        string uploadsFolderr = Path.Combine(webHostEnvironment.WebRootPath, "pdfs");
                        uniqueFileNamee = Guid.NewGuid().ToString() + "_" + viewmodel.url.FileName;
                        string filePathh = Path.Combine(uploadsFolderr, uniqueFileNamee);
                        using (var fileStreamm = new FileStream(filePathh, FileMode.Create))
                        {
                            viewmodel.url.CopyTo(fileStreamm);
                        }
                    }

                 
                    viewmodel.Book.DownloadUrl = uniqueFileNamee;
                   
                    _context.Update(viewmodel.Book);
                    await _context.SaveChangesAsync();

                    IEnumerable<int> newGenreList = viewmodel.SelectedGenres;
                    IEnumerable<int> prevGenreList = _context.BookGenre.Where(s => s.BookId == id).Select(s => s.GenreId);
                    IQueryable<BookGenre> toBeRemoved = _context.BookGenre.Where(s => s.BookId == id);
                    if (newGenreList != null)
                    {
                        toBeRemoved = toBeRemoved.Where(s => !newGenreList.Contains(s.GenreId));
                        foreach (int genreId in newGenreList)
                        {
                            if (!prevGenreList.Any(s => s == genreId))
                            {
                                _context.BookGenre.Add(new BookGenre { GenreId = genreId, BookId = id });
                            }
                        }
                    }
                    _context.BookGenre.RemoveRange(toBeRemoved);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(viewmodel.Book.Id)) { return NotFound(); }
                    else { throw; }
                }
                return RedirectToAction(nameof(Index));
            }
            // var errors = ModelState.Values.SelectMany(v => v.Errors);
            /*else
            {
                var errors =
                from value in ModelState.Values
                where value.ValidationState == ModelValidationState.Invalid
                select value;
                return View();  // <-- I set a breakpoint here, and examine "errors"
            } */
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "FullName", viewmodel.Book.AuthorId);
           // ViewData["i"] = 1;
            return View(viewmodel);
        }



        // GET: Books/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'WorkshopImprovedContext.Book'  is null.");
            }
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return (_context.Book?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
