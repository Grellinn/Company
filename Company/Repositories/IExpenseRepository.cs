using Company.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Repositories
{
	public interface IExpenseRepository : IDisposable
	{
		IEnumerable<Expense> GetExpenses();
		Expense GetExpenseByID(int expenseID);
		void InsertExpense(Expense expense);
		void DeleteExpense(int expenseID);
		void UpdateExpense(Expense expense);
		void Save();
	}
}