using ASPNet_Day1.Models;
using ASPNet_Day1.Models.Repositorys;
using DocumentFormat.OpenXml.VariantTypes;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ASPNet_Day1.BusinessLogic
{
    public class CrudPerson : ICrudPerson
    {
        private readonly IPersonRepository _personRepository;
        private List<Person> _persons;

        public CrudPerson(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
            _persons = _personRepository.GetPersons();
        }
        public void AddPerson(PersonViewModel personViewModel)
        {
            if(personViewModel != null)
            {
                Person newPerson = new Person();
                newPerson.FirstName = personViewModel.FirstName;
                newPerson.LastName = personViewModel.LastName;
                newPerson.Gender = personViewModel.Gender;
                newPerson.PhoneNumber = personViewModel.PhoneNumber;
                newPerson.DateOfBirth = personViewModel.DateOfBirth;
                newPerson.BirthPlace = personViewModel.BirthPlace;
                newPerson.IsGraduated = personViewModel.IsGraduated;

                _persons.Add(newPerson);
            }
        }
        public Person GetPersonById(int id)
        {
            var person = _persons.FirstOrDefault(p => p.Id == id);
            return person;
        }


        public void UpdatePerson(PersonViewModel personViewModel)
        {
            var existingPerson = _persons.FirstOrDefault(p => p.Id == personViewModel.Id);
            if (existingPerson != null)
            {
                existingPerson.FirstName = personViewModel.FirstName;
                existingPerson.LastName = personViewModel.LastName;
                existingPerson.Gender = personViewModel.Gender;
                existingPerson.BirthPlace = personViewModel.BirthPlace;
                existingPerson.DateOfBirth = personViewModel.DateOfBirth;
                existingPerson.IsGraduated = personViewModel.IsGraduated;
            }
        }

        public void DeleteConfirmed(int id)
        {
            var personToDelete = _persons.FirstOrDefault(p => p.Id == id);
            if (personToDelete != null)
            {
                _persons.Remove(personToDelete);
            }
        }
    }

}
