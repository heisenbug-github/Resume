using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
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
        [ValidateAntiForgeryToken]
        public IActionResult SendMessage([Bind("Body,SenderName,SenderEmail,Subject")]Message message)
        {
            //Message message2 = this.contactService.GetById(Guid.Parse("7f0a9a23-e4ab-45c5-a647-d6e1c19c0f6a"));
            //message2.Subject = "Change tracker tests";
            //message.Id = Guid.Parse("7f0a9a23-e4ab-45c5-a647-d6e1c19c0f6a");
            //message.Subject = "Attach test";
            //this.contactService.UpdateMessage(message);
            ValidationResult validationResult = this.contactService.SendMessage(message);
            string errorMessage = "";
            if(validationResult.IsValid==false)
            {
                foreach (var validationFailure in validationResult.Errors)
                {
                    errorMessage += validationFailure.ErrorMessage + "\n";
                }
            }
            ViewBag.ErrorMessage = errorMessage;

            return View("Index"); // RedirectToAction("Index");
        }
    }
}
