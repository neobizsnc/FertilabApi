using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FertilabApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FertilabApi.Controllers
{
    public class LoginController : Controller
    {

        private readonly FertilabContext _context;

        public LoginController(FertilabContext context)
        {
            _context = context;
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Login user)
        {
            try
            {
                var userRes = _context.User.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault();
                if (userRes != null)
                {
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddMinutes(3600);
                    Response.Cookies.Append("lgg", "true", option);
                }
                return RedirectToAction("Index", "Centers");
            }
            catch
            {
                return View();
            }
        }

        
    }
}