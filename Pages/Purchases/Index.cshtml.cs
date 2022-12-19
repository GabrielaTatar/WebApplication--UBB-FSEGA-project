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
    public class IndexModel : PageModel
    {
        private readonly WebApplication_DRUGSTORE.Data.WebApplication_DRUGSTOREContext _context;

        public IndexModel(WebApplication_DRUGSTORE.Data.WebApplication_DRUGSTOREContext context)
        {
            _context = context;
        }

        public IList<Purchase> Purchase { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Purchase != null)
            {
                Purchase = await _context.Purchase
                .Include(b => b.Product)
                .ThenInclude(b => b.Review)
                .Include(b => b.Member).ToListAsync();
            }
        }
    }
}
