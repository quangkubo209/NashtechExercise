using Common;
using Infrastructure.GenericRepository;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<List<Employee>> GetByDepartmentId(int departmentId);
        Task<Salaries> GetSalaryByEmployeeId(int id);

    }
}
