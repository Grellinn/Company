using System;
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

namespace Company.Controllers
{
    public class IncomeController : Controller
    {
        private IIncomeRepository incomeRepo;
		private IProjectRepository projectRepo;

		public IncomeController()
		{
			this.incomeRepo = new IncomeRepository(new CompanyContext());
			this.projectRepo = new ProjectRepository(new CompanyContext());
		}
		
		// GET: /Income/
        public ActionResult Index()
        {
			var incomes = incomeRepo.GetIncomes();
            return View(incomes);
        }

        // GET: /Income/Details/5
        public ActionResult Details(int id)
        {
			Income income = incomeRepo.GetIncomeByID(id);
            return View(income);
        }

        // GET: /Income/Create
        public ActionResult Create()
        {
            ViewBag.ProjectID = new SelectList(projectRepo.GetProjects(), "ID", "Title");
            return View();
        }

        // POST: /Income/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Title,Description,Amount,ProjectID")] Income income)
        {
            if (ModelState.IsValid)
            {
				income.Registered = DateTime.Now;
				TryUpdateModel(income);
				incomeRepo.InsertIncome(income);
				incomeRepo.Save();

				#region uppfæra heildartekjur á verkefni
				projectRepo.UpdateProjectTotalIncome(income.ProjectID);
				projectRepo.Save();
				#endregion

				return RedirectToAction("Index");
            }

            ViewBag.ProjectID = new SelectList(projectRepo.GetProjects(), "ID", "Title", income.ProjectID);
            return View(income);
        }

        // GET: /Income/Edit/5
        public ActionResult Edit(int id)
        {
			Income income = incomeRepo.GetIncomeByID(id);
            ViewBag.ProjectID = new SelectList(projectRepo.GetProjects(), "ID", "Title", income.ProjectID);
            return View(income);
        }

        // POST: /Income/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Title,Description,Amount,ProjectID")] Income income)
        {
            if (ModelState.IsValid)
            {
				incomeRepo.UpdateIncome(income);
				incomeRepo.Save();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectID = new SelectList(projectRepo.GetProjects(), "ID", "Title", income.ProjectID);
            return View(income);
        }

        // GET: /Income/Delete/5
        public ActionResult Delete(int id)
        {
			Income income = incomeRepo.GetIncomeByID(id);
            return View(income);
        }

        // POST: /Income/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			Income income = incomeRepo.GetIncomeByID(id);
			incomeRepo.DeleteIncome(id);
			incomeRepo.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                incomeRepo.Dispose();
				projectRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
