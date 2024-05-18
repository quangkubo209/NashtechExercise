using ASPNet_Day1.Models;
using ClosedXML.Excel;

namespace ASPNet_Day1.BusinessLogic
{
    public interface IPersonService
    {
        public IEnumerable<Person> GetListPersons();
        public IEnumerable<Person> GetMalePersons();

        public Person GetOldestPerson();
        public IEnumerable<String> GetFullnameListPersons();

        public IEnumerable<Person> GetPersonBaseOnYear(Func<Person, bool> condition);

        public XLWorkbook GetExcelFile();

        public int DeleteConfirmed(int id);

        public int UpdatePerson(PersonViewModel person);

        public PersonViewModel GetPersonById(int id);

        public int AddPerson(PersonViewModel personViewModel);
    }

}
