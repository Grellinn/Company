using Company.DAL;
using Company.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Company.Repositories
{
	public class ExpenseRepository : IExpenseRepository, IDisposable
	{
		public CompanyContext context;

		public ExpenseRepository(CompanyContext context)
		{
			this.context = context;
		}

		public IEnumerable<Expense> GetExpenses()
		{
			return context.Expenses.Include(ex => ex.Project).ToList();
		}

		public Expense GetExpenseByID(int id)
		{
			return context.Expenses.Find(id);
		}

		public void InsertExpense(Expense expense)
		{
			context.Expenses.Add(expense);
		}

		public void DeleteExpense(int expenseID)
		{
			Expense expense = context.Expenses.Find(expenseID);
		}

		public void UpdateExpense(Expense expense)
		{
			context.Entry(expense).State = EntityState.Modified;
		}

		public void Save()
		{
			context.SaveChanges();
		}

		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					context.Dispose();
				}
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}