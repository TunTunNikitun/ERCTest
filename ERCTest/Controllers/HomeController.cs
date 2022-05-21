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
        public async Task<IActionResult> AddPersonalAccount(string name, string surname, string? patronymic, string city, string street,
            int building, int? housing, int? flat, double square, int residentsNumber)
        {
            PersonalAccounts PersonalAccount = new PersonalAccounts();
            Person client = new Person
            {
                Name = name,
                Surname = surname,
                Patronymic = patronymic
            };
            Address address = new Address
            {
                Street = street,
                City = city,
                Building = building,
                Housing = housing,
                Flat = flat
            };
            PersonalAccount.Client = client;
            PersonalAccount.Address = address;
            PersonalAccount.Square = square;
            PersonalAccount.ResidentsNumber = residentsNumber;

            string personalAccountJson = Serialization.Getjson<PersonalAccounts>(PersonalAccount);
            HttpContent content = new StringContent(personalAccountJson, Encoding.UTF8, "application/json");


            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(
                    $"https://localhost:7274/PersonalAccounts/CreateAccount?personalAccountJson={personalAccountJson}", content);

                
            }
            return View(PersonalAccount);
        }

        public async Task<IActionResult> PersonalAccountDetails(int Id)
        {

        }
    }
}