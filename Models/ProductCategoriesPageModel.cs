using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication_DRUGSTORE.Data;

namespace WebApplication_DRUGSTORE.Models
{
    public class ProductCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;
        public void PopulateAssignedCategoryData(WebApplication_DRUGSTOREContext context, Product product)
        {
            var allCategories = context.Category;
            var productCategories = new HashSet<int>();

            if (product is not null)
            {
                productCategories = new HashSet<int>(product.ProductCategories.Select(c => c.CategoryID));
            }

            AssignedCategoryDataList = new List<AssignedCategoryData>();

            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.ID,
                    Name = cat.CategoryName,
                    Assigned = productCategories.Contains(cat.ID)
                });
            }
        }
        public void UpdateProductCategories(WebApplication_DRUGSTOREContext context, string[] selectedCategories, Product productToUpdate)
        {
            if (selectedCategories == null)
            {
                productToUpdate.ProductCategories = new List<ProductCategory>();
                return;
            }
            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var productCategories = new HashSet<int>(productToUpdate.ProductCategories.Select(c => c.Category.ID));

            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!productCategories.Contains(cat.ID))
                    {
                        productToUpdate.ProductCategories.Add(
                        new ProductCategory
                        {
                            ProductID = productToUpdate.ID,
                            CategoryID = cat.ID
                        });
                    }
                }
                else
                {
                    if (productCategories.Contains(cat.ID))
                    {
                        ProductCategory courseToRemove = productToUpdate.ProductCategories.SingleOrDefault(i => i.CategoryID == cat.ID);
                        context.Remove(courseToRemove);

                    }
                }
            }
        }
    }
}
