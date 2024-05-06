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

        public IActionResult Index()
        {
            var listPerson = _personService.GetListPersons();
            var listMalePerson = _personService.GetMalePersons();
            var listFullName = _personService.GetFullnameListPersons();
            var oldestPerson = _personService.GetOldestPerson();
            var threeList = _personService.GetUserByYear();
            //return View(listPerson);
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
