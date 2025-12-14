using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lutac_Ecaterina_Lab2.Data;
using Lutac_Ecaterina_Lab2.Models;

namespace Lutac_Ecaterina_Lab2.Pages.Borrowings
{
    public class EditModel : PageModel
    {
        private readonly Lutac_Ecaterina_Lab2.Data.Lutac_Ecaterina_Lab2Context _context;

        public EditModel(Lutac_Ecaterina_Lab2.Data.Lutac_Ecaterina_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing =  await _context.Borrowing
                .Include(b => b.Member)
                .Include(b => b.Book)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (borrowing == null)
            {
                return NotFound();
            }
            Borrowing = borrowing;
           ViewData["BookID"] = new SelectList(_context.Book, "ID", "Title");
           ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //se va include Author conform cu sarcina de la lab 2
            var borrowingToUpdate = await _context.Borrowing
            .Include(i => i.Member)
            .Include(i => i.Book)
            .FirstOrDefaultAsync(s => s.ID == id);
            if (borrowingToUpdate == null)
            {
                return NotFound();
            }
            //se va modifica AuthorID conform cu sarcina de la lab 2
            if (await TryUpdateModelAsync<Borrowing>(
            borrowingToUpdate,
            "Borrowing",
            i => i.MemberID, i => i.BookID, i => i.ReturnDate))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            
            return Page();
        }
        private bool BorrowingExists(int id)
        {
            return _context.Borrowing.Any(e => e.ID == id);
        }
    }
}
