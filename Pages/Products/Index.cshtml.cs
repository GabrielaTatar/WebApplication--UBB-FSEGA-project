using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication_DRUGSTORE.Data;
using WebApplication_DRUGSTORE.Models;

namespace WebApplication_DRUGSTORE.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly WebApplication_DRUGSTORE.Data.WebApplication_DRUGSTOREContext _context;

        public IndexModel(WebApplication_DRUGSTORE.Data.WebApplication_DRUGSTOREContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get; set; } = default!;
        public ProductData ProductD { get; set; }
        public int ProductID { get; set; }
        public int CategoryID { get; set; }


        public string TitleSort { get; set; }
        public string ReviewSort { get; set; }

        public string CurrentFilter { get; set; }


        public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
        {
            ProductD = new ProductData();

            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ReviewSort = String.IsNullOrEmpty(sortOrder) ? "review_desc" : "";

            CurrentFilter = searchString;

            ProductD.Products = await _context.Product
                .Include(b => b.Brand)
                .Include(b => b.Review)
                .Include(b => b.ProductCategories)
                .ThenInclude(b => b.Category)
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                ProductD.Products = ProductD.Products.Where(s => s.Review.FullReview.Contains(searchString)
                                                              || s.Title.Contains(searchString));

                if (id != null)
                {
                    ProductID = id.Value;
                    Product product = ProductD.Products
                    .Where(i => i.ID == id.Value).Single();

                    ProductD.Categories = product.ProductCategories.Select(s => s.Category);
                }

                switch (sortOrder)
                {
                    case "title_desc":
                        ProductD.Products = ProductD.Products.OrderByDescending(s => s.Title);
                        break;
                    case "review_desc":
                        ProductD.Products = ProductD.Products.OrderByDescending(s => s.Review.FullReview);
                        break;

                }
            }
        }
    }
}
