using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;
using System.Text.Json;
using System.Text;

namespace WebApp.Controllers
{
    [Authentication]
    public class AccountRequestsController : Controller
    {
        public async Task<ActionResult> Index()
        {
            try
            {
                IEnumerable<UserChangeStatus>? users = new List<UserChangeStatus>();
                HttpRequestMessage request = new HttpRequestMessage();
                HttpResponseMessage response = new HttpResponseMessage();
                string usersJson;
                UriBuilder requestUriBuilder = new($"{Api.URLApi}/AccountRequest/ListUsers");
                string token = HttpContext.Session.GetString("_Token");

                if (token == null)
                    return RedirectToAction("Index", "Home");

                using (HttpClient client = new())
                {
                    request.Method = HttpMethod.Get;
                    request.RequestUri = requestUriBuilder.Uri;
                    request.Headers.Add("Authorization", $"Bearer {token}");

                    response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    usersJson = await response.Content.ReadAsStringAsync();
                }

                users = JsonSerializer.Deserialize<List<UserChangeStatus>>(usersJson);

                return View(users);
            }
            catch (HttpRequestException ex)
            {
                ErrorViewModel errorView = new ErrorViewModel { ErrorMessage = ex.Message };
                return View("Error", errorView);
            }
        }

        public async Task<ActionResult> UpdateStatus(UserChangeStatus user)
        {
            try
            {
                HttpRequestMessage request = new();
                HttpResponseMessage response = new();
                string token = HttpContext.Session.GetString("_Token");

                if (token == null)
                    return RedirectToAction("Index", "Home");

                using (HttpClient client = new())
                {
                    request.Method = HttpMethod.Put;
                    request.RequestUri = new Uri($"{Api.URLApi}/AccountRequest/UpdateUserStatus");
                    request.Content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
                    request.Headers.Add("Authorization", $"Bearer {token}");

                    response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                }

                return RedirectToAction("Index", "Place");
            }
            catch (HttpRequestException ex)
            {
                ErrorViewModel errorView = new ErrorViewModel { ErrorMessage = ex.Message };
                return View("Error", errorView);
            }
        }
    }
}