using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cybergames.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cybergames.Data;

namespace Cybergames.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CategoryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public string CategoryName { get; set; }
        public IList<Game> Games { get; set; } = new List<Game>();

        public async Task<IActionResult> OnGetAsync(string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                return RedirectToPage("/Index");
            }

            CategoryName = category;

            Games = await _context.Games
                .Where(g => g.Category == category)
                .ToListAsync();

            return Page();
        }
    }
}