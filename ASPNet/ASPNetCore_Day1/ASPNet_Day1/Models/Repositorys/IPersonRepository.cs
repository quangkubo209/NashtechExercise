using ASP_Day1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_Day1.Models.Repositorys
{
    public interface IPersonRepository
    {
        public IEnumerable<Person> GetPersons();
    }
}
