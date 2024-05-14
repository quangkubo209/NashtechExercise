using Infrastructure.GenericModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Salaries : BaseEntity
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Salary must be greater than 0.")]
        public int Salary { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
