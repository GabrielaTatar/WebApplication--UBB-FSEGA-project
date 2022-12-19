using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication_DRUGSTORE.Data;
using WebApplication_DRUGSTORE.Models;

namespace WebApplication_DRUGSTORE.Pages.Products
{
    [Authorize(Roles = "Admin")]

    public class EditModel : ProductCategoriesPageModel
    {
        private readonly WebApplication_DRUGSTORE.Data.WebApplication_DRUGSTOREContext _context;

        public EditModel(WebApplication_DRUGSTORE.Data.WebApplication_DRUGSTOREContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product
                .Include(b => b.Brand)
                .Include(b => b.Review)
                .Include(b => b.ProductCategories).ThenInclude(b => b.Category)
                .Include(b => b.Review)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Product == null)
            {
                return NotFound();
            }

            PopulateAssignedCategoryData(_context, Product);

            var reviewList = _context.Review.Select(x => new
            {
                x.ID,
                FullReview = x.Comment + " " + x.Stars
            });

            ViewData["BrandID"] = new SelectList(_context.Set<Brand>(), "ID", "BrandName");
            ViewData["ReviewID"] = new SelectList(_context.Set<Review>(), "ID", "FullReview");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            {
                if (id == null)
                {
                    return NotFound();
                }

                var productToUpdate = await _context.Product
                .Include(i => i.Brand)
                .Include(b => b.Review)
                .Include(i => i.ProductCategories)
                .ThenInclude(i => i.Category)
                .Include(i => i.Review)
                .FirstOrDefaultAsync(s => s.ID == id);

                if (productToUpdate == null)
                {
                    return NotFound();
                }

                if (await TryUpdateModelAsync<Product>(productToUpdate, "Product",
                i => i.Title, i => i.ReviewID,
                i => i.Price, i => i.ReleaseDate, i => i.BrandID))
                {
                    UpdateProductCategories(_context, selectedCategories, productToUpdate);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }

                UpdateProductCategories(_context, selectedCategories, productToUpdate);
                PopulateAssignedCategoryData(_context, productToUpdate);
                return Page();
            }
        }
    }

}
