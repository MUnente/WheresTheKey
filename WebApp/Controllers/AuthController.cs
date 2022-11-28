using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Text.Json;
using System.Text;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin person)
        {
            try
            {
                HttpRequestMessage request = new();
                HttpResponseMessage response = new();
                string json = "";
                UserAccess userAccess = new UserAccess();

                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        request.Method = HttpMethod.Post;
                        request.RequestUri = new Uri($"{Api.URLApi}/Auth/Login");
                        request.Content = new StringContent(JsonSerializer.Serialize(person), Encoding.UTF8, "application/json");

                        response = await client.SendAsync(request);
                        // Console.WriteLine(await response.Content.ReadAsStringAsync());
                        response.EnsureSuccessStatusCode();
                        json = await response.Content.ReadAsStringAsync();
                    }
                }
                else
                {
                    return View("~/Views/Auth/Login.cshtml", person);
                }

                userAccess = JsonSerializer.Deserialize<UserAccess>(json);

                HttpContext.Session.SetString("_Token", userAccess.token);
                HttpContext.Session.SetInt32("_Role", userAccess.role);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ErrorViewModel errorView = new ErrorViewModel { ErrorMessage = ex.Message };
                return View("Error", errorView);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegister person)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage();
                HttpResponseMessage response = new HttpResponseMessage();

                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        request.Method = HttpMethod.Post;
                        request.RequestUri = new Uri($"{Api.URLApi}/Auth/Register");
                        request.Content = new StringContent(JsonSerializer.Serialize(person), Encoding.UTF8, "application/json");

                        response = await client.SendAsync(request);
                        // Console.WriteLine(await response.Content.ReadAsStringAsync());
                        response.EnsureSuccessStatusCode();
                    }
                }
                else
                {
                    return View("~/Views/Auth/Register.cshtml", person);
                }

                return RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
                ErrorViewModel errorView = new ErrorViewModel { ErrorMessage = ex.Message };
                return View("Error", errorView);
            }
        }
    }
}
