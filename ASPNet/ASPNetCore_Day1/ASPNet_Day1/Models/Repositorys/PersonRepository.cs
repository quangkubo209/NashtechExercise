using ASP_Day1.Models;
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
    }
}
