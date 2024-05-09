
using API.DTOs.RequestDTOs;
using API.DTOs.ResponseDTOs;
using API.Services;
using AutoMapper;
using Infrastructure.Repositories;
using QuangNN_ASPWebAPI2.Infrastructure.Models;

namespace QuangNN_ASPWebAPI2.API.Services
{
    public class PersonService: IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private List<Person> _persons;
        private readonly IMapper _mapper;


        public PersonService(IPersonRepository personRepository, IMapper mapper)
        {

            _personRepository = personRepository;
            _persons = _personRepository.GetPersons();
            _mapper = mapper;
        }

        public IEnumerable<ResponsePersonDTO> GetPersons()
        {
            var persons = _personRepository.GetPersons();
            var personDto = _mapper.Map<IEnumerable<ResponsePersonDTO>>(persons);

            return personDto;
        }


        public Person Create(RequestPersonDTO personDTO)
        {
            var newPerson = _mapper.Map<Person>(personDTO);
            newPerson.Id = Guid.NewGuid();

            if (!string.IsNullOrEmpty(personDTO.DateOfBirth) && DateTime.TryParseExact(personDTO.DateOfBirth, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dateOfBirth))
            {
                newPerson.DateOfBirth = dateOfBirth;
            }
            else
            {
                newPerson.DateOfBirth = DateTime.Now;
            }

            _persons.Add(newPerson);

            return newPerson;
        }

        public int UpdateById(Guid id, RequestPersonDTO personDto)
        {
            var person = _mapper.Map<Person>(personDto);

            if (!string.IsNullOrEmpty(personDto.DateOfBirth) && DateTime.TryParseExact(personDto.DateOfBirth, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dateOfBirth))
            {
                person.DateOfBirth = dateOfBirth;
            }
            else
            {
                person.DateOfBirth = DateTime.Now;
            }

            return _personRepository.UpdateById(id, person);
        }

        public int DeleteById(Guid id)
        {
            return _personRepository.DeleteById(id);
        }

        public ResponsePersonDTO GetById(Guid Id)
        {
            var person =  _personRepository.GetById(Id);
            var personResponse = _mapper.Map<ResponsePersonDTO>(person);
            return personResponse;
        }

        public IEnumerable<ResponsePersonDTO> GetFilterPerson(RequestFilterDTO requestFilterDto)
        {
            IEnumerable<Person> filterPersons = _persons;

            if (!string.IsNullOrEmpty(requestFilterDto.Name))
            {
                filterPersons = filterPersons.Where(p => p.FirstName.Contains(requestFilterDto.Name) || p.LastName.Contains(requestFilterDto.Name));
            }

            if (requestFilterDto.Gender != null)
            {
                filterPersons = filterPersons.Where(p => p.Gender == requestFilterDto.Gender);
            }

            if (!string.IsNullOrEmpty(requestFilterDto.BirthPlace))
            {
                filterPersons = filterPersons.Where(p => p.BirthPlace.Contains(requestFilterDto.BirthPlace));
            }

            IEnumerable<ResponsePersonDTO> filterPersonResponse = _mapper.Map<IEnumerable<ResponsePersonDTO>>(filterPersons);

            return filterPersonResponse;
        } 
    }
}
