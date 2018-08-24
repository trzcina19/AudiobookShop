using Shop.DAL;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Infrastructure
{
    public class CartMenager : Controller
    {
        private AudiobooksContext db;
        private ISessionMenager session { get; set; }

        public CartMenager(ISessionMenager session, AudiobooksContext db)
        {
            this.session = session;
            this.db = db;
        }

        public List<CartItem> DownloadCart()
        {
            List<CartItem> cart;

            if (session.Get<List<CartItem>>(Consts.CartSessionKey) == null)
            {
                cart = new List<CartItem>();
            }
            else
            {
                cart = session.Get<List<CartItem>>(Consts.CartSessionKey) as List<CartItem>; 
            }
            return cart;
        }


        public void AddToCart(int AudiobookId)
        {
            var cart = DownloadCart();
            var cartItem = cart.Find(k => k.Audiobook.AudiobookId == AudiobookId);

            if (cartItem != null)
                cartItem.Amount++;
            else
            {
                var audiobookToAdd = db.Audiobooks.Where(k => k.AudiobookId == AudiobookId).SingleOrDefault();

                if (audiobookToAdd != null)
                {
                    var newCartItem = new CartItem()
                    {
                        Audiobook = audiobookToAdd,
                        Amount = 1,
                        ValueCart = audiobookToAdd.PriceAudiobook
                    };
                    cart.Add(newCartItem);
                }
            }
            session.Set(Consts.CartSessionKey, cart);
        }

        public int RemoveFromCart(int AudiobookId)
        {
            var cart = DownloadCart();
            var cartItem = cart.Find(k => k.Audiobook.AudiobookId == AudiobookId);

            if (cartItem != null)
            {
                if (cartItem.Amount > 1)
                {
                    cartItem.Amount--;
                    return cartItem.Amount;

                }
                else
                {
                    cart.Remove(cartItem);
                }
            }
            return 0;
        }



        public decimal DdownloadValueOfTheCart()
        {
            var cart = DownloadCart();
            return cart.Sum(k => (k.Audiobook.PriceAudiobook * k.Amount));

        }

        public int DownloadAmountCartItem()
        {
            var cart = DownloadCart();
            int amount = cart.Sum(k => k.Amount);
            return amount;
        }

        public  Order CreateOrder(Order newOrder, string userId)
        {
            var cart = DownloadCart();
            newOrder.DateAdded = DateTime.Now;
            newOrder.UserId = userId;

            db.Orders.Add(newOrder);

            if (newOrder.PositionsOrder == null)
            {
                newOrder.PositionsOrder = new List<PositionOrder>();
            }

            decimal cartValue = 0;

            foreach (var elementCart in cart)
            {

                var newPositionOrder = new PositionOrder()
                {
                    AudiobookId = elementCart.Audiobook.AudiobookId,
                    Amount = elementCart.Amount,
                    TotalPrice = elementCart.Audiobook.PriceAudiobook
                };

                cartValue += (elementCart.Amount * elementCart.Audiobook.PriceAudiobook);
                newOrder.PositionsOrder.Add(newPositionOrder);

            
            }

            newOrder.OrderValue = cartValue;
            db.SaveChanges();
            return newOrder;

        }


        public void EmptyCart()
        {
            session.Set<List<PositionOrder>>(Consts.CartSessionKey, null);
        }
    }
}