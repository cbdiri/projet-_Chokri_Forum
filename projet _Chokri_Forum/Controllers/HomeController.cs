using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projet__Chokri_Forum.Models;
using System;
using System.Diagnostics;

namespace projet__Chokri_Forum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Auth()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Auth(string n1, string n2)
        {
            bool vn1=false;
            bool vn2 = false;
            bool vadmin = false;
            bool vvalide = false;

            var users = await _context.Users.ToListAsync();
            foreach (var user in users) 
            { 
                
             if(n1==user.pseudonyme && n2==user.motdepasse)
                {
                    vn1=true;
                    vn2=true;
                    vadmin = user.admin;
                    vvalide = user.valide;
                    HttpContext.Session.SetString("ID", user.id.ToString());

                    HttpContext.Session.SetInt32("ID_User", user.id);
                    break;
                }
                
            }
            Console.WriteLine($"{vn1}, {vn2}, {vadmin}");

            if (vn1==true && vn2==true &&  vvalide == true && vadmin == true)
            {
                HttpContext.Session.SetString("AutoriseAdmin", "true");
                return Redirect("Admin/Index");
            }
            else if(vn1 == true && vn2 == true && vvalide == true && vadmin == false)
            {
                HttpContext.Session.SetString("AutoriseUser", "true");
                return Redirect("User/Index");
            }
            else
            {
                ViewBag.Message = "pseudonyme ou mot de passe incorrect";
                return View("Auth");
            }
        }

       

        [HttpGet]
        public IActionResult Insc()
        {
            return View(); 
        }
        [HttpPost]
        public IActionResult Insc(string n1,string n2,string n3)
        {
            var newUser = new Users
            {
                pseudonyme = n1,
                motdepasse = n2,
                email = n3,
                inscrit = true,
                valide = false,
                cheminavatar = "/images/av.png",
                signature = "signature",
                actif = true,
                admin = false,

            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
            ViewBag.message = "demande d'inscription envoyé avec succès";
            return View();
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
    }
}

