using Common;
using QuangNN_ASPWebAPI2.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PersonRepository: IPersonRepository
    {
        List<Person> _persons = new List<Person> { };
        public List<Person> GetPersons()
        {
            return _persons;
        }

        public int UpdateById(Guid Id, Person updatePerson)
        {
            var existingPerson = _persons.FirstOrDefault(p => p.Id == Id);
            if (existingPerson != null)
            {
                existingPerson.FirstName = updatePerson.FirstName;
                existingPerson.LastName = updatePerson.LastName;

                return ConstantsStatus.Success;
            }
            return ConstantsStatus.Failed;
        }

        public int DeleteById(Guid id)
        {
            var deleteTask = _persons.FirstOrDefault(task => task.Id == id);
            if (deleteTask != null)
            {
                var taskList = _persons.ToList();
                if (taskList.Remove(deleteTask))
                {
                    _persons = taskList;
                    return ConstantsStatus.Success;
                }
                return ConstantsStatus.Failed;
            }
            return ConstantsStatus.Failed;
        }

        public Person GetById(Guid Id)
        {
            var person = _persons.FirstOrDefault(t => t.Id == Id);
            return person;
        }
    }
}
