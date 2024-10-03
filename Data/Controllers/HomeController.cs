using Data.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using System.Diagnostics;

namespace Data.Controllers
{
    public class HomeController : Controller
    {
        private readonly PracticalContext db;

 
        public HomeController(PracticalContext _db)
        {
            db = _db;
        }

        public IActionResult Index()
        {
            var std_data = db.Students.Include(c => c.Trainer).Include(d => d.Department).ToList();
            return View(std_data);
        }
        public IActionResult Creates()
        {
            ViewBag.TrainerId = new SelectList(db.Trainers, "TrainerId", "TrainerName");
            ViewBag.departId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");

            return View();
        }

        [HttpPost]

        public IActionResult Store(Student home , IFormFile file)
        {
            var imageName = DateTime.Now.ToString("yymmddhhmmss");//24074455454454
            imageName += Path.GetFileName(file.FileName);//24074455454454apple.png

            string imagepath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/images");
            var imagevalue = Path.Combine(imagepath, imageName);

            using (var stream = new FileStream(imagevalue, FileMode.Create))
            {

                file.CopyTo(stream);

            }
            var dbimage = Path.Combine("/images", imageName);//   /uploads/240715343434apple.png
            home.StudentImages = dbimage;

            db.Students.Add(home);
            db.SaveChanges();


            ViewBag.Id = new SelectList(db.Students, "StudentId", "StudentName");
            return RedirectToAction("Index");

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}