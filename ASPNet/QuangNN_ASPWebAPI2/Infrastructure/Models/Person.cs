namespace QuangNN_ASPWebAPI2.Infrastructure.Models
{
        public class Person
        {
            public Person(string firstName, string lastName, DateTime dateOfBirth,  GenderEnum gender, string birthPlace)
            {
                this.FirstName = firstName;
                this.LastName = lastName;
                this.DateOfBirth = dateOfBirth;
                this.Gender = gender;
                this.BirthPlace = birthPlace;
            }
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public GenderEnum Gender { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string BirthPlace { get; set; }

    }

}
