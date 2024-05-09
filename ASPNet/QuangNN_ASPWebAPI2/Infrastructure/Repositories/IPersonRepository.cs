using QuangNN_ASPWebAPI2.Infrastructure.Models;


namespace Infrastructure.Repositories
{
    public interface IPersonRepository
    {
        public List<Person> GetPersons();

        public int UpdateById(Guid Id, Person person);

        public int DeleteById(Guid id);

        public Person GetById(Guid Id);
    }
}
