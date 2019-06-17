using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFHmwk_4_30_2019.Models;
using EFUploadPicture.Web;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace EFHmwk_4_30_2019.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;

        private IHostingEnvironment _environment;

        public HomeController(IHostingEnvironment environment, IConfiguration configuration)            
        {
            _environment = environment;
           _connectionString = configuration.GetConnectionString("ConStr");
        }        

        public IActionResult Index()
        {
            var repo = new ImageRepository(_connectionString);
           IEnumerable<Image> img = repo.GetImages();
            return View(img);
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile image, string description)
        {
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            string fullPath = Path.Combine(_environment.WebRootPath, "UploadedImages", fileName);
            using (var stream = new FileStream(fullPath, FileMode.CreateNew))
            {
                image.CopyTo(stream);
            }

            var img = new Image
            {
                FileName = fileName,
                Description = description
            };

            var repo = new ImageRepository(_connectionString);
            repo.AddImage(img);

            return Redirect("/");
        }
        
    }
}
