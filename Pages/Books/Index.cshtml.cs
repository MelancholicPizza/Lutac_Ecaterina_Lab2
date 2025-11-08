using Lutac_Ecaterina_Lab2.Data;
using Lutac_Ecaterina_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lutac_Ecaterina_Lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Lutac_Ecaterina_Lab2.Data.Lutac_Ecaterina_Lab2Context _context;

        public IndexModel(Lutac_Ecaterina_Lab2.Data.Lutac_Ecaterina_Lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Book = await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .ToListAsync();
        }
    }
}
