using CodixioCrafts.WebSite.Models;
using CodixioCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodixioCrafts.WebSite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public JsonFileProductsService ProductService;
        public IEnumerable<Product> Products { get; private set; }

        public IndexModel(
            ILogger<IndexModel> logger,
            JsonFileProductsService productsService)
        {
            _logger = logger;
            ProductService = productsService;
			Products = Enumerable.Empty<Product>();
		}

        public void OnGet()
        {
            Products = ProductService.GetProducts();
        }
    }
}
