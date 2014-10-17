using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Company.DAL;
using Company.Models;
using System.Data.Entity;

namespace Company.Repositories
{
	public class EmployeeRepository : IEmployeeRepository, IDisposable
	{
		private CompanyContext context;

		public EmployeeRepository(CompanyContext context)
		{
			this.context = context;
		}

		public IEnumerable<Employee> GetEmployees()
		{
			return context.Employees.ToList();
		}

		public IEnumerable<Employee> Get5NewestEmployees()
		{
			return context.Employees.OrderByDescending(e => e.Started).Take(5).ToList();
		}

		public Employee GetEmployeeByID(int id)
		{
			return context.Employees.Find(id);
		}

		public void InsertEmployee(Employee employee)
		{
			context.Employees.Add(employee);
		}

		public void DeleteEmployee(int employeeID)
		{
			Employee employee = context.Employees.Find(employeeID);
			context.Employees.Remove(employee);
		}

		public void UpdateEmployee(Employee employee)
		{
			context.Entry(employee).State = EntityState.Modified;
		}

		public void Save()
		{
			context.SaveChanges();
		}

		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if(!this.disposed)
			{
				if(disposing)
				{
					context.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}