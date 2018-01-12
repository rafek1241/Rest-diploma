using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using Newtonsoft.Json.Linq;
using Rest.Web.Engineer.Models;

namespace Rest.Web.Engineer.Logic
{
    public class CartProductLogic : LogicBase
    {
        private Cart GetCart(string token)
        {
            var guid = string.IsNullOrEmpty(token) ? new Guid() : new Guid(token);
            var yesterday = DateTime.Now.AddDays(-1);
            var cart = DbContext.Carts.SingleOrDefault(p => p.CookieToken == guid && p.ExpireDate >= yesterday);
            return cart;
        }

        public HttpResponseMessage AddProduct(Product viewProduct, string token)
        {
            var cart = GetCart(token);
            var product = DbContext.Products.Find(viewProduct.ProductId);

            try
            {
                var cartProduct = new CartProduct()
                {
                    Product = product,
                    Cart = cart,
                    Count = 1
                };
                using (var scope = new TransactionScope())
                {
                    cartProduct = DbContext.CartProducts.Add(cartProduct);
                    DbContext.SaveChanges();

                    cart.Products.Add(cartProduct);
                    DbContext.SaveChanges();

                    scope.Complete();
                }
            }
            catch (Exception er)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, er);
            }
            return Request.CreateResponse(HttpStatusCode.OK, cart);
        }
        public HttpResponseMessage RemoveProduct(long cartProductId, string token)
        {
            var cart = GetCart(token);

            if (cart == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Nie znaleziono koszyka o podanym tokenie.");
            }

            cart.Products.Remove(cart.Products.SingleOrDefault(p => p.CartProductId == cartProductId));
            DbContext.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, cart);
        }

        public HttpResponseMessage ChangeProductCount(JObject requestBody, string token /*, Product product, string option */)
        {
            var option = requestBody["option"].ToString();
            var product = requestBody["product"].ToObject<Product>();

            var cart = GetCart(token);

            if (cart == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Nie znaleziono koszyka o podanym tokenie.");
            }

            foreach (var cartProduct in cart.Products)
            {
                if (cartProduct.Product.ProductId == product.ProductId)
                {
                    switch (option.ToLower())
                    {
                        case CartProductOption.Increase:
                            cartProduct.Count++;
                            break;
                        case CartProductOption.Decrease:
                            if (cartProduct.Count > 1)
                            {
                                cartProduct.Count--;
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.MethodNotAllowed,
                                    "Nie można zmniejszyć ilości produktu poniżej dopuszczalnej liczby (1)");
                            }
                            break;
                    }
                }
            }

            DbContext.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, cart);
        }
    }

    public static class CartProductOption
    {
        public const string
            Increase = "increase",
            Decrease = "decrease";
    }
}
