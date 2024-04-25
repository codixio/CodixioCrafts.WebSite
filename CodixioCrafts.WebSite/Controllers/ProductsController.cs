using CodixioCrafts.WebSite.Models;
using CodixioCrafts.WebSite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodixioCrafts.WebSite.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		public ProductsController(JsonFileProductsService productsService) => this.ProductService = productsService;

		public JsonFileProductsService ProductService { get; }

		[HttpGet]
		public IEnumerable<Product> Get() => this.ProductService.GetProducts();

		//[HttpPatch] "[FromBody]"
		[Route("Rate")]
		[HttpGet]
		public ActionResult Get(
			[FromQuery] string productId,
			[FromQuery] int Rating)
		{
			this.ProductService.AddRating(productId, Rating);
			return Ok();
		}
	}
}
