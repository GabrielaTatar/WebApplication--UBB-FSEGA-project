using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication_DRUGSTORE.Data;
using WebApplication_DRUGSTORE.Models;
using WebApplication_DRUGSTORE.Models.ViewModels;

namespace WebApplication_DRUGSTORE.Pages.Brands
{
    public class IndexModel : PageModel
    {
        private readonly WebApplication_DRUGSTORE.Data.WebApplication_DRUGSTOREContext _context;

        public IndexModel(WebApplication_DRUGSTORE.Data.WebApplication_DRUGSTOREContext context)
        {
            _context = context;
        }

        public IList<Brand> Brand { get; set; } = default!;

        public BrandIndexData BrandData { get; set; }
        public int BrandID { get; set; }
        public int ProductID { get; set; }

        public async Task OnGetAsync(int? id, int? productID)
        {
            BrandData = new BrandIndexData();
            BrandData.Brands = await _context.Brand
            .Include(i => i.Products)
            .ThenInclude(c => c.Review)
            .OrderBy(i => i.BrandName)
            .ToListAsync();

            if (id != null)
            {
                BrandID = id.Value;
                Brand brand = BrandData.Brands
                .Where(i => i.ID == id.Value).Single();
                BrandData.Products = brand.Products;
            }
        }
    }
}
