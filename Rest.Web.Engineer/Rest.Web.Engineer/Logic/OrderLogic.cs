using System;
using System.Net;
using System.Net.Http;
using Rest.Web.Engineer.Models;

namespace Rest.Web.Engineer.Logic
{
    public class OrderLogic : LogicBase
    {
        public HttpResponseMessage CreateOrder(Order order)
        {
            try
            {
                order.Cart = DbContext.Carts.Find(order.Cart.CartId);

                if (order.Cart == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound,
                        "Nie znaleziono zawartości koszyka.");
                }

                DbContext.Orders.Add(order);
                DbContext.SaveChanges();

            }
            catch (Exception err)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, err);
            }

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}
