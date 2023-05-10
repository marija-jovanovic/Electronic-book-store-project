﻿using System;
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
using System.Collections;
using NuGet.Protocol.Core.Types;

namespace WorkshopImproved.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly WorkshopImprovedContext _context;

        public ReviewsController(WorkshopImprovedContext context)
        {
            _context = context;
        }

        // GET: Reviews
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index()
        {
            var workshopImprovedContext = _context.Review.Include(r => r.Book);
            return View(await workshopImprovedContext.ToListAsync());
        }

        // GET: Reviews/Details/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Review == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .Include(r => r.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            review.AppUser = HttpContext.User.Identity.Name;
            return View(review);
        }

        // GET: Reviews/Create
        [Authorize(Roles = "User")]
        public IActionResult Create()
        {
            // ViewData["BookId"] = new SelectList(_context.UserBooks, "BookId");

            var userBooksContext = _context.UserBooks.Include(s => s.Book);
            var kupeniOdSite = from m in userBooksContext select m; //site zapisi od userBooks tabelata zemi gi
            var kupeniOdKorisnikot = kupeniOdSite.Where(s => s.AppUser!.Equals(HttpContext.User.Identity.Name));
            //kupeniOdKorsnikot se samo onie zapisi od userBooks cij sto kupec e logiraniot korisnik
            var bookContext = _context.Book;
            var knigi = from n in bookContext select n; //zemi gi site knigi sto postojat
            var innerJoin = from n in knigi join m in kupeniOdKorisnikot on n.Id equals m.BookId select new { n };

            //ArrayList lista = new ArrayList();

             ArrayList lista = new ArrayList();
             List<SelectListItem> selectlist = new List<SelectListItem>();

             foreach (var iterator in innerJoin)
             {
                 selectlist.Add(new SelectListItem { Text = iterator.n.Title, Value = iterator.n.Id.ToString() });

             }

             ViewBag.SelectList = selectlist; 


            //ViewData["BookId"] = new SelectList(lista, "Id", "Title");
            
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create([Bind("Id,BookId,AppUser,Comment,Rating")] Review review, string ddlname)
        {
            if (ModelState.IsValid)
            {
                //System.Security.Claims.ClaimsPrincipal currentUser = this.User;
                //bool isAdmin = currentUser.IsInRole("User");
                //var id = currentUser.; // Get user id:

                review.AppUser = HttpContext.User.Identity.Name;



                var userBooksContext = _context.UserBooks.Include(s => s.Book);
                var kupeniOdSite = from m in userBooksContext select m; //site zapisi od userBooks tabelata zemi gi
                var kupeniOdKorisnikot = kupeniOdSite.Where(s => s.AppUser!.Equals(HttpContext.User.Identity.Name));
                //kupeniOdKorsnikot se samo onie zapisi od userBooks cij sto kupec e logiraniot korisnik
                var bookContext = _context.Book;
                var knigi = from n in bookContext select n; //zemi gi site knigi sto postojat
                var innerJoin = from n in knigi join m in kupeniOdKorisnikot on n.Id equals m.BookId select new { n };

                //ArrayList lista = new ArrayList();

                 ArrayList lista = new ArrayList();
                 List<SelectListItem> selectlist = new List<SelectListItem>();

                 foreach (var iterator in innerJoin)
                 {
                     selectlist.Add(new SelectListItem { Text = iterator.n.Title, Value = iterator.n.Id.ToString() });

                 }

                 ViewBag.SelectList = selectlist;
               review.BookId = Int16.Parse(ddlname);

                // review.BookId = Convert.ToInt32(ViewBag.SelectList.selectedItem.value); 
                // ViewData["BookId"] = new SelectList(innerJoin, "Id", "Title", review.BookId);



                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


           

            //ViewData["BookId"] = new SelectList(lista, "Id", "Title", review.BookId);

            // ViewData["BookId"] = new SelectList(query, "Id", "Title");
            return View(review);
        }

        // GET: Reviews/Edit/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Review == null)
            {
                return NotFound();
            }

            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
           
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title", review.BookId);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,AppUser,Comment,Rating")] Review review)
        {
            if (id != review.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               review.AppUser = HttpContext.User.Identity.Name;
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title", review.BookId);
            return View(review);
        }

        // GET: Reviews/Delete/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Review == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .Include(r => r.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            review.AppUser = HttpContext.User.Identity.Name;
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Review == null)
            {
                return Problem("Entity set 'WorkshopImprovedContext.Review'  is null.");
            }
            var review = await _context.Review.FindAsync(id);
            review.AppUser = HttpContext.User.Identity.Name;
            if (review != null)
            {
                _context.Review.Remove(review);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
          return (_context.Review?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
