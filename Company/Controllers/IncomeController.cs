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
using PagedList;

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
		public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
			var incomes = incomeRepo.GetIncomes();


			#region leitarvél
			if (!String.IsNullOrEmpty(searchString))
			{
				incomes = incomes.Where(i => i.Title.ToUpper().Contains(searchString.ToUpper()) || i.Title.ToUpper().Contains(searchString.ToUpper())).ToList();
			}
			#endregion

			#region ViewBags
			ViewBag.CurrentSort = sortOrder;
			ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
			ViewBag.DescriptionSortParm = sortOrder == "Description" ? "description_desc" : "Description";
			ViewBag.RegisteredSortParm = sortOrder == "Registered" ? "registered_desc" : "Registered";
			ViewBag.ClientSortParm = sortOrder == "Client" ? "client_desc" : "Client";
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
					incomes = incomes.OrderByDescending(i => i.Project.Client.Name).ToList();
					break;
				case "Description":
					incomes = incomes.OrderBy(i => i.Description).ToList();
					break;
				case "description_desc":
					incomes = incomes.OrderByDescending(i => i.Description).ToList();
					break;
				case "Registered":
					incomes = incomes.OrderBy(i => i.Registered).ToList();
					break;
				case "registered_desc":
					incomes = incomes.OrderByDescending(i => i.Registered).ToList();
					break;
				case "Project":
					incomes = incomes.OrderBy(i => i.Project.Title).ToList();
					break;
				case "project_desc":
					incomes = incomes.OrderByDescending(i => i.Project.Title).ToList();
					break;
				case "Amount":
					incomes = incomes.OrderBy(i => i.Amount).ToList();
					break;
				case "amount_desc":
					incomes = incomes.OrderByDescending(i => i.Amount).ToList();
					break;
				default:
					incomes = incomes.OrderBy(i => i.Title).ToList();
					break;
			}
			#endregion


			int pageSize = 10;
			int pageNumber = (page ?? 1);
			return View(incomes.ToPagedList(pageNumber, pageSize));
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
