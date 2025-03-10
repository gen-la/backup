using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class CheckoutModel : PageModel
{
    [BindProperty]
    public bool ShowConfirmationMessage { get; set; } = false;

    public void OnGet()
    {
        // This method is called when the page is loaded
    }

    public IActionResult OnPost()
    {
        // Show the confirmation message
        ShowConfirmationMessage = true;

        // Return the same page to display the message
        return Page();
    }
}