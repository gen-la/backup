using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cybergames.Models;
using Cybergames.Data;

namespace Cybergames.Pages.Admin.Games
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Game> Games { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string searchString { get; set; }

        //public async Task OnGetAsync()
        //{
        //    Games = await _context.Games.ToListAsync();
        //}
        public async Task OnGetAsync(string searchString)
        {
            var games = from g in _context.Games
                         select g;

            if (!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(s => s.Title.Contains(searchString) ||
                s.Category.Contains(searchString));
            }


            Games = await games.ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);

            if (game != null)
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}