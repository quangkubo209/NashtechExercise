using ASPNet_Day1.Common;
using ASPNet_Day1.Models;
using ASPNet_Day1.Models.Repositorys;
using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ASPNet_Day1.BusinessLogic
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private List<Person> _persons;
        IMapper _mapper;

        public PersonService(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _persons = _personRepository.GetPersons();
            _mapper = mapper;
        }
        IEnumerable<Person> IPersonService.GetListPersons()
        {
            return _personRepository.GetPersons();
        }

        IEnumerable<Person> IPersonService.GetMalePersons()
        {
            return _personRepository.GetPersons().Where(p => p.Gender == GenderEnum.Male);
        }

        Person IPersonService.GetOldestPerson()
        {
            return _personRepository.GetOldestPerson();
        }

        IEnumerable<String> IPersonService.GetFullnameListPersons()
        {
            return _personRepository.GetFullnameListPersons();

        }

        public IEnumerable<Person> GetPersonBaseOnYear(Func<Person, bool> condition)
        {
            return _personRepository.GetPersonBaseOnYear(condition);
        }

        XLWorkbook IPersonService.GetExcelFile()
        {
            return _personRepository.GetExcelFile();
        }
        public int AddPerson(PersonViewModel personViewModel)
        {
            Person person = _mapper.Map<Person>(personViewModel);
            if(person.FirstName == null || person.LastName == null)
            {
                return ConstantsStatus.Failed;
            }
             return _personRepository.AddPerson(person);
        }
        public PersonViewModel GetPersonById(int id)
        {
            var person =  _personRepository.GetPersonById(id);
            return _mapper.Map<PersonViewModel>(person);
        }


        public int UpdatePerson(PersonViewModel personViewModel)
        {
            Person personUpdate = _mapper.Map<Person>(personViewModel);
            return _personRepository.UpdatePerson(personUpdate);
        }

        public int DeleteConfirmed(int id)
        {
            return _personRepository.DeleteConfirmed(id);
        }
    }
}
