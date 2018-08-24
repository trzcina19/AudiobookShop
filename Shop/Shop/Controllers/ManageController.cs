using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Shop.App_Start;
using Shop.DAL;
using Shop.Infrastructure;
using Shop.Models;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;





namespace Shop.Controllers
{

    [Authorize]
    public class ManageController : Controller
    {

        private AudiobooksContext db = new AudiobooksContext();
        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            Error
        }
        // GET: Manage


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

        private IAuthenticationManager IauthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        // GET: Manage
        public async Task<ActionResult> Index(ManageMessageId? message)
        {

            var name = User.Identity.Name;

            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            if (User.IsInRole("Admin"))
                ViewBag.UserIsAdmin = true;
            else
                ViewBag.UserIsAdmin = false;

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }

            var model = new ManageCredentialsViewModel
            {

                Message = message,
                UserData = user.UserData
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeProfile([Bind(Prefix = "UserData")]UserData UserData)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                user.UserData = UserData;
                var result = await UserManager.UpdateAsync(user);

                AddErrors(result);
            }

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword([Bind(Prefix = "ChangePasswordViewModel")]ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            var message = ManageMessageId.ChangePasswordSuccess;
            return RedirectToAction("Index", new { Message = message });
        }



        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("password-error", "Błędne hasło!");
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));

        }

        public ActionResult ListOrders()
        {


            bool isAdmin = User.IsInRole("Admin");
            ViewBag.UserIsAdmin = isAdmin;

            IEnumerable<Order> userOrder; // zamówieniea użytkownial

            // Dla administratora zwracamy wszystkie zamowienia
            if (isAdmin)
            {
                userOrder = db.Orders.Include("PositionsOrder").OrderByDescending(o => o.DateAdded).ToArray();
            }
            else
            {
                var userId = User.Identity.GetUserId();
                userOrder = db.Orders.Where(o => o.UserId == userId).Include("PositionsOrder").OrderByDescending(o => o.DateAdded).ToArray();
            }

            return View(userOrder);
        }

        // ChangeStateOrders  // wywołana przez ajax asynchronicznie
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public OrderStatus ChangeStateOrders(Order order)
        {
            Order orderToModify = db.Orders.Find(order.OrderId);
            orderToModify.OrderStatus = order.OrderStatus;
            db.SaveChanges();

            if (orderToModify.OrderStatus == OrderStatus.Zrealizowane)
            {

                OrderRealizedEmail email = new OrderRealizedEmail();
                email.To = orderToModify.Email;
                email.From = "xxx@gmail.com";
                email.OrderId = orderToModify.OrderId;
                email.Send();
            }
            return order.OrderStatus;
        }


        [Authorize(Roles = "Admin")]
        public ActionResult AddAudiobook(int? AudiobookId, bool? confirmation)
        {
            Audiobook audiobook;
            if (AudiobookId.HasValue)
            {
                ViewBag.EditMode = true;
                audiobook = db.Audiobooks.Find(AudiobookId);
            }
            else
            {
                ViewBag.EditMode = false;
                audiobook = new Audiobook();
            }

            var result = new EditAudiobookViewModel();
            result.Categories = db.Categories.ToList();
            result.Audiobook = audiobook;
            result.Confirmation = confirmation;

            return View(result);
        }



        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddAudiobook(EditAudiobookViewModel model, HttpPostedFileBase file)
        {
            if (model.Audiobook.AudiobookId > 0)
            {
                // modyfikacja Audiobook
                db.Entry(model.Audiobook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AddAudiobook", new { confirmation = true });
            }
            else
            {
                // Sprawdzenie, czy użytkownik wybrał plik
                if (file != null && file.ContentLength > 0)
                {
                    if (ModelState.IsValid)
                    {
                        // Generowanie pliku
                        var fileExt = Path.GetExtension(file.FileName);
                        var filename = Guid.NewGuid() + fileExt;

                        var path = Path.Combine(Server.MapPath(AppConfig.ImagesFolderRelative), filename);
                        file.SaveAs(path);

                        model.Audiobook.ImageFileName = filename;
                        model.Audiobook.DateAdded = DateTime.Now;

                        db.Entry(model.Audiobook).State = EntityState.Added;
                        db.SaveChanges();

                        return RedirectToAction("AddAudiobook", new { confirmation = true });
                    }
                    else
                    {
                        var categories = db.Categories.ToList();
                        model.Categories = categories;
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Nie wskazano pliku");
                    var categories = db.Categories.ToList();
                    model.Categories = categories;
                    return View(model);
                }
            }

        }


        [Authorize(Roles = "Admin")]
        public ActionResult LackAudiobook(int AudiobookId)
        {
            var audiobook = db.Audiobooks.Find(AudiobookId);
            audiobook.LackAudiobook = true;
            db.SaveChanges();

            return RedirectToAction("AddAudiobook", new { confirmation = true });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ShowAudiobook(int AudiobookId)
        {
            var Audiobook = db.Audiobooks.Find(AudiobookId);
            Audiobook.LackAudiobook = false;
            db.SaveChanges();

            return RedirectToAction("AddAudiobook", new { confirmation = true });
        }


    }
}