using Infrastructure.GenericModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Project : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }

    }
}
