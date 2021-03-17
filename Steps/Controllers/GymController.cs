using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Steps.Controllers
{
    public class GymController : Controller
    {
        [HttpGet("/goal")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/gyms/Stockholm")]
        public IActionResult Stockholm()
        {
            return View();
        }

        [HttpGet("/gyms/London")]
        public IActionResult London()
        {
            return View();
        }

        [HttpGet("/gyms/Oslo")]
        public IActionResult Oslo()
        {
            return View();
        }

        [HttpGet("/gyms/news")]
        public IActionResult News()
        {
            return View();
        }

        [HttpGet("/gyms/prices")]
        public IActionResult Prices()
        {
            return View();
        }
    }
}
