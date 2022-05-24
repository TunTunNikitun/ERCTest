using ERCTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ERCTest.Controllers
{
    public class HomeController : Controller
    {
        
        public async Task<IActionResult> Index()
        {
            List<PersonalAccounts> accounts = new List<PersonalAccounts>();
            using(var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7274/PersonalAccounts/GetAccount"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    accounts= JsonConvert.DeserializeObject<List<PersonalAccounts>>(apiResponse);
                }
            }
            return View(accounts);
        }

        public ViewResult AddPersonalAccount() => View();

        [HttpPost]
        //public async Task<IActionResult> AddPersonalAccount(string name, string surname, string? patronymic, string city, string street,
        //    int building, int? housing, int? flat, double square, int residentsNumber)
        public async Task<IActionResult> AddPersonalAccount(PersonalAccounts PersonalAccount)
        {
            string personalAccountJson = Serialization.Getjson<PersonalAccounts>(PersonalAccount);
            HttpContent content = new StringContent(personalAccountJson, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(
                    $"https://localhost:7274/PersonalAccounts/CreateAccount?personalAccountJson={personalAccountJson}", content);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> PersonalAccountDetails(int Id )
        {
            PersonalAccounts account = new PersonalAccounts();
            using(var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7274/PersonalAccounts/GetAccount?id={Id}"))
                {
                    string apiResponse= await response.Content.ReadAsStringAsync();
                    account= JsonConvert.DeserializeObject<PersonalAccounts>(apiResponse);
                }
            }
            return View(account);
        }

        public async Task<IActionResult> ClosePersonalAccount(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"https://localhost:7274/PersonalAccounts/CloseAccount?id={id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("PersonalAccountDetails", new { Id = id });
        }

        private async Task<IActionResult> GetAccountDataToChange(int id)
        {
            PersonalAccounts account = new PersonalAccounts();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7274/PersonalAccounts/GetAccount?id={id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    account = JsonConvert.DeserializeObject<PersonalAccounts>(apiResponse);
                }
            }
            return View(account);
        }
        public async Task<IActionResult> AccountChanging(int id)
        {
            PersonalAccounts account = new PersonalAccounts();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7274/PersonalAccounts/GetAccount?id={id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    account = JsonConvert.DeserializeObject<PersonalAccounts>(apiResponse);
                }
            }
            ViewBag.Data = account;
            return View();
        }
        public async Task<IActionResult> SaveAccountChanging(PersonalAccounts account)
        {
            var accountJson = Serialization.Getjson(account);
            using (var httpClient = new HttpClient())
            {
                HttpContent content = new StringContent(accountJson, Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync($"https://localhost:7274/PersonalAccounts/ChangeAccount?newAccountJson={accountJson}", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("PersonalAccountDetails", new { Id = account.Id });
        }
    }
}