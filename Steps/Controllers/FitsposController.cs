using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Steps.Data;
using Steps.Models;

namespace Steps.Controllers
{
    public class FitsposController : Controller
    {
        private readonly StepsContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public FitsposController(StepsContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        [HttpGet("/fitspo/summary")]
        public async Task<IActionResult> Articles()
        {
            return View(await _context.Steps_Fitspos.ToListAsync());
        }

        public IActionResult Logout()
        {
            Response.Cookies.Append("User", "loggedOut", new CookieOptions() { Expires = DateTime.Now.AddDays(-1), IsEssential = true });
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("/fitspo/single")]
        public async Task<IActionResult> Single(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitspo = await _context.Steps_Fitspos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fitspo == null)
            {
                return NotFound();
            }

            return View(fitspo);


        }

        public async Task<IActionResult> Index(string id)
        {
            if(HttpContext.Request.Cookies["User"] == null)
            {
                if(id != null)
                {
                    Response.Cookies.Append("User", id, new CookieOptions() { Expires = DateTime.Now.AddHours(5), IsEssential = true });
                    return View(await _context.Steps_Fitspos.ToListAsync());
                } else
                {
                    return NotFound();
                }
                
            } else
            {
                var user = HttpContext.Request.Cookies["User"];

                if (user != null)
                {
                    return View(await _context.Steps_Fitspos.ToListAsync());
                }
            }

            
            return NotFound();
        }

        public static string GetImageFromByteArray(byte[] img)
        {
            string imgBase64Data = Convert.ToBase64String(img);
            string imgDataURL = string.Format("data:image/png;base64,{0}", imgBase64Data);
            return imgDataURL;
        }

        // GET: Fitspos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var user = HttpContext.Request.Cookies["User"];
            if (id == null)
            {
                return NotFound();
            }

            var fitspo = await _context.Steps_Fitspos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fitspo == null)
            {
                return NotFound();
            }

            if (user != null)
            {
                return View(fitspo);
            }
            return NotFound();

            
        }

        // GET: Fitspos/Create
        public async Task<IActionResult> Create()
        {
            var user = HttpContext.Request.Cookies["User"];
            if (user != null)
            {
                return View();
            }
            return NotFound();
        }

        // POST: Fitspos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateOfPost,Title,Blurb,Post,Image,FormFile")] Fitspo fitspo, IFormFile FormFile)
        {
            if(ModelState.IsValid)
            {
                byte[] image;
                using (var memoryStream = new MemoryStream())
                {
                    await FormFile.CopyToAsync(memoryStream);
                    if (memoryStream.Length > 2097152)
                    {
                        ModelState.AddModelError("FormFile", "The file is too large.");
                    }
                    else
                    {
                        image = memoryStream.ToArray();

                        Fitspo fitspoNew = new Fitspo
                        {
                            Id = fitspo.Id,
                            DateOfPost = fitspo.DateOfPost,
                            Title = fitspo.Title,
                            Blurb = fitspo.Blurb,
                            Post = fitspo.Post,
                            Image = image,
                            FormFile = FormFile
                        };

                        _context.Add(fitspoNew);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));

                    }
                    return View(fitspo);
                }


            }

            return View(fitspo);




        }

        // GET: Fitspos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = HttpContext.Request.Cookies["User"];
            
            if (id == null)
            {
                return NotFound();
            }

            var fitspo = await _context.Steps_Fitspos.FindAsync(id);
            
            if (fitspo == null)
            {
                return NotFound();
            }
            if (user != null)
            {
                if (fitspo.Image != null)
                {
                    var base64Image = Convert.ToBase64String(fitspo.Image);
                    TempData["Image"] = base64Image;
                }
                return View(fitspo);
            }
            return NotFound();
        }

        // POST: Fitspos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateOfPost,Title,Blurb,Post,Image,FormFile")] Fitspo fitspo, IFormFile FormFile, string param)
        {
            if (id != fitspo.Id)
            {
                return NotFound();
            }

            byte[] image;
            
            if (ModelState.IsValid)
            {
                try
                {
                    
                    using (var memoryStream = new MemoryStream())
                    {
                        await FormFile.CopyToAsync(memoryStream);
                        if (memoryStream.Length > 2097152)
                        {
                            ModelState.AddModelError("FormFile", "The file is too large.");
                        }
                        else
                        {
                            image = memoryStream.ToArray();

                            Fitspo fitspoNew = new Fitspo
                            {
                                Id = fitspo.Id,
                                DateOfPost = fitspo.DateOfPost,
                                Title = fitspo.Title,
                                Blurb = fitspo.Blurb,
                                Post = fitspo.Post,
                                Image = image,
                                FormFile = FormFile
                            };

                            _context.Update(fitspoNew);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FitspoExists(fitspo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            } else if(fitspo.DateOfPost != null && fitspo.Title != null && fitspo.Blurb != null && fitspo.Post != null)
            {
                    string imageBase64 = TempData["Image"].ToString();
                    byte[] imageByteArr = Convert.FromBase64String(imageBase64);


                Fitspo fitspoNew = new Fitspo
                    {
                        Id = fitspo.Id,
                        DateOfPost = fitspo.DateOfPost,
                        Title = fitspo.Title,
                        Blurb = fitspo.Blurb,
                        Post = fitspo.Post,
                        Image = imageByteArr,
                        FormFile = FormFile
                    };
                    _context.Update(fitspoNew);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
            }
            return View(fitspo);
        }

        // GET: Fitspos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var user = HttpContext.Request.Cookies["User"];

            if (id == null)
            {
                return NotFound();
            }

            var fitspo = await _context.Steps_Fitspos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fitspo == null)
            {
                return NotFound();
            }

            if (user != null)
            {
                return View(fitspo);
            }
            return NotFound();
        }

        // POST: Fitspos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fitspo = await _context.Steps_Fitspos.FindAsync(id);
            _context.Steps_Fitspos.Remove(fitspo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FitspoExists(int id)
        {
            return _context.Steps_Fitspos.Any(e => e.Id == id);
        }
    }
}
