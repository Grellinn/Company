using Company.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Repositories
{
	public interface IIncomeRepository : IDisposable
	{
		IEnumerable<Income> GetIncomes();
		Income GetIncomeByID(int incomeID);
		void InsertIncome(Income income);
		void DeleteIncome(int incomeID);
		void UpdateIncome(Income income);
		void Save();
	}
}