using ASP_Day1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_Day1.Models.Repositorys
{
    public class PersonRepository : IPersonRepository
    {
        public IEnumerable<Person> GetPersons()
        {
            IEnumerable<Person> persons = new List<Person> {
            (new Person("Quang", "Nguyen", new DateTime(2002, 08, 09), "099898981", false, GenderEnum.Male, "Phu Tho")),
            (new Person("Thao", "Le", new DateTime(2000, 08, 09), "099898982", false, GenderEnum.Female, "Ha Tay")),
            (new Person("Hung", "Dang", new DateTime(2004, 08, 09), "099898983", false, GenderEnum.Male, "Ha Noi")),
            (new Person("Huyen", "Nguyen", new DateTime(1999, 08, 09), "099898984", false, GenderEnum.Female, "Ha Nam")),
            (new Person("Tung", "Nguyen", new DateTime(2002, 08, 09), "099898985", false, GenderEnum.Male, "Ha Noi")),
            (new Person("Dat", "Nguyen", new DateTime(2004, 08, 09), "099898986", false, GenderEnum.Female, "Ha Noi")),
            (new Person("Quang", "Tran", new DateTime(2006, 08, 09), "099898987", false, GenderEnum.Male, "Ha Noi")),
            (new Person("Linh", "Nguyen", new DateTime(2008, 08, 09), "099898988", false, GenderEnum.Female, "Ha Noi")),
            (new Person("Quang", "Nguyen", new DateTime(2000, 08, 09), "099898989", false, GenderEnum.Male, "Ha Noi")),
            (new Person("Nhi", "Nguyen", new DateTime(1997, 08, 09), "099898921", false, GenderEnum.Female, "Ha Noi")),
            (new Person("Tuan", "Hoang", new DateTime(1995, 08, 09), "099898924", false, GenderEnum.Male, "Ha Noi")),
            (new Person("Thanh", "Phung", new DateTime(2002, 08, 09), "099898967", false, GenderEnum.Female, "Ha Noi")),
            };
            return persons;
        }
    }
}
