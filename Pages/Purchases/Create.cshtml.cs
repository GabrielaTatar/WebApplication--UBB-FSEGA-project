using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication_DRUGSTORE.Data;
using WebApplication_DRUGSTORE.Models;

namespace WebApplication_DRUGSTORE.Pages.Purchases
{
    public class CreateModel : PageModel
    {
        private readonly WebApplication_DRUGSTORE.Data.WebApplication_DRUGSTOREContext _context;

        public CreateModel(WebApplication_DRUGSTORE.Data.WebApplication_DRUGSTOREContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var productList = _context.Product
                .Include(b => b.Review)
                .Select(x => new
                {
                    x.ID,
                    ProductFullName = x.Title + " - " + x.Review.Comment + " " + x.Review.Stars
                });

            ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName");
            ViewData["ProductID"] = new SelectList(productList, "ID", "ProductFullName");
            return Page();
        }

        [BindProperty]
        public Purchase Purchase { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Purchase.Add(Purchase);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
