using ASP_Day1.Models;
using ASPNet_Day1.BusinessLogic;
using ASPNet_Day1.Common;
using ASPNet_Day1.Models;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace ASPNet_Day1.Controllers
{
    public class PersonController : Controller
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService, ILogger<PersonController> logger)
        {
            _logger = logger;
            _personService = personService;
        }

        public IActionResult GetList()
        {
            var listPerson = _personService.GetListPersons();
            return View(listPerson);
        }

        public IActionResult GetMalePerson()
        {
            var listMalePerson = _personService.GetMalePersons();
            return View(listMalePerson);
        }

        public IActionResult GetListFullname()
        {
            var listFullName = _personService.GetFullnameListPersons();
            return View(listFullName);
        }

        public IActionResult GetOldestPerson()
        {
            var oldestPerson = _personService.GetOldestPerson();
            return View(oldestPerson);
        }

        [HttpGet]
        public IActionResult GetPersonBaseOnYear(int choice)
        {
            //var threeList = _personService.GetUserByYear();
            //return Ok(threeList);
            Func<Person, bool> condition = s => s.DateOfBirth.Year > 0;
            IEnumerable<Person> persons = new List<Person>();

            switch (choice)
            {
                case -1:
                    condition = s => s.DateOfBirth.Year < 2000;
                    break;
                case 0:
                    condition = s => s.DateOfBirth.Year == 2000;
                    break;
                case 1:
                    condition = s => s.DateOfBirth.Year > 2000;
                    break;
            }
            persons = _personService.GetPersonBaseOnYear(condition);
            int totalCount = _personService.GetPersonBaseOnYear(condition).Count();
            //paging model: pass value like total count, page size, page index to view
            return Ok(persons);
        }


        [HttpGet]
        public IActionResult ExportExcel()
        {
            XLWorkbook wb = _personService.GetExcelFile();
            MemoryStream ms = new MemoryStream();
            wb.SaveAs(ms);
            ms.Position = 0;
            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ListPeople.xlsx");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var listPerson = _personService.GetListPersons();
            return View(listPerson);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreatePerson([Bind("Id,LastName,FirstName, Gender, BirthPlace, DateOfBirth,IsGraduated")]  PersonViewModel personViewModel)
        {
            if (ModelState.IsValid)
            {
                int status = _personService.AddPerson(personViewModel);
                if(status == ConstantsStatus.Success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["errorDetail"] = "FirstName or LastName is required!";
                    return View(personViewModel);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var person = _personService.GetPersonById(id);
            if (person == null)
            {
                return RedirectToAction("Index");
            }

            return View(person);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            return View(_personService.GetPersonById(id));
        }

        [HttpPost]
        public IActionResult UpdatePerson([Bind("Id,LastName,FirstName, Gender, BirthPlace, DateOfBirth,IsGraduated")] int id, PersonViewModel person)
        {
            if (ModelState.IsValid)
            {
                _personService.UpdatePerson(person);
                return RedirectToAction("Index");
            }
            return View(person);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var person = _personService.GetPersonById(id);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _personService.DeleteConfirmed(id);
            return RedirectToAction("Index");
        }
    }
}
