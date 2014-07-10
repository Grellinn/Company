﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Company.Models;
using Company.DAL;
using Company.Repositories;
using PagedList;

namespace Company.Controllers
{
    public class ExpenseController : Controller
    {
		private IExpenseRepository expenseRepo;
		private IProjectRepository projectRepo;

		public ExpenseController()
		{
			this.expenseRepo = new ExpenseRepository(new CompanyContext());
			this.projectRepo = new ProjectRepository(new CompanyContext());
		}

        // GET: /Expense/
		public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
			var expenses = expenseRepo.GetExpenses();

			#region leitarvél
			if (!String.IsNullOrEmpty(searchString))
			{
				expenses = expenses.Where(e => e.Title.ToUpper().Contains(searchString.ToUpper()) || e.Title.ToUpper().Contains(searchString.ToUpper())).ToList();
			}
			#endregion

			#region ViewBags
			ViewBag.CurrentSort = sortOrder;
			ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
			ViewBag.DescriptionSortParm = sortOrder == "Description" ? "description_desc" : "Description";
			ViewBag.RegisteredSortParm = sortOrder == "Registered" ? "registered_desc" : "Registered";
			ViewBag.ProjectSortParm = sortOrder == "Project" ? "project_desc" : "Project";
			ViewBag.AmountSortParm = sortOrder == "Amount" ? "amoung_desc" : "Amount";

			if (searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			ViewBag.CurrentFilter = searchString;

			#endregion

			#region switch for sortOrder
			switch (sortOrder)
			{
				case "title_desc":
					expenses = expenses.OrderByDescending(e => e.Project.Client.Name).ToList();
					break;
				case "Description":
					expenses = expenses.OrderBy(e => e.Description).ToList();
					break;
				case "description_desc":
					expenses = expenses.OrderByDescending(e => e.Description).ToList();
					break;
				case "Registered":
					expenses = expenses.OrderBy(e => e.Registered).ToList();
					break;
				case "registered_desc":
					expenses = expenses.OrderByDescending(e => e.Registered).ToList();
					break;
				case "Project":
					expenses = expenses.OrderBy(e => e.Project.Title).ToList();
					break;
				case "project_desc":
					expenses = expenses.OrderByDescending(e => e.Project.Title).ToList();
					break;
				case "Amount":
					expenses = expenses.OrderBy(e => e.Amount).ToList();
					break;
				case "amount_desc":
					expenses = expenses.OrderByDescending(e => e.Amount).ToList();
					break;
				default:
					expenses = expenses.OrderBy(e => e.Title).ToList();
					break;
			}
			#endregion

			int pageSize = 10;
			int pageNumber = (page ?? 1);
			return View(expenses.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Expense/Details/5
        public ActionResult Details(int id)
        {
			Expense expense = expenseRepo.GetExpenseByID(id);
            return View(expense);
        }

        // GET: /Expense/Create
        public ActionResult Create()
        {
            ViewBag.ProjectID = new SelectList(projectRepo.GetProjects(), "ID", "Title");
            return View();
        }

        // POST: /Expense/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Title,Description,ProjectID,Amount")] Expense expense)
        {
            if (ModelState.IsValid)
            {
				expense.Registered = DateTime.Now;
				expenseRepo.InsertExpense(expense);
				expenseRepo.Save();

				#region uppfæra heildarkostnað á verkefni
				projectRepo.UpdateProjectTotalExpense(expense.ProjectID);
				projectRepo.Save();
				#endregion

				return RedirectToAction("Index");
            }

            ViewBag.ProjectID = new SelectList(projectRepo.GetProjects(), "ID", "Title", expense.ProjectID);
            return View(expense);
        }

        // GET: /Expense/Edit/5
        public ActionResult Edit(int id)
        {
			Expense expense = expenseRepo.GetExpenseByID(id);
            ViewBag.ProjectID = new SelectList(projectRepo.GetProjects(), "ID", "Title", expense.ProjectID);
            return View(expense);
        }

        // POST: /Expense/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Title,Description,ProjectID,Amount")] Expense expense)
        {
            if (ModelState.IsValid)
            {
				expenseRepo.UpdateExpense(expense);
				expenseRepo.Save();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectID = new SelectList(projectRepo.GetProjects(), "ID", "Title", expense.ProjectID);
            return View(expense);
        }

        // GET: /Expense/Delete/5
        public ActionResult Delete(int id)
        {
			Expense expense = expenseRepo.GetExpenseByID(id);
            return View(expense);
        }

        // POST: /Expense/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			Expense expense = expenseRepo.GetExpenseByID(id);
			expenseRepo.DeleteExpense(id);
			expenseRepo.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                expenseRepo.Dispose();
				projectRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
