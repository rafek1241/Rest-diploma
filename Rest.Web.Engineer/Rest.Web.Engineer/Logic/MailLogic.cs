using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Rest.Web.Engineer.Models;

namespace Rest.Web.Engineer.Logic
{
    public class MailLogic : LogicBase
    {
        public async Task<HttpResponseMessage> GetMail(long mailId)
        {
            try
            {
                var mail = await DbContext.Mail.FindAsync(mailId);
                return Request.CreateResponse(HttpStatusCode.OK, mail);
            }
            catch (DbException)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        public async Task<HttpResponseMessage> SendMail(long orderId)
        {
            var order = await DbContext.Orders.FindAsync(orderId);

            if (order == null)
            {
                return Request.CreateResponse(HttpStatusCode.MethodNotAllowed, "Order which is attached to mail doesn't exists. Contact with administrator");
            }

            try
            {
                var subject = GetSubjectName(order);
                //W zależności od metody wyślij różne maile.
                switch (order.PaymentMethod)
                {
                    case PaymentMethod.personal:
                        SendMail(order, subject, GetClientBody(order));
                        break;
                    case PaymentMethod.on_delivery:
                        SendMail(order, subject, GetOwnerBody(order), true);
                        SendMail(order, subject, GetClientBody(order));
                        break;
                    case PaymentMethod.online_bank:
                        SendMail(order, subject, GetOwnerBody(order), true);
                        SendMail(order, subject, GetClientBody(order));
                        break;
                }

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception error)
            {
                order.Status = TransactionStatus.Aborted;
                return Request.CreateResponse(HttpStatusCode.InternalServerError, error);
            }

        }

        private string GetOwnerBody(Order order)
        {
            var sb = new StringBuilder();
            var @params = DbContext.Params.ToDictionary(p => p.Code, p => p.Value);

            sb.Append("<p>Magic little shop</p><br/><br/>");

            switch (order.PaymentMethod)
            {
                case PaymentMethod.on_delivery:
                    sb.Append($"<p><strong>Wybrana metoda:</strong> Przy odbiorze</p>");
                    break;
                default:
                    sb.Append($"<p><strong>Wybrana metoda:</strong> Przelew</p>");
                    break;
            }

            sb.Append($"<br/><br/><p>Dane klienta: </p>" +
                      $"<p><strong>Imię</strong>{order.PersonalInformation.Name}</p>" +
                      $"<p><strong>Nazwisko</strong>{order.PersonalInformation.Surname}</p>" +
                      $"<p><strong>Adres mail</strong>{order.PersonalInformation.Mail}</p>" +
                      $"<p><strong>Adres</strong>{order.PersonalInformation.Address}</p>" +
                      $"<p><strong>Adres c.d.</strong>{order.PersonalInformation.AddressTwo}</p>" +
                      $"<p><strong>Telefon</strong>{order.PersonalInformation.Phone}</p>" +
                      $"<p><strong>Kraj</strong>{order.PersonalInformation.Country}</p>" +
                      $"<p><strong>Kod pocztowy</strong>{order.PersonalInformation.PostalCode}</p>" +
                      $"<p><strong>Miasto</strong>{order.PersonalInformation.City}</p><br/><br/>");

            sb.Append("<p>Koszyk klienta</p>");
            //order.Cart.Products.ForEach(p => sb.Append($"<p><strong>{p.Product.Name}</strong> Ilość: {p.Count} Łączna cena: {(p.Count * p.Product.Price):C}</p>"));
            sb.Append($"<br/><p>Łączna cena koszyka: {order.Cart.Products.Sum(p => p.Count * p.Product.Price):C}</p>");

            return sb.ToString();
        }

        private string GetClientBody(Order order)
        {
            var sb = new StringBuilder();
            var @params = DbContext.Params.ToDictionary(p => p.Code, p => p.Value);

            sb.Append("<p>Magic little shop</p><br/><br/>");

            switch (order.PaymentMethod)
            {
                case PaymentMethod.personal:
                    sb.Append($"<p><strong>Wybrana metoda:</strong> Odbiór osobisty</p>" +
                              $"<p>Uprzejmie informujemy, że sklep otrzymał zawiadomienie o podanym zamówieniu " +
                              $"i zarezerwuje przedmioty do 3 dni roboczych od otrzymania zamówienia</p>");
                    break;
                case PaymentMethod.on_delivery:
                    sb.Append($"<p><strong>Wybrana metoda:</strong> Przy odbiorze</p>" +
                              $"<p>Uprzejmie informujemy, że sklep otrzymał zawiadomienie o podanym zamówieniu " +
                              $"i wyśle do Państwa zamówione przedmioty w najbliższym terminie (za pobraniem).</p>" +
                              $"<p>Proszę przygotować kwotę przy kurierze w wysokości {order.Cart.Products.Sum(p => p.Count * p.Product.Price):C}.");
                    break;
                default:
                    sb.Append($"<p><strong>Wybrana metoda:</strong> Przelew</p>" +
                              $"<p>Uprzejmie informujemy, że sklep otrzymał zawiadomienie o podanym zamówieniu " +
                              $"i wyśle do Państwa zamówione przedmioty po dokonaniu wpłaty na konto.</p><br/><br/>" +
                              $"Dane dot. przelewu:<br/>" +
                              $"<p><strong>Nr konta</strong>{@params["OWNER_ACCOUNT_NUMBER"]}</p>" +
                              $"<p><strong>Imię i nazwisko</strong>{@params["OWNER_NAME_AND_SURNAME"]}</p>" +
                              $"<p><strong>Tytułem przelewu</strong>MLS/Zamówienie nr. {order.OrderId}</p>" +
                              $"<p><strong>Kwotą:</strong>{order.Cart.Products.Sum(p => p.Count * p.Product.Price):C}</p>");
                    break;
            }
            return sb.ToString();
        }

        private void SendMail(Order order, string subject, string body, bool sendToOwner = false)
        {
            var @params = DbContext.Params.ToDictionary(p => p.Code, p => p.Value);

            using (var mail = new SmtpClient(@params["SMTP_HOST"], Convert.ToInt32(@params["SMTP_PORT"])))
            {
                var message = new MailMessage()
                {
                    Body = body,
                    IsBodyHtml = true,
                    From = new MailAddress(@params["OWNER_MAIL"]),
                    Subject = subject
                };

                message.To.Add((sendToOwner ? @params["OWNER_MAIL"] : order.PersonalInformation.Mail));

                mail.DeliveryMethod = SmtpDeliveryMethod.Network;
                mail.EnableSsl = true;
                mail.Credentials = new NetworkCredential(@params["SMTP_USERNAME"], @params["SMTP_PASSWORD"]);

                mail.Send(message);
            }

        }

        private string GetSubjectName(Order order)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("[");

            switch (order.PaymentMethod)
            {
                case PaymentMethod.personal:
                    stringBuilder.Append("Odbiór osobisty");
                    break;
                case PaymentMethod.on_delivery:
                    stringBuilder.Append("Przy odbiorze");
                    break;
                default:
                    stringBuilder.Append("Przelew");
                    break;
            }

            stringBuilder.Append($"] Nr zamówienia: {order.OrderId}");

            return stringBuilder.ToString();
        }

        private static class PaymentMethod
        {
            public const string
                on_delivery = "on_delivery",
                personal = "personal",
                online_bank = "online_bank"
                ;
        }

    }
}
