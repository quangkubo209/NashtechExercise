using ASPNet_Day1.BusinessLogic;
using ASPNet_Day1.Models;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASPNet_Day1.Controllers
{
    public class PersonController : Controller
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        //[Route("Person")]
        //public IActionResult Index()
        //{
        //    var listPerson = _personService.GetListPersons();
        //    var listMalePerson = _personService.GetMalePersons();
        //    var listFullName = _personService.GetFullnameListPersons();
        //    var oldestPerson = _personService.GetOldestPerson();
        //    var threeList = _personService.GetUserByYear();
        //    _personService.GetExcelFile();
        //    //return View(listPerson);
        //    return Ok(listFullName);
        //}
        [HttpGet]
        [Route("Person")]
        public IActionResult ExportExcel()
        {
            XLWorkbook wb = _personService.GetExcelFile();
            MemoryStream ms = new MemoryStream();
            wb.SaveAs(ms);
            // Reset the stream position before returning
            ms.Position = 0;
            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ListPeople.xlsx");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
