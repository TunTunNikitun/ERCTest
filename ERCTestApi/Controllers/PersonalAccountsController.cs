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
        [HttpPost]
        [Route("CreateAccount")]
        public async void CreatePersonalAccount(string personalAccountJson)
        {
            PersonalAccounts PersonalAccount = Serialization.JsonDeserializing<PersonalAccounts>(personalAccountJson);
            PersonalAccount.OpenDate = DateTime.Now;
            using (ApplicationContext db = new ApplicationContext())
            {
                db.PersonalAccounts.Add(PersonalAccount);
                db.SaveChanges();
            }
        }
        
        [HttpGet]
        [Route("GetAccount")]
        public string GetPersonalAccount(int? id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (id != null)
                {
                    if (id > 0)
                    {
                        var account = (db.PersonalAccounts.Include(a => a.Client).Include(a => a.Address).ToList()).Where(a => a.Id == id).FirstOrDefault();
                        var accountJson = Serialization.Getjson<PersonalAccounts>(account);
                        return accountJson;
                    }
                    else
                        return "Wrong id";
                }
                else
                {
                    var accounts = db.PersonalAccounts.Include(a => a.Address).Include(a => a.Client).ToList();
                    var result = Serialization.Getjson<List<PersonalAccounts>>(accounts);
                    return result;
                }
            }
        }



        //[HttpDelete("{id:int}")]
        [HttpDelete]
        [Route("CloseAccount")]
        public void ClosePersonalAccount(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                //var account = (db.PersonalAccounts.ToList()).Where(a => a.Id == id).FirstOrDefault();
                var account = db.PersonalAccounts.Where(a => a.Id == id).FirstOrDefault();
                if (account.CloseDate == null)
                {
                    account.CloseDate = DateTime.Now;
                    db.SaveChanges();
                }

            }
        }

        [HttpPut]
        [Route("ChangeAccount")]
        public void ChangeAccount(string newAccountJson)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                var newAccount = Serialization.JsonDeserializing<PersonalAccounts>(newAccountJson);
                var account = db.PersonalAccounts.Where(a => a.Id == newAccount.Id).Include(a=>a.Client).Include(a=>a.Address).FirstOrDefault();
                //var newAccount = new PersonalAccounts(name, surname, patronymic, city, street, building, housing, flat, square, residentsNumber);
               
                account.Client.Name = newAccount.Client.Name;
                account.Client.Surname = newAccount.Client.Surname;
                account.Client.Patronymic = newAccount.Client.Patronymic;
                account.Address.City = newAccount.Address.City;
                account.Address.Street = newAccount.Address.Street;
                account.Address.Building = newAccount.Address.Building;
                account.Address.Housing = newAccount.Address.Housing;
                account.Address.Flat = newAccount.Address.Flat;
                account.Square = newAccount.Square;
                account.ResidentsNumber = newAccount.ResidentsNumber;
                db.PersonalAccounts.Update(account);
                db.SaveChanges();
            }
        } 
    }
}
