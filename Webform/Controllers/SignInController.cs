using Microsoft.AspNetCore.Mvc;
using Webform.Data;
using System.Linq;
using Webform.Models;
using Microsoft.AspNetCore.Http;


namespace Webform.Controllers
{
    public class SignInController : Controller
    {
        private readonly WebformContext _context;

        public SignInController(WebformContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            Console.WriteLine("hello");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {


                var f_password = password;
                var data = _context.SalesAgent.Where(s => s.Email.Equals(email) && s.Password.Equals(password)).ToList();
                if (data.Count() > 0)
                {


                    //add session
                    /* Session["Name"] = data.FirstOrDefault().Name;*/
                    HttpContext.Session.SetString("SalesAgentName", data.FirstOrDefault().Name);
                    HttpContext.Session.SetString("SalesAgentEmail", data.FirstOrDefault().Email);
                    HttpContext.Session.SetInt32("SalesAgentID", data.FirstOrDefault().ID);
                    Console.WriteLine(data.FirstOrDefault().Name + data.FirstOrDefault().Email + data.FirstOrDefault().ID);
                    /*ViewData["SalesAgentID"] = data.FirstOrDefault().ID.ToString();*/
                    /*HttpContext.Current.Session["FullName"] = data.FirstOrDefault().Name;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().ID;*/
                    return RedirectToAction("Index","Products");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }


   
   
}
