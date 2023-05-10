using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkshopImproved.Data;
using WorkshopImproved.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using WorkshopImproved.Areas.Identity.Data;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace WorkshopImproved.Controllers
{
    public class UserBooksController : Controller
    {
        private readonly WorkshopImprovedContext _context;

        public UserBooksController(WorkshopImprovedContext context)
        {
            _context = context;
        }

        // GET: UserBooks
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index()
        {
            var workshopImprovedContext = _context.UserBooks.Include(u => u.Book);
            return View(await workshopImprovedContext.ToListAsync());
        }

        // GET: UserBooks/Details/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserBooks == null)
            {
                return NotFound();
            }

            var userBooks = await _context.UserBooks
                .Include(u => u.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userBooks == null)
            {
                return NotFound();
            }

            return View(userBooks);
        }

 

        // GET: UserBooks/Create
        [Authorize(Roles = "User")]
        public IActionResult Create()
        {
            
                ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title");
                return View();
           
        }

        // POST: UserBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create([Bind("Id,AppUser,BookId")] UserBooks userBooks)
        {
           
                if (ModelState.IsValid)
                {
                    userBooks.AppUser = HttpContext.User.Identity.Name;
                    _context.Add(userBooks);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
              
                ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title", userBooks.BookId);
                return View(userBooks);
            
        }

        // GET: UserBooks/Edit/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int? id)
            {
                if (id == null || _context.UserBooks == null)
                {
                    return NotFound();
                }

                var userBooks = await _context.UserBooks.FindAsync(id);
                if (userBooks == null)
                {
                    return NotFound();
                }
                ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title", userBooks.BookId);
                return View(userBooks);

            }


        // POST: UserBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AppUser,BookId")] UserBooks userBooks)
        {
            if (id != userBooks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userBooks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserBooksExists(userBooks.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title", userBooks.BookId);
            return View(userBooks);
        }

        // GET: UserBooks/Delete/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserBooks == null)
            {
                return NotFound();
            }

            var userBooks = await _context.UserBooks
                .Include(u => u.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userBooks == null)
            {
                return NotFound();
            }

            return View(userBooks);
        }

        // POST: UserBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "User")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserBooks == null)
            {
                return Problem("Entity set 'WorkshopImprovedContext.UserBooks'  is null.");
            }
            var userBooks = await _context.UserBooks.FindAsync(id);
            if (userBooks != null)
            {
                _context.UserBooks.Remove(userBooks);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserBooksExists(int id)
        {
          return (_context.UserBooks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
