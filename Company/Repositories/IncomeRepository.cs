using Company.DAL;
using Company.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Company.Repositories
{
	public class IncomeRepository : IIncomeRepository, IDisposable
	{
		public CompanyContext context;

		public IncomeRepository(CompanyContext context)
		{
			this.context = context;
		}

		public IEnumerable<Income> GetIncomes()
		{
			return context.Incomes.Include(i => i.Project).ToList();
		}

		public Income GetIncomeByID(int id)
		{
			return context.Incomes.Find(id);
		}

		public void InsertIncome(Income income)
		{
			context.Incomes.Add(income);
		}

		public void DeleteIncome(int incomeID)
		{
			Income income = context.Incomes.Find(incomeID);
			context.Incomes.Remove(income);
		}

		public void UpdateIncome(Income income)
		{
			context.Entry(income).State = EntityState.Modified;
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
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}