using ERCTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ERCTest.Controllers
{
    public class HomeController : Controller
    {
        
        public async Task<IActionResult> Index()
        {
            List<PersonalAccounts> accounts = new List<PersonalAccounts>();
            using(var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7274/PersonalAccounts/GetAllAccounts"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    accounts= JsonConvert.DeserializeObject<List<PersonalAccounts>>(apiResponse);
                }
            }
            return View(accounts);
        }

       
    }
}