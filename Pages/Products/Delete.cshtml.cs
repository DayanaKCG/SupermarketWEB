using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Products
{
	public class DeleteModel : PageModel
	{
		private readonly SupermarketContext _context;
		public DeleteModel(SupermarketContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Product Product { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null || _context.Products == null)
			{
				return NotFound();
			}
			var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
			if (product == null)
			{
				return NotFound();
			}
			else
			{
				Product = product;
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int? id)
		{
			if (id == null || _context.Products == null)
			{
				return NotFound();
			}
			var product = await _context.Products.FindAsync(id);

			if (product != null)
			{
				Product = product;
				_context.Products.Remove(product);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("./Index");
		}
	}
}
