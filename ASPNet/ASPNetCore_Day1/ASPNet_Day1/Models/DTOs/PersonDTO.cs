using ASP_Day1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_Day1.Models.DTOs
{
    internal class PersonDTO
    {
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
