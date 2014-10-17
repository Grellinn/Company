using Company.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Repositories
{
	public interface IEmployeeRepository : IDisposable
	{
		IEnumerable<Employee> GetEmployees();
		IEnumerable<Employee> Get5NewestEmployees();
		Employee GetEmployeeByID(int employeeID);
		void InsertEmployee(Employee employee);
		void DeleteEmployee(int employeeID);
		void UpdateEmployee(Employee employee);
		void Save();
	}
}