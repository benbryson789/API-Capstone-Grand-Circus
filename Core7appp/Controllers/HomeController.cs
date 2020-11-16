using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Core7appp.Models;
using RestSharp;
using Newtonsoft.Json;

namespace Core7appp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private RestClient client;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            client = new RestClient("https://localhost:44300/api/");
        }

        public IActionResult Index()
        {
            // create api request sent using RestRequest from RestClient Nuget package
            var request = new RestRequest("product/searchByUnitPrice?maxPrice=20");

            // send api reqiest using Restclient
            var response = client.Get(request);

            // deserialize json content int C# object model using NewtonSoft JsonConvert method
            var productList = JsonConvert.DeserializeObject<IEnumerable<MaxPriceProductModel>>(response.Content);

            // send result (list of product) to the view.
            return View(productList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
