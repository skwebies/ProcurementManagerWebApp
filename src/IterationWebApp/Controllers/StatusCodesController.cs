using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace IterationWebApp.Controllers
{
    [RequireHttps]
    public class StatusCodesController : Controller
    {
        // GET: /<controller>/
        public IActionResult StatusCode404()
        {
            return View();
        }

        public IActionResult StatusCode500()
        {
            return View();
        }
    }
}
