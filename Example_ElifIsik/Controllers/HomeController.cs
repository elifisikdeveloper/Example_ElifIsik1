using Example_ElifIsik.Dtos;
using Example_ElifIsik.Models;
using Example_ElifIsik.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Example_ElifIsik.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController( IHttpClientFactory httpClientFactory)
        {
  
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(MaterialModel model)
        {
            var token = "axJgnrHEziivwAWfEFGqTcbKFhTaM";

            using (var client = _httpClientFactory.CreateClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var jsonData = JsonConvert.SerializeObject(model);

                using (var content = new StringContent(jsonData, Encoding.UTF8, "application/json"))
                {
                    var responseMessage = await client.PostAsync("http://testapi.dogruer.com/api/MaterialMaster/SaveMaterialMaster", content);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            ModelState.AddModelError("", "İşlem gerçekleştirilemedi. Lütfen daha sonra tekrar deneyin.");
            return View(model);
        }





    }
}
