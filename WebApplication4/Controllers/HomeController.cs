using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
       public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetSingleData(Input id)
        {
            ViewBag.g = false;
            string baseUrl = "https://petstore.swagger.io/v2/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("store/order/" + id.Pro);
                if (res.IsSuccessStatusCode==true)
                {
                    var emps = res.Content.ReadAsStringAsync().Result;
                    dynamic dObject = JObject.Parse(emps);
                    List<Data> l = new List<Data>
                    {
                        new Data{id = dObject.id, petId=dObject.petId,quantity=dObject.quantity,shipDate=dObject.shipDate,status=dObject.status}
                    };
                    id.Data = l;
                    ViewBag.s = id.Data;
                }
                if(id.Pro>=1)
                {
                    ViewBag.g = true;
                }
                return View();
            }
        }
        public IActionResult Create(Data d)
        {
            ViewBag.p = false;
            if (ModelState.IsValid)
            {
                var httpClient = new HttpClient();
                var json = JsonConvert.SerializeObject(d);
                HttpContent httpContent = new StringContent(json);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpClient.PostAsync("https://petstore.swagger.io/v2/store/order/", httpContent);
                if (d.shipDate != null)
                {
                    ViewBag.p = true;
                    ViewBag.data = "Your data has been Updated";
                }
            }
            return View();
        }
        public IActionResult Delete(Input id)
        {
            ViewBag.f = false;
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response =httpClient.DeleteAsync("https://petstore.swagger.io/v2/store/order/" + id.Pro).Result;
            if(id.Pro>=1)
            {
                ViewBag.f = response.IsSuccessStatusCode;
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
