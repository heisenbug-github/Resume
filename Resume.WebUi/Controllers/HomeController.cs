using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Resume.Entities;
using Resume.Services;
using Resume.WebUi.Models;

namespace Resume.WebUi.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IContactService contactService)
        {
            this.contactService = contactService;
        }

        private readonly IContactService contactService;
        public IActionResult Index()
        {
            return View();
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

        [HttpPost]
        public IActionResult SendMessage(Message message)
        {
            this.contactService.SaveMessage(message);
            return View("Index");
        }
    }
}
