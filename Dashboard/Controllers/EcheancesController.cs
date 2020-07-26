using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Model;

namespace Dashboard.Controllers
{
    public class EcheancesController : Controller
    {
        private readonly ReminderContext _context;

        public EcheancesController(ReminderContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(Service.Echeances[id]);
        }
    }
}