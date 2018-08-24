using Hangfire;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Shop.App_Start;
using Shop.DAL;
using Shop.Infrastructure;
using Shop.Models;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace Shop.Controllers
{
    public class CartController : Controller
    {

        private CartMenager cartMenager;
        private ISessionMenager sessionMenager { get; set; }
        private AudiobooksContext db;

        public CartController()
        {
            db = new AudiobooksContext();
            sessionMenager = new SessionMenager();
            cartMenager = new CartMenager(sessionMenager, db);
        }


        // GET: Cart
        public ActionResult Index()
        {

            var carItemS = cartMenager.DownloadCart();
            var totalPrice = cartMenager.DdownloadValueOfTheCart();
            CartViewModel cartVM = new CartViewModel()
            {
                CartItemS = carItemS,
                TotalPrice = totalPrice,
            };
            return View(cartVM);

        }

        public ActionResult AddToCart(int id)
        {
            cartMenager.AddToCart(id);
            return RedirectToAction("Index");
        }

        public int DownloadAmountElementsInCart()
        {
            return cartMenager.DownloadAmountCartItem();
        }

        public ActionResult RemoveFromCart(int AudiobookId2)
        {
            int amountItems = cartMenager.RemoveFromCart(AudiobookId2);
            int amountItemsCart = cartMenager.DownloadAmountCartItem();
            decimal valueCart = cartMenager.DdownloadValueOfTheCart();

            var result = new CartRemovalViewModel
            {
                IdItemRemoved = AudiobookId2,
                AmountItemsRemoved = amountItems,
                CartPriceTotal = valueCart,
                CartAmountItems = amountItemsCart,

            };
            return Json(result);
        }

        public async Task<ActionResult> Pay()
        {
            if (Request.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());  //gsy asynchroniczneie musi zwracac task
                var order = new Order 
                {
                    Name = user.UserData.Name,
                    Surname = user.UserData.Surname,
                    Street = user.UserData.Street,
                    City = user.UserData.City,
                    PostalCode = user.UserData.PostalCode,
                    Email = user.UserData.Email,
                    PhoneNumber = user.UserData.PhoneNumber
                };
                return View(order);
            }
            else
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Pay", "Cart") });
        }

        [HttpPost]
        public async Task<ActionResult> Pay(Order orderDetails)
        {
            if (ModelState.IsValid)
            {
                // pobieramy iduzytkownika aktualnie zalogowanego
                var userId = User.Identity.GetUserId();

                // utworzenie biektu zamowienia na podstawie tego co mamy w koszyku
                var newOrder = cartMenager.CreateOrder(orderDetails, userId);

                // szczegóły użytkownika - aktualizacja danych 
                var user = await UserManager.FindByIdAsync(userId);
                TryUpdateModel(user.UserData);
                await UserManager.UpdateAsync(user);

                // opróżnimy nasz koszyk zakupów
                cartMenager.EmptyCart();


                var order = db.Orders.Include("PositionsOrder").Include("PositionsOrder.Audiobook")
                    .SingleOrDefault(o=>o.OrderId == newOrder.OrderId);
                OrderConfirmationEmail email = new OrderConfirmationEmail();
                email.To = order.Email;
                email.From = "trzcinski.michal@interia.pl";
                email.OrderValue = order.OrderValue;
                email.OrderId = order.OrderId;
                email.PositionsOrder = order.PositionsOrder;
                email.Send();

                return RedirectToAction("OrderConfirmation");
            }
            else
                return View(orderDetails);
        }

        public ActionResult OrderConfirmation()
        {
            var name = User.Identity.Name;
            return View();
        }

        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}