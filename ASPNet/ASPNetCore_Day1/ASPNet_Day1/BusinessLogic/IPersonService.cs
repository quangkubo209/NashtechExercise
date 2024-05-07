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

        public (List<Person>, List<Person>, List<Person>) GetUserByYear();

        public XLWorkbook GetExcelFile();
    }

}
