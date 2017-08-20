using System;
using System.Collections.Generic;
using WebApplicationLibrary.Entities;

namespace WebApplicationLibrary.Services
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetDepartments();
        Department GetDepartment(Guid departmentId);
        IEnumerable<Department> GetDepartments(IEnumerable<Guid> departmentIds);
        void AddDepartment(Department department);
        void DeleteDepartment(Department department);
        void UpdateDepartment(Department department);
        bool DepartmentExists(Guid departmentId);
        IEnumerable<Employee> GetEmployeesForDepartment(Guid departmentId);
        Employee GetEmployeeForDepartment(Guid departmentId, Guid employeeId);
        void AddEmployeeForDepartment(Guid departmentId, Employee employee);
        void UpdateEmployeeForDepartment(Employee employee);
        void DeleteEmployee(Employee employee);
        bool EmployeeExists(Guid employeeId);
        bool Save();
    }
}
