using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace WebApiTest4.Services.Impls
{
    public class EmailServiceImpl : IIdentityMessageService
    {
        //public Task SendAsync(IdentityMessage message)
        //{
        //    const string apiKey = "key-f98167465467afde3fa2fb6772c0e98d";
        //    const string sandBox = "sandboxb3749802b03b419790ab67f1a82156c7.mailgun.org";
        //    byte[] apiKeyAuth = Encoding.ASCII.GetBytes($"api:{apiKey}");
        //    var httpClient = new HttpClient { BaseAddress = new Uri("https://api.mailgun.net/v3/") };
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
        //        Convert.ToBase64String(apiKeyAuth));

        //    var form = new Dictionary<string, string>
        //    {
        //        ["from"] = "postmaster@ege-app.com",
        //        ["to"] = message.Destination,
        //        ["subject"] = message.Subject,
        //        ["text"] = message.Body
        //    };

        //    HttpResponseMessage response =
        //        httpClient.PostAsync(sandBox + "/messages", new FormUrlEncodedContent(form)).Result;
        //    return Task.FromResult((int)response.StatusCode);
        //}
        public Task SendAsync(IdentityMessage message)
        {
            // настройка логина, пароля отправителя
            var rm = new ResourceManager("WebApiTest4.Services.Impls.EmailAccountSettings", Assembly.GetExecutingAssembly());
            var from = rm.GetString("address");
            var pass = rm.GetString("password");

            // адрес и порт smtp-сервера, с которого мы и будем отправлять письмо
            SmtpClient client = new SmtpClient("smtp.yandex.ru", 25);

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(from, pass);
            client.EnableSsl = true;

            // создаем письмо: message.Destination - адрес получателя
            var mail = new MailMessage(from, message.Destination);
            mail.Subject = message.Subject;
            mail.Body = message.Body;
            mail.IsBodyHtml = true;

            return client.SendMailAsync(mail);
        }
    }
}