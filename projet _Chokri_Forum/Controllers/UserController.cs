using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using projet__Chokri_Forum.Models;

namespace projet__Chokri_Forum.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
          if(  HttpContext.Session.GetString("AutoriseUser") == "true")
            {
                var av = await _context.Users.FindAsync(HttpContext.Session.GetInt32("ID_User"));

                HttpContext.Session.SetString("CheminAvatar", av.cheminavatar);

                var Frms = await _context.Forums.OrderByDescending(f => f.Date).ToListAsync();

                return View(Frms);
            }
            return RedirectToAction("Auth", "Home");
        }

        public async Task<IActionResult> ForumsDétails(int id)
        {
            if (HttpContext.Session.GetString("AutoriseUser") == "true")
            {
                var thms = await _context.Themes.Where(t => t.ForumId == id).ToListAsync();

                return View(thms);
            }
            return RedirectToAction("Auth", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Posts(int id)
        {
            if (HttpContext.Session.GetString("AutoriseUser") == "true")
            {
                HttpContext.Session.SetInt32("ID_Theme", id);
                ViewBag.ID_Theme = HttpContext.Session.GetInt32("ID_Theme");
                ViewBag.ID_User =  HttpContext.Session.GetInt32("ID_User");

                var posts = await _context.Posts
                            .Include(p => p.User)
                            .Where(t => t.ThemeID == id)
                            .OrderByDescending(f => f.datecreationmessage)
                            .ToListAsync();

                return View(posts);
            }
            return RedirectToAction("Auth", "Home");
        }
        [HttpPost]
        public IActionResult Posts(string n1)
        {
            var pst = new Posts
            {
                message = n1,
                datecreationmessage = DateTime.Now,
                ThemeID = (int)HttpContext.Session.GetInt32("ID_Theme"),
                UserID = (int)HttpContext.Session.GetInt32("ID_User"),
            };
            _context.Posts.Add(pst);
            _context.SaveChanges();

            return RedirectToAction("Posts", new { id = HttpContext.Session.GetInt32("ID_Theme") });
        }
    
        [HttpGet]
        public async Task<IActionResult> PostsEdit(int id)
        {
            if (HttpContext.Session.GetString("AutoriseUser") == "true")
            {
             
                var frm = await _context.Posts.FindAsync(id);
                return View(frm);
            }
            return RedirectToAction("Auth", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> PostsEdit(Posts pst)
        {
            if (HttpContext.Session.GetString("AutoriseUser") == "true")
            {

                _context.Posts.Update(pst);
                await _context.SaveChangesAsync();
                return View(pst);
            }
            return RedirectToAction("Auth", "Home");
        }
        public async Task<IActionResult> PostsDelete(int id)
        {
            if (HttpContext.Session.GetString("AutoriseUser") == "true")
            {
                var pst = await _context.Posts.FindAsync(id);
                _context.Posts.Remove(pst);
                await _context.SaveChangesAsync();
                return RedirectToAction("Posts", new { id = HttpContext.Session.GetInt32("ID_Theme") });
            }
            return RedirectToAction("Auth", "Home");
        }




        [HttpGet]
        public async Task<IActionResult> PostsDétails(int id, int UserID)
        {
            if (HttpContext.Session.GetString("AutoriseUser") == "true")
            {
                HttpContext.Session.SetInt32("ID_Post", id);
                ViewBag.ID_Post = HttpContext.Session.GetInt32("ID_Post");
                ViewBag.ID_User = HttpContext.Session.GetInt32("ID_User");

                var msg = await _context.Messages
                        .Include(p => p.Post)
                        .Include(p => p.User)
                        .Where(t => t.PostID == id)
                        .ToListAsync();
               
                var own = await _context.Users.Where(f => f.id == UserID).ToListAsync();
                foreach (var user in own)
                {
                    ViewBag.v1 = user.cheminavatar;
                    ViewBag.v2 = user.pseudonyme;
                  
                }

                return View(msg);
            }
            return RedirectToAction("Auth", "Home");
        }
        [HttpPost]
        public IActionResult PostsDétails(string n1)
        {
            var msg = new Messages
            {
                contenu = n1,
                dateMsg = DateTime.Now,
                PostID = (int)HttpContext.Session.GetInt32("ID_Post"),
                UserID = (int)HttpContext.Session.GetInt32("ID_User"),
            };
            _context.Messages.Add(msg);
            _context.SaveChanges();

            return RedirectToAction("PostsDétails", new { id = HttpContext.Session.GetInt32("ID_Post") });
        }
        public async Task<IActionResult> DeleteMSG(int id)
        {
            if (HttpContext.Session.GetString("AutoriseUser") == "true")
            {
                var msg = await _context.Messages.FindAsync(id);
                _context.Messages.Remove(msg);
                await _context.SaveChangesAsync();
                return RedirectToAction("PostsDétails", new { id = HttpContext.Session.GetInt32("ID_Post") });
            }
            return RedirectToAction("Auth", "Home");
        }
        public async Task<IActionResult> EditMSG(int id)
        {
            if (HttpContext.Session.GetString("AutoriseUser") == "true")
            {
                ViewBag.ID_Post = (int)HttpContext.Session.GetInt32("ID_Post");

                var msg = await _context.Messages.FindAsync(id);
                return View(msg);
            }
            return RedirectToAction("Auth", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> EditMSG(Messages msg)
        {
            if (HttpContext.Session.GetString("AutoriseUser") == "true")
            {
                ViewBag.ID_Post = (int)HttpContext.Session.GetInt32("ID_Post");

                _context.Messages.Update(msg);
                await _context.SaveChangesAsync();
                return View(msg);
            }
            return RedirectToAction("Auth", "Home");
        }




        

        [HttpGet]
        public async Task<IActionResult> Message()
        {
            if (HttpContext.Session.GetString("AutoriseUser") == "true")
            {
               
              
                ViewBag.ID_User = HttpContext.Session.GetInt32("ID_User");

                var id = HttpContext.Session.GetInt32("ID_User");

                var posts = await _context.Posts
                            .Include(p => p.User)
                            .Where(t => t.UserID == id)
                            .OrderByDescending(f => f.datecreationmessage)
                            .ToListAsync();

                return View(posts);
            }
            return RedirectToAction("Auth", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Profil()
        {
           

            if (HttpContext.Session.GetString("AutoriseUser") == "true")
            {
               var ID = HttpContext.Session.GetString("ID");

               
               var user = await _context.Users.FindAsync(int.Parse(ID));

               

                return View(user);
            }
            return RedirectToAction("Auth", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Profil(string n1, string n2, string n3, IFormFile file)
        {
            if (HttpContext.Session.GetString("AutoriseUser") == "true")
            {
 
                var ID = HttpContext.Session.GetString("ID");
                var user = await _context.Users.FindAsync(int.Parse(ID));

                using (var fileStream = new FileStream($"wwwroot/images/{ID}_{file.FileName}", FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                user.pseudonyme = n1;
                user.motdepasse = n2;
                user.email = n3;
                user.cheminavatar = Url.Content($"~/images/{ID}_{file.FileName}");
                HttpContext.Session.SetString("CheminAvatar", user.cheminavatar);

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return View(user);
            }

            return RedirectToAction("Auth", "Home");
        }




        public IActionResult Logout()
        {
            HttpContext.Session.SetString("AutoriseUser", "False");
            return RedirectToAction("Auth", "Home");
        }

        public IActionResult About()
        {
            return View();
        }

    }
}
