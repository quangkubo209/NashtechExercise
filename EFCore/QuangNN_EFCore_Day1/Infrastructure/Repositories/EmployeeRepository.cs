using Infrastructure.GenericRepository;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly QuangDbContext _context;
        public EmployeeRepository(QuangDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetByDepartmentId(int departmentId)
        {
            return await _context.Employees
                .Where(x => x.DepartmentId == departmentId && !x.IsDeleted).ToListAsync();
        }

        public async Task<Salaries> GetSalaryByEmployeeId(int id)
        {
            return await _context.Salaries.FirstOrDefaultAsync(x => x.EmployeeId == id);

        }

    }
}