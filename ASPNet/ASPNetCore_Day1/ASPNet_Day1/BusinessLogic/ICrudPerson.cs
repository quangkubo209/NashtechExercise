using ASPNet_Day1.Models;

namespace ASPNet_Day1.BusinessLogic
{
    public interface ICrudPerson
    {
        void AddPerson(PersonViewModel person);
        void UpdatePerson(PersonViewModel person);
        void DeleteConfirmed(int id);
        public Person GetPersonById(int id);

    }
}
