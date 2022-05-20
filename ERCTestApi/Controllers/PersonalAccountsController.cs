using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ERCTestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ERCTestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonalAccountsController : ControllerBase
    {
        //// GET: LsController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: LsController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: LsController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: LsController/Create
        [HttpPost]
        [Route("CreateAccount")]
        public void CreatePersonalAccount(string name, string surname, string? patronymic, string city, string street, 
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
            PersonalAccount.OpenDate = DateTime.Now;

            using(ApplicationContext db= new ApplicationContext())
            {
                db.PersonalAccounts.Add(PersonalAccount);
                db.SaveChanges();
            }
        }
        [HttpGet]
        [Route("GetAccount")]
        public string GetPersonalAccount(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var account = (db.PersonalAccounts.Include(a => a.Client).Include(a => a.Address).ToList()).Where(a => a.Id == id).FirstOrDefault();
                var accountJson = Serialization.Getjson<PersonalAccounts>(account);
                return accountJson;
            }
        }



        [HttpPost]
        [Route("CloseAccount")]
        public  void ClosePersonalAccount(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var account = (db.PersonalAccounts.ToList()).Where(a => a.Id == id).FirstOrDefault();
                account.CloseDate = DateTime.Now;
                db.SaveChanges();
            }
        }

        [HttpGet]
        [Route("GetAllAccounts")]
        public string GetAllAccounts()
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                var accounts= db.PersonalAccounts.Include(a=>a.Address).Include(a=>a.Client).ToList();
                var result = Serialization.Getjson<List<PersonalAccounts>>(accounts);
                return result;
            }
        }

        [HttpPost]
        [Route("Change Account")]
        public void ChangeAccount()
        {

        }

        //// GET: LsController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: LsController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: LsController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: LsController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
