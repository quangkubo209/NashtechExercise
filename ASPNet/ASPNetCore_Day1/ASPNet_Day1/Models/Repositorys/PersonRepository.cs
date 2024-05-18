using ASP_Day1.Models;
using ASPNet_Day1.BusinessLogic;
using ASPNet_Day1.Common;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2013.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNet_Day1.Models.Repositorys
{
    public class PersonRepository : IPersonRepository
    {
        List<Person> _persons =
        [
            (new Person(1,"Quang", "Nguyen", new DateTime(2002, 08, 09), "099898981", false, GenderEnum.Male, "Phu Tho")),
            (new Person(2,"Thao", "Le", new DateTime(2000, 08, 09), "099898982", false, GenderEnum.Female, "Ha Tay")),
            (new Person(3,"Hung", "Dang", new DateTime(2004, 08, 09), "099898983", false, GenderEnum.Male, "Ha Noi")),
            (new Person(4,"Huyen", "Nguyen", new DateTime(1999, 08, 09), "099898984", false, GenderEnum.Female, "Ha Nam")),
            (new Person(5,"Tung", "Nguyen", new DateTime(2002, 08, 09), "099898985", false, GenderEnum.Male, "Ha Noi")),
            (new Person(6,"Dat", "Nguyen", new DateTime(2004, 08, 09), "099898986", false, GenderEnum.Female, "Ha Noi")),
            (new Person(7,"Quang", "Tran", new DateTime(2006, 08, 09), "099898987", false, GenderEnum.Male, "Ha Noi")),
            (new Person(8,"Linh", "Nguyen", new DateTime(2008, 08, 09), "099898988", false, GenderEnum.Female, "Ha Noi")),
            (new Person(9,"Quang", "Nguyen", new DateTime(2000, 08, 09), "099898989", false, GenderEnum.Male, "Ha Noi")),
            (new Person(10,"Nhi", "Nguyen", new DateTime(1997, 08, 09), "099898921", false, GenderEnum.Female, "Ha Noi")),
            (new Person(11,"Tuan", "Hoang", new DateTime(1995, 08, 09), "099898924", false, GenderEnum.Male, "Ha Noi")),
            (new Person(12,"Thanh", "Phung", new DateTime(2002, 08, 09), "099898967", false, GenderEnum.Female, "Ha Noi")),
            ];
        public List<Person> GetPersons()
        {            
            return _persons;
        }
        public int AddPerson(Person person)
        {
            if (person == null) return ConstantsStatus.Failed;
            _persons.Add(person);
            return ConstantsStatus.Success;
        }

        public int UpdatePerson(Person personUpdate)
        {
            Person existingPerson = _persons.FirstOrDefault( x => x.Id == personUpdate.Id);
            if(existingPerson == null)
            { return ConstantsStatus.Failed; }
            existingPerson.FirstName = personUpdate.FirstName;
            existingPerson.LastName = personUpdate.LastName;
            existingPerson.Gender = personUpdate.Gender;
            existingPerson.BirthPlace = personUpdate.BirthPlace;
            existingPerson.DateOfBirth = personUpdate.DateOfBirth;
            existingPerson.IsGraduated = personUpdate.IsGraduated;
            return ConstantsStatus.Success;

        }

        public int DeleteConfirmed (int id)
        {
            var personToDete = _persons.FirstOrDefault(p => p.Id == id);
            if(personToDete == null) { return ConstantsStatus.Failed; };
            _persons.Remove(personToDete);
            return ConstantsStatus.Success;
            
        }

        public Person GetPersonById(int id)
        {
            var person = _persons.FirstOrDefault(p => p.Id == id);
            return person;
        }

        public IEnumerable<Person> GetMalePersons()
        {
            return _persons.Where(p => p.Gender == GenderEnum.Male);
        }

        public Person GetOldestPerson()
        {
            List<Person> listPersons = new List<Person>();
            listPersons = _persons.ToList();
            Person oldestPerson = listPersons.First(m => m.DateOfBirth == listPersons.Min(m => m.DateOfBirth));

            return oldestPerson;
        }

        public IEnumerable<String> GetFullnameListPersons()
        {
            List<Person> listPersons = new List<Person>();
            listPersons = _persons.ToList();
            return listPersons.Select(member => member.LastName + " " + member.FirstName).ToList();

        }

        public IEnumerable<Person> GetPersonBaseOnYear(Func<Person, bool> condition)
        {
            var people = _persons.Where(condition).ToList();
            return people;
        }

        public XLWorkbook GetExcelFile()
        {

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

            foreach (Person person in _persons)
            {
                worksheet.Cell(rowNum, 1).Value = person.FirstName;
                worksheet.Cell(rowNum, 2).Value = person.LastName;
                worksheet.Cell(rowNum, 3).Value = person.Gender.ToString();
                worksheet.Cell(rowNum, 4).Value = person.DateOfBirth;
                worksheet.Cell(rowNum, 5).Value = person.PhoneNumber;
                worksheet.Cell(rowNum, 6).Value = person.BirthPlace;
                worksheet.Cell(rowNum, 7).Value = person.IsGraduated;

                rowNum++; // Move to the next row
            }


            return workbook;
        }
    }
}
