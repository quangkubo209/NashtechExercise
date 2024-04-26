using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamental_Day2
{
    public enum Gen
    {
        male, female
    };
    public class Member

    {
        public Member()
        {
        }

        public Member(string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, bool isGraduated, Gen gender, string birthPlace)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
            this.phoneNumber = phoneNumber;
            this.isGraduated = isGraduated;
            this.gender = gender;
            this.birthPlace = birthPlace;
        }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public Gen gender { get; set; }
        public DateTime dateOfBirth { get; set; }

        public string phoneNumber { get; set; }
        public string birthPlace { get; set; }

        public int age { get; set; }
        public bool isGraduated { get; set; }

    }
}

























