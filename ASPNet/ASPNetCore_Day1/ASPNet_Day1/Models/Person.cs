using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_Day1.Models
{
    public class Person
    {

        public Person(string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, bool isGraduated, GenderEnum gender, string birthPlace)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
            this.phoneNumber = phoneNumber;
            this.isGraduated = isGraduated;
            this.gender = gender;
            this.birthPlace = birthPlace;
        }
        public Guid Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public GenderEnum gender { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string phoneNumber { get; set; }
        public string birthPlace { get; set; }
        public bool isGraduated { get; set; }
    }


}
