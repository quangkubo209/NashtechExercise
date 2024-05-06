using ASP_Day1.Models;
using ASP_Day1.Models.Repositorys;
using ClosedXML.Excel;

namespace ASPNet_Day1.BusinessLogic
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        IEnumerable<Person> IPersonService.GetListPersons()
        {
            return _personRepository.GetPersons();
        }

        IEnumerable<Person> IPersonService.GetMalePersons()
        {
            return _personRepository.GetPersons().Where(p => p.gender == GenderEnum.Male);
        }

        Person IPersonService.GetOldestPerson()
        {
            List<Person> listPersons = new List<Person>();
            listPersons = _personRepository.GetPersons().ToList();
            Person oldestPerson = listPersons.First(m => m.dateOfBirth == listPersons.Min(m => m.dateOfBirth));

            return oldestPerson;
        }

        IEnumerable<String> IPersonService.GetFullnameListPersons()
        {
            List<Person> listPersons = new List<Person>();
            listPersons = _personRepository.GetPersons().ToList();
            return listPersons.Select(member => member.lastName + " " + member.firstName).ToList();

        }

        (List<Person>, List<Person>, List<Person>) IPersonService.GetUserByYear()
        {
            List<Person> listPersons = new List<Person>();
            listPersons = _personRepository.GetPersons().ToList();
            List<Person> year2000List = listPersons.Where(member => member.dateOfBirth.Year == 2000).ToList();
            List<Person> lessYear2000 = listPersons.Where(member => member.dateOfBirth.Year < 2000).ToList();
            List<Person> greaterYear2000 = listPersons.Where(member => member.dateOfBirth.Year > 2000).ToList();
            return (year2000List, lessYear2000, greaterYear2000);
        }

        XLWorkbook IPersonService.GetExcelFile()
        {
            List<Person> listPersons = _personRepository.GetPersons().ToList();

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("List person");

            worksheet.Cell(1, 1).Value = "First Name";
            worksheet.Cell(1, 2).Value = "Last Name";
            worksheet.Cell(1, 3).Value = "Gender";
            worksheet.Cell(1, 4).Value = "Date of birth";
            worksheet.Cell(1, 5).Value = "Phone Number";
            worksheet.Cell(1, 6).Value = "Birth Place";
            worksheet.Cell(1, 7).Value = "Is graduated";

            int rowNum = 2; 

            foreach (Person person in listPersons)
            {
                worksheet.Cell(rowNum, 1).Value = person.firstName;
                worksheet.Cell(rowNum, 2).Value = person.lastName;
                worksheet.Cell(rowNum, 3).Value = person.gender.ToString();
                worksheet.Cell(rowNum, 4).Value = person.dateOfBirth;
                worksheet.Cell(rowNum, 5).Value = person.phoneNumber;
                worksheet.Cell(rowNum, 6).Value = person.birthPlace;
                worksheet.Cell(rowNum, 7).Value = person.isGraduated;

                rowNum++; // Move to the next row
            }


            return workbook;
        }
    }
}
