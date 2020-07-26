using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using App;
using DAL;
using DAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dashboard.Models;
using Services;

namespace Dashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

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

        public IActionResult Settings()
        {
            return View(AppSettingsParser.Settings);
        }

        [HttpPost]
        public IActionResult Settings(AppSettings model)
        {
            if (ModelState.IsValid)
            {
                AppSettingsParser.AppSettings(model);
            }

            return View(model);
        }

        public IActionResult Facture()
        {
            Service.Echeances = Service.GetEcheances(300);
            if (Service.Echeances.Count == 0)
                return null;
            int i = 0;
            var url = Url.ActionLink("Details", "Echeances", new { id = i });

            var Renderer = new IronPdf.HtmlToPdf();
            var PDF = Renderer.RenderUrlAsPdf(url);
            PDF.Password = Service.Echeances[i].DossierNumero;
            return File(PDF.BinaryData, "application/pdf;");
        }
    }
}