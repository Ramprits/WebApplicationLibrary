using System;
using System.Collections.Generic;
using System.Linq;
using WebApplicationLibrary.Entities;

namespace WebApplicationLibrary.Services
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly LibraryContext _context;

        public DepartmentRepository(LibraryContext context) => _context = context;

        public void AddDepartment(Department department)
        {
            if (department == null) return;
            department.DepartmentId = Guid.NewGuid();
            _context.Departments.Add(department);
            if (!department.Employees.Any()) return;
            foreach (var employee in department.Employees)
            {
                employee.EmployeeId = Guid.NewGuid();
            }
        }

        public void AddEmployeeForDepartment(Guid departmentId, Employee employee)
        {
            var department = GetDepartment(departmentId);
            department?.Employees.Add(employee);
        }

        public void DeleteDepartment(Department department)
        {
            _context?.Departments.Remove(department);
        }

        public void DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
        }

        public bool DepartmentExists(Guid departmentId)
        {
            return _context.Departments.Any(x => x.DepartmentId == departmentId);
        }

        public bool EmployeeExists(Guid employeeId)
        {
            return _context.Employees.Any(x => x.EmployeeId == employeeId);
        }

        public Department GetDepartment(Guid departmentId)
        {
            return _context.Departments.FirstOrDefault(x => x.DepartmentId == departmentId);
        }

        public IEnumerable<Department> GetDepartments()
        {
            return _context.Departments.ToList();
        }

        public IEnumerable<Department> GetDepartments(IEnumerable<Guid> departmentIds)
        {
            return _context.Departments.Where(x => departmentIds.Contains(x.DepartmentId)).OrderBy(x => x.Name)
                .ToList();
        }

        public Employee GetEmployeeForDepartment(Guid departmentId, Guid employeeId)
        {
            return _context.Employees
                .FirstOrDefault(x => x.DepartmentId == departmentId && x.EmployeeId == employeeId);
        }

        public IEnumerable<Employee> GetEmployeesForDepartment(Guid departmentId)
        {
            return _context.Employees.Where(x => x.DepartmentId == departmentId).OrderBy(x => x.FirstName).ToList();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateDepartment(Department department)
        {
            // no code in this implementation
        }

        public void UpdateEmployeeForDepartment(Employee employee)
        {
            // no code in this implementation
        }
    }
}