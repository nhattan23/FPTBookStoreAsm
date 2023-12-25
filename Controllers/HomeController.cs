using FPTBookStore.Data;
using FPTBookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using X.PagedList;
using FPTBookStore.Helper;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;

namespace FPTBookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult Index(int? page)
        {
            
                int pageSize = 8;
                int pageNumber = page == null || page < 0 ? 1 : page.Value;

                if (db == null)
                {
                    throw new Exception("Database context (db) is null.");
                }

                var lstBook = db.Book.AsNoTracking().OrderBy(x => x.BookName).ToList();

                if (lstBook == null)
                {
                    throw new Exception("The list of books (lstBook) is null.");
                }

                var cartItems = GetCartItems();

                cartItems ??= new List<CartItem>();

                PagedList<Book> lst = new PagedList<Book>(lstBook, pageNumber, pageSize);

                ViewBag.CartItems = cartItems;
                return View(lst);
            
            
        }


        public IActionResult BookByCategory(long genre)
        {
            List<Book> lstBook = db.Book.Where(x => x.Genre == genre).OrderBy(x => x.BookName).ToList();
            return View(lstBook);
        }

        public IActionResult BookDetails(long bookID)
        {
            var b = db.Book.SingleOrDefault(x => x.IdBook == bookID);
            var books = db.Book.Where(x => x.IdBook == bookID).ToList();
            ViewBag.books = b;
            return View(b);
        }

        public const string CARTKEY = "cart";
        List<CartItem> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}