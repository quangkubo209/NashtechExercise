using ASP_Day1.Models;
using ASPNet_Day1.BusinessLogic;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNet_Day1.Models.Repositorys
{
    public interface IPersonRepository
    {
        public List<Person> GetPersons();

        public int AddPerson(Person person);

        public int UpdatePerson(Person personUpdate);
        public int DeleteConfirmed(int id);

        public Person GetPersonById(int id);

        public IEnumerable<String> GetFullnameListPersons();

        public Person GetOldestPerson();

        public IEnumerable<Person> GetMalePersons();

        public IEnumerable<Person> GetPersonBaseOnYear(Func<Person, bool> condition);
        public  XLWorkbook GetExcelFile();
    }
}
