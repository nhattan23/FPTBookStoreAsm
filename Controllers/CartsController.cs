using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FPTBookStore.Models;
using FPTBookStore.Data;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using FPTBookStore.Helper;

namespace FPTBookStore.Controllers
{
    public class CartsController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly ILogger<CartsController> _logger;

        public CartsController(ILogger<CartsController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            this.db = db;
        }
        
        [Authorize(Roles = "User")]
        public IActionResult AddToCart(long productid, int Soluong)
        {

            var cart = GetCartItems();
            var cartitem = cart.SingleOrDefault(p => p.book.IdBook == productid);
            if (cartitem == null)
            {
                var product = db.Book.SingleOrDefault(p => p.IdBook == productid);
                cart.Add(new CartItem() { quantity = Soluong, book = product });
            }
            else
            {
                cartitem.quantity += Soluong;
            }

            SaveCartSession(cart);

            return RedirectToAction("Cart");
        }


        [Route("/addcart/{productid:int}", Name = "addcart")]
        [Authorize(Roles = "User")]
        public IActionResult AddCart([FromRoute] long productid)
        {

            var cart = GetCartItems();
            var cartitem = cart.SingleOrDefault(p => p.book.IdBook == productid);
            if (cartitem == null)
            {
                var product = db.Book.SingleOrDefault(p => p.IdBook == productid);
                cart.Add(new CartItem() { quantity = 1, book = product });
            }
            else
            {
                cartitem.quantity++;
            }

            SaveCartSession(cart);

            return RedirectToAction("Cart");
        }


        [Authorize(Roles = "User")]
        [Route("/removecart/{productid:int}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] long productid)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.book.IdBook == productid);
            if (cartitem != null)
            {
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }


        [Authorize(Roles = "User")]
        public IActionResult RemoveBook(long bookID)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.book.IdBook == bookID);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity--;
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }


        public List<CartItem> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (data == null)
                {
                    data = new List<CartItem>();
                }
                return data;
            }
        }


       
        
        [Authorize(Roles = "User")]
        [Route("/cart", Name = "cart")]
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }



        [Authorize(Roles = "User")]
        [Route("/checkout")]
        public IActionResult CheckOut()
        {
            // Xử lý khi đặt hàng
            return View();
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

        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }
    }
}