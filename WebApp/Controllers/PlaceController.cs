using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;
using System.Text.Json;
using System.Text;

namespace WebApp.Controllers
{
    [Authentication]
    public class PlaceController : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Place>? places = new List<Place>();
                HttpRequestMessage request = new HttpRequestMessage();
                HttpResponseMessage response = new HttpResponseMessage();
                string placeJson;
                UriBuilder requestUriBuilder = new($"{Api.URLApi}/Place/GetPlaces");
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
                    placeJson = await response.Content.ReadAsStringAsync();
                }

                places = JsonSerializer.Deserialize<List<Place>>(placeJson);

                return View(places);
            }
            catch (HttpRequestException ex)
            {
                ErrorViewModel errorView = new ErrorViewModel { ErrorMessage = ex.Message };
                return View("Error", errorView);
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertPlace(Place place)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage();
                HttpResponseMessage response = new HttpResponseMessage();
                string token = HttpContext.Session.GetString("_Token");

                if (token == null)
                    return RedirectToAction("Index", "Home");

                using (HttpClient client = new HttpClient())
                {
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri($"{Api.URLApi}/Place/PostPlace");
                    request.Content = new StringContent(JsonSerializer.Serialize(place), Encoding.UTF8, "application/json");
                    request.Headers.Add("Authorization", $"Bearer {token}");

                    response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                }

                return RedirectToAction("Index", "Place");
            }
            catch (Exception ex)
            {
                ErrorViewModel errorView = new ErrorViewModel { ErrorMessage = ex.Message };
                return View("Error", errorView);
            }
        }

        public async Task<IActionResult> UpdatePlace(Place place)
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
                    request.RequestUri = new Uri($"{Api.URLApi}/Place/UpdatePlace");
                    request.Content = new StringContent(JsonSerializer.Serialize(place), Encoding.UTF8, "application/json");
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

        public async Task<IActionResult> DeletePlace([FromRoute] int? id = null)
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
                    request.Method = HttpMethod.Delete;
                    request.RequestUri = new Uri($"{Api.URLApi}/Place/DeletePlace/{id}");
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