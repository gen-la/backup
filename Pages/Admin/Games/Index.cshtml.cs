using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cybergames.Models;
using Cybergames.Data;

namespace Cybergames.Pages.Admin.Games
{
    //public class IndexModel : PageModel
    //{
    //    private readonly ApplicationDbContext _context;

    //    public IndexModel(ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }

    //   public IList<Game> Games { get;set; } = default!;

    //    public async Task OnGetAsync()
    //    {
    //        Games = await _context.Games.ToListAsync();
    //    }


    //}

    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Game> Games { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Games = await _context.Games.ToListAsync();
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