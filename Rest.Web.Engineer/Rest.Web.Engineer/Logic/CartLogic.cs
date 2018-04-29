using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Rest.Web.Engineer.Models;

namespace Rest.Web.Engineer.Logic
{
    public class CartLogic : LogicBase
    {
        public HttpResponseMessage Get(string token)
        {
            token = token.Replace("00000000-0000-0000-0000-000000000000", "");
            var guid = string.IsNullOrEmpty(token) ? Guid.NewGuid() : new Guid(token);

            return GetCart(guid);
        }

        public HttpResponseMessage Put(Cart cart)
        {
            var item = DbContext.Carts.Find(cart.CartId);
            if (item == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            item = cart;
            DbContext.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, cart);
        }

        private HttpResponseMessage GetCart(Guid guid)
        {
            var yesterday = DateTime.Now.AddDays(-1);
            var cart = DbContext.Carts.SingleOrDefault(p => p.CookieToken == guid && p.ExpireDate >= yesterday);

            if (cart != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, cart);
            }

            var tomorrow = DateTime.Now.AddDays(1);
            cart = DbContext.Carts.Add(new Cart()
            {
                CookieToken = guid,
                ExpireDate = tomorrow,
            });

            DbContext.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Created, cart);
        }

        public HttpResponseMessage Delete(string token)
        {
            var guid = string.IsNullOrEmpty(token) ? Guid.NewGuid() : new Guid(token);
            var yesterday = DateTime.Now.AddDays(-1);
            var cart = DbContext.Carts.SingleOrDefault(p => p.CookieToken == guid && p.ExpireDate >= yesterday);

            if (cart == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            DbContext.Carts.Remove(cart);
            DbContext.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}