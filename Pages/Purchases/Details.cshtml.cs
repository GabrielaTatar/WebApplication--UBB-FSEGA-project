using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication_DRUGSTORE.Data;
using WebApplication_DRUGSTORE.Models;

namespace WebApplication_DRUGSTORE.Pages.Purchases
{
    public class DetailsModel : PageModel
    {
        private readonly WebApplication_DRUGSTORE.Data.WebApplication_DRUGSTOREContext _context;

        public DetailsModel(WebApplication_DRUGSTORE.Data.WebApplication_DRUGSTOREContext context)
        {
            _context = context;
        }

      public Purchase Purchase { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Purchase == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase.FirstOrDefaultAsync(m => m.ID == id);
            if (purchase == null)
            {
                return NotFound();
            }
            else 
            {
                Purchase = purchase;
            }
            return Page();
        }
    }
}
