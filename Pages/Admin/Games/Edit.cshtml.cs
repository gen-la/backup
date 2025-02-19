using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cybergames.Models;
using Cybergames.Data;
#nullable disable

namespace Cybergames.Pages.Admin.Games
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Game Game { get; set; }

        public SelectList CategoryOptions { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Game = await _context.Games.FindAsync(id);

            if (Game == null)
            {
                return NotFound();
            }

            CategoryOptions = new SelectList(new[]
            {
                "Action",
                "Adventure",
                "Arcade",
                "Casual",
                "Fighting",
                "FPS",
                "Platform",
                "Puzzle",
                "Racing",
                "RPG",
                "Simulation",
                "Sports"
            });

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                CategoryOptions = new SelectList(new[]
                {
                    "Action",
                    "Adventure",
                    "Arcade",
                    "Casual",
                    "Fighting",
                    "FPS",
                    "Platform",
                    "Puzzle",
                    "Racing",
                    "RPG",
                    "Simulation",
                    "Sports"
                });
                return Page();
            }

            _context.Attach(Game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(Game.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.ID == id);
        }
    }
}