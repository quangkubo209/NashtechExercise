using ASP_Day1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNet_Day1.Models
{
    public class Person
    {
        public Person()
        {

        }
        public Person(int id,string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, bool isGraduated, GenderEnum gender, string birthPlace)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.PhoneNumber = phoneNumber;
            this.IsGraduated = isGraduated;
            this.Gender = gender;
            this.BirthPlace = birthPlace;
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string BirthPlace { get; set; }
        public bool IsGraduated { get; set; }
    }


}
