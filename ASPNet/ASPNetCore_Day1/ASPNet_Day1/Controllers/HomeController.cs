using ASPNet_Day1.BusinessLogic;
using ASPNet_Day1.Models;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASPNet_Day1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPersonService _personService;

        public HomeController(ILogger<HomeController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        public IActionResult GetList()
        {
            var listPerson = _personService.GetListPersons();
            return View(listPerson);
        }

        public IActionResult GetMalePersons()
        {
            var listMalePerson = _personService.GetMalePersons();
            return View(listMalePerson);
        }

        public IActionResult GetFullnameListPersons()
        {
            var listFullName = _personService.GetFullnameListPersons();
            return View(listFullName);
        }

        public IActionResult GetOldestPerson()
        {
            var oldestPerson = _personService.GetOldestPerson();
            return View(oldestPerson);
        }

        public IActionResult GetThreeLists()
        {
            var threeList = _personService.GetUserByYear();
            return Ok(threeList);
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
