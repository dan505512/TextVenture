using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TextVenture.Admin.WebView.Models;
using TextVenture.DAL;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using TextVenture.Admin.WebView.SessionManagement;

namespace TextVenture.Admin.WebView.Controllers
{
    /// <summary>
    /// An admin panel controller to log in
    /// </summary>
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly ITextVentureDB _db;
        private readonly SessionManager _sessionManager;
        public LoginController()
        {
            _db = DbFactory.GetTextVentureDb("adventures");
            _sessionManager = SessionManagerFactory.GetSessionManager();
        }

        /// <summary>
        /// Validated the log in credentials. Returns a session cookie.
        /// </summary>
        /// <param name="login">The login username and password</param>
        /// <returns>Unauthorized if not found or password isn't correct. Ok and a cookie if everything is right.</returns>
        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Post([FromBody]LoginRequest login)
        {
            var dbSha = _db.GetPasswordHashForUser(login.Username);
            var requestSha = GetShaOfString(login.Password);


            if (dbSha == null || !dbSha.Equals(requestSha))
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            var sessionId = _sessionManager.GetNewSessionsId();

            var cookie = new CookieHeaderValue("session", sessionId);

            var response = new HttpResponseMessage(HttpStatusCode.OK);

            response.Headers.AddCookies(new[] { cookie });

            return response;
        }

        /// <summary>
        /// Validates the session cookie
        /// </summary>
        /// <returns>Ok if the cookie is valid. Unauthorized if not found or not valid</returns>
        [HttpGet]
        public IActionResult ValidateLogin()
        {
            var sessionCookieFound = Request.Cookies.TryGetValue("session", out var sessionId);

            var isSessionValid = sessionCookieFound && _sessionManager.ValidateSession(sessionId);

            return isSessionValid ? (IActionResult) Ok() : Redirect("/login");
        }

        private string GetShaOfString(string value)
        {
            var Sb = new StringBuilder();

            var enc = Encoding.UTF8;
            using (var hash = SHA256.Create())
            {
                var result = hash.ComputeHash(enc.GetBytes(value));

                foreach (var b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

    }
}
