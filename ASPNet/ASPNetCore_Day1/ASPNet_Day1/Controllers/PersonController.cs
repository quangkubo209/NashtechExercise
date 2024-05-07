using ASP_Day1.Models;
using ASPNet_Day1.BusinessLogic;
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
        private readonly ICrudPerson _crudPerson;

        public PersonController(ILogger<PersonController> logger, IPersonService personService, ICrudPerson crudPerson)
        {
            _logger = logger;
            _personService = personService;
            _crudPerson = crudPerson;
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

        public IActionResult GetThreeLists()
        {
            var threeList = _personService.GetUserByYear();
            return Ok(threeList);
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
                _crudPerson.AddPerson(personViewModel);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var person = _crudPerson.GetPersonById(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            return View(_crudPerson.GetPersonById(id));
        }

        [HttpPost]
        public IActionResult UpdatePerson([Bind("Id,LastName,FirstName, Gender, BirthPlace, DateOfBirth,IsGraduated")] int id, PersonViewModel person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _crudPerson.UpdatePerson(person);
                return RedirectToAction("Index");
            }
            return View(person);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = _crudPerson.GetPersonById(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }


        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _crudPerson.DeleteConfirmed(id);
            return RedirectToAction("Index");
        }
    }
}
