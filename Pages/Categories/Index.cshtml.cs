using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication_DRUGSTORE.Data;
using WebApplication_DRUGSTORE.Models;
using WebApplication_DRUGSTORE.Models.ViewModels;

namespace WebApplication_DRUGSTORE.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly WebApplication_DRUGSTORE.Data.WebApplication_DRUGSTOREContext _context;

        public IndexModel(WebApplication_DRUGSTORE.Data.WebApplication_DRUGSTOREContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get; set; } = default!;

        public CategoryIndexData CategoryData { get; set; }
        public int CategoryID { get; set; }
        public int ProductID { get; set; }
        public async Task OnGetAsync(int? id, int? productID)
        {
            CategoryData = new CategoryIndexData();
            CategoryData.Categories = await _context.Category
            .Include(i => i.ProductCategories)
            .ThenInclude(c => c.Product)
            .ThenInclude(c => c.Review)
            .OrderBy(i => i.CategoryName)
            .ToListAsync();

            if (id != null)
            {
                CategoryID = id.Value;
                Category category = CategoryData.Categories
                .Where(i => i.ID == id.Value).Single();
                CategoryData.Products = category.ProductCategories.Select(bc => bc.Product);
            }
        }
    }
}
