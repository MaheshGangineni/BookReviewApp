using BookReview.API.Entities;
using BookReview.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.UI.Controllers
{
    public class BooksController : Controller
    {
        private readonly ILogger<BooksController> _logger;

        public BooksController(ILogger<BooksController> logger)
        {
            _logger = logger;
        }

        public async Task<ActionResult> BookReviews()
        {
                List < BookReviewModel> books = new List<BookReviewModel>();
                using (var client = CreateApiClient.Client())
                {
                    HttpResponseMessage Res = await client.GetAsync("/api/v1/Book");
                    if (Res.IsSuccessStatusCode)
                    {
                        var bookResponse = Res.Content.ReadAsStringAsync().Result;
                        books = JsonConvert.DeserializeObject<List<BookReviewModel>>(bookResponse);
                    }

                }
                    return View(books);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult CreateReview()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateReview(CreateReviewModel createReviewModel)
        {
            if (ModelState.IsValid)
            {
                using (var client = CreateApiClient.Client())
                {
                    HttpResponseMessage Res = await client.PostAsync("/api/v1/Book", new StringContent(JsonConvert.SerializeObject(createReviewModel), Encoding.UTF8, "application/json"));
                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(BookReviews));
                    }
                }
            }
            return RedirectToAction(nameof(BookReviews));
        }

        public async Task<ActionResult> EditReview(string id)
        {

            try
            {
                BookReviewModel book = new BookReviewModel();
                if (id != null)
                {
                    using (var client = CreateApiClient.Client())
                    {
                        HttpResponseMessage Res = await client.GetAsync("/api/v1/Book/" + id);
                        if (Res.IsSuccessStatusCode)
                        {
                            var bookResponse = Res.Content.ReadAsStringAsync().Result;
                            book = JsonConvert.DeserializeObject<BookReviewModel>(bookResponse);
                        }

                    }
                }

                if (book != null)
                {
                    return View(book);
                }
                else
                    TempData["error"] = "error while editing review";
                return RedirectToAction("Home", "Error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EditReview(BookReviewModel bookReviewModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    using (var client = CreateApiClient.Client())
                    {
                        HttpResponseMessage Res = await client.PutAsync("/api/v1/Book", new StringContent(JsonConvert.SerializeObject(bookReviewModel), Encoding.UTF8, "application/json"));
                        if (Res.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(BookReviews));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }
            return RedirectToAction(nameof(BookReviews));
        }

        public async Task<ActionResult> DeleteReview(string id)
        {
            BookReviewModel book = new BookReviewModel();
            if (id != null)
            {
                using (var client = CreateApiClient.Client())
                {
                    HttpResponseMessage Res = await client.GetAsync("/api/v1/Book/" + id);
                    if (Res.IsSuccessStatusCode)
                    {
                        var bookResponse = Res.Content.ReadAsStringAsync().Result;
                        book = JsonConvert.DeserializeObject<BookReviewModel>(bookResponse);
                    }

                    return View(book);
                }

            }
            return View(book);
        }

        [HttpPost,ActionName("DeleteReview")]
        public async Task<ActionResult> DeleteReviewConfirmed(string id)
        {

            try
            {
                using (var client = CreateApiClient.Client())
                {
                    HttpResponseMessage Res = await client.DeleteAsync("/api/v1/Book/"+id);
                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(BookReviews));
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
            }

            return RedirectToAction(nameof(BookReviews));
        }
    }
}
