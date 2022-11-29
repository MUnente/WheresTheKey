using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;
using System.Text.Json;
using System.Text;

namespace WebApp.Controllers
{
    [Authentication]
    public class ReservationController : Controller
    {
        public ActionResult CreateReservation()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateReservation(Reservation reservationForm)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage();
                HttpResponseMessage response = new HttpResponseMessage();
                string token = HttpContext.Session.GetString("_Token");

                if (token == null)
                    return RedirectToAction("Index", "Home");

                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        request.Method = HttpMethod.Post;
                        request.RequestUri = new Uri($"{Api.URLApi}/Reservation/CreateNewReservation");
                        request.Content = new StringContent(JsonSerializer.Serialize(reservationForm), Encoding.UTF8, "application/json");
                        request.Headers.Add("Authorization", $"Bearer {token}");

                        response = await client.SendAsync(request);
                        response.EnsureSuccessStatusCode();
                    }

                    return RedirectToAction("ListMyReservations", "Reservation");
                }
                else
                {
                    return View("~/Views/Reservation/CreateReservation.cshtml", reservationForm);
                }
            }
            catch (HttpRequestException ex)
            {
                ErrorViewModel errorView = new ErrorViewModel { ErrorMessage = ex.Message };
                return View("Error", errorView);
            }
        }

        public async Task<ActionResult> ListMyReservations()
        {
            try
            {
                IEnumerable<Reservation>? myReservations = new List<Reservation>();
                HttpRequestMessage request = new HttpRequestMessage();
                HttpResponseMessage response = new HttpResponseMessage();
                string reservationsJson;
                UriBuilder requestUriBuilder = new($"{Api.URLApi}/Reservation/ListMyReservations");
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
                    reservationsJson = await response.Content.ReadAsStringAsync();
                }

                myReservations = JsonSerializer.Deserialize<List<Reservation>>(reservationsJson);

                return View(myReservations);
            }
            catch (HttpRequestException ex)
            {
                ErrorViewModel errorView = new ErrorViewModel { ErrorMessage = ex.Message };
                return View("Error", errorView);
            }
        }
    }
}