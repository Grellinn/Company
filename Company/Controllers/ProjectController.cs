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
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Company.Controllers
{
    public class ProjectController : Controller
    {
		private IProjectRepository projectRepo;
		private IClientRepository clientRepo;

		public ProjectController()
		{
			this.projectRepo = new ProjectRepository(new CompanyContext());
			this.clientRepo = new ClientRepository(new CompanyContext());
		}

        // GET: /Project/
		public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
			var projects = projectRepo.GetProjects();

			#region leitarvél
			if (!String.IsNullOrEmpty(searchString))
			{
				projects = projects.Where(p => p.Title.ToUpper().Contains(searchString.ToUpper()) || p.Title.ToUpper().Contains(searchString.ToUpper())).ToList();
			}
			#endregion

			#region ViewBags
			ViewBag.CurrentSort = sortOrder;
			ViewBag.ClientSortParm = String.IsNullOrEmpty(sortOrder) ? "client_desc" : "";
			ViewBag.TitleSortParm = sortOrder == "Title" ? "title_desc" : "Title";
			ViewBag.AddressSortParm = sortOrder == "Address" ? "address_desc" : "Address";
			ViewBag.ZipCodeSortParm = sortOrder == "ZipCode" ? "zipCode_desc" : "ZipCode";
			ViewBag.TotalPriceSortParm = sortOrder == "TotalPrice" ? "totalPrice_desc" : "TotalPrice";
			ViewBag.StatusSortParm = sortOrder == "Status" ? "status_desc" : "Status";

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
				case "client_desc":
					projects = projects.OrderByDescending(p => p.Client.Name).ToList();
					break;
				case "Title":
					projects = projects.OrderBy(p => p.Title).ToList();
					break;
				case "title_desc":
					projects = projects.OrderByDescending(p => p.Title).ToList();
					break;
				case "Address":
					projects = projects.OrderBy(p => p.Address).ToList();
					break;
				case "address_desc":
					projects = projects.OrderByDescending(p => p.Address).ToList();
					break;
				case "ZipCode":
					projects = projects.OrderBy(p => p.ZipCode).ToList();
					break;
				case "zipCode_desc":
					projects = projects.OrderByDescending(p => p.ZipCode).ToList();
					break;
				case "TotalPrice":
					projects = projects.OrderBy(p => p.TotalIncome).ToList();
					break;
				case "totalPrice_desc":
					projects = projects.OrderByDescending(p => p.TotalIncome).ToList();
					break;
				case "Status":
					projects = projects.OrderBy(p => p.Status).ToList();
					break;
				case "status_desc":
					projects = projects.OrderByDescending(p => p.Status).ToList();
					break;
				default:
					projects = projects.OrderBy(p => p.Client.Name).ToList();
					break;
			}
			#endregion


			int pageSize = 10;
			int pageNumber = (page ?? 1);
			return View(projects.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Project/Details/5
        public ActionResult Details(int id)
        {
			ViewBag.jss = GetMorrisData(id);
			Project project = projectRepo.GetProjectByID(id);
            return View(project);
        }

        // GET: /Project/Create
        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(clientRepo.GetClients(), "ID", "Name");
            return View();
        }

        // POST: /Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Title,Address,ZipCode,ClientID,Details")] Project project)
        {
            if (ModelState.IsValid)
            {
				if (project.Address == null)
				{
					project.Address = clientRepo.GetClientByID(project.ClientID).Address;
					project.ZipCode = clientRepo.GetClientByID(project.ClientID).ZipCode;
				}
				project.Status = "Ekki hafið";
				project.TotalExpense = 0;
				project.TotalIncome = 0;
				project.RegisteredDate = DateTime.Now;
				projectRepo.InsertProject(project);
				projectRepo.Save();
                return RedirectToAction("Index");
            }

            ViewBag.ClientID = new SelectList(clientRepo.GetClients(), "ID", "Name", project.ClientID);
            return View(project);
        }

        // GET: /Project/Edit/5
        public ActionResult Edit(int id)
        {
			Project project = projectRepo.GetProjectByID(id);
            ViewBag.ClientID = new SelectList(clientRepo.GetClients(), "ID", "Name", project.ClientID);
            return View(project);
        }

        // POST: /Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Title,Address,ZipCode,ClientID,Details")] Project project)
        {
            if (ModelState.IsValid)
            {
				projectRepo.UpdateProject(project);
				projectRepo.Save();
                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(clientRepo.GetClients(), "ID", "Name", project.ClientID);
            return View(project);
        }

        // GET: /Project/Delete/5
        public ActionResult Delete(int id)
        {
			Project project = projectRepo.GetProjectByID(id);
            return View(project);
        }

        // POST: /Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			Project project = projectRepo.GetProjectByID(id);
			projectRepo.DeleteProject(id);
			projectRepo.Save();
			return RedirectToAction("Index");
        }

		// GET: /Project/ProjectStarted/5
		public ActionResult ProjectStarted(int id)
		{
			Project project = projectRepo.GetProjectByID(id);
			project.Status = "Verkefni hafið";
			project.StartedDate = DateTime.Now;
			projectRepo.UpdateProject(project);
			projectRepo.Save();

			return RedirectToAction("Details/" + project.ID);
		}

		// GET: /Project/ProjectFinished/5
		public ActionResult ProjectFinished(int id)
		{
			Project project = projectRepo.GetProjectByID(id);
			project.Status = "Verkefni lokið";
			project.FinishDate = DateTime.Now;
			
			if (project.RegisteredDate != null || project.StartedDate != null)
			{
				DateTime startDate = project.StartedDate.Value;
				DateTime finishDate = project.FinishDate.Value;
				project.NumberOfHours = finishDate.Subtract(startDate).Days;
			}
			
			projectRepo.UpdateProject(project);
			projectRepo.Save();

			return RedirectToAction("Details/" + project.ID);
		}

		// GET: /Project/MorrisData
		public string GetMorrisData (int id)
		{
			Project tempProject = projectRepo.GetProjectByID(id);
			List<MorrisData> Data = new List<MorrisData>();
			int counter;

			if (tempProject.FinishDate != null)
			{
				double tempLength = (Convert.ToDateTime(tempProject.FinishDate) - Convert.ToDateTime(tempProject.StartedDate)).TotalDays;
				counter = Convert.ToInt32(tempLength);
			}
			else
			{
				double tempLength = (Convert.ToDateTime(tempProject.StartedDate) - DateTime.Now).TotalDays;
				counter = Convert.ToInt32(tempLength);
			}

			for (int i = 0; i < counter; i++)
			{
				if (i == 0)
				{
					MorrisData tempData = new MorrisData();
					DateTime tempDate = Convert.ToDateTime(tempProject.StartedDate);
					tempData.Date = Convert.ToDateTime(tempProject.StartedDate).ToString("dd-MM-yyyy");
					#region get incomes
					foreach (var income in tempProject.Incomes)
					{
						if (income.Registered == tempDate)
						{
							tempData.Income += income.Amount;
						}
					}
					#endregion
					#region get expenses
					foreach (var expense in tempProject.Expenses)
					{
						if (expense.Registered == tempDate)
						{
							tempData.Expense += expense.Amount;
						}
					}
					#endregion
					tempData.ProfitLoss = tempData.Income - tempData.Expense;

					Data.Add(tempData);
				}
				else 
				{
					MorrisData tempData = new MorrisData();
					DateTime tempDate = Convert.ToDateTime(tempProject.StartedDate).AddDays(i);
					tempData.Date = Convert.ToDateTime(tempProject.StartedDate).AddDays(i).ToString("dd-MM-yyyy");					
					
					foreach (var income in tempProject.Incomes)
					{
						if (income.Registered == tempDate)
						{
							tempData.Income += income.Amount;
						}
					}
					tempData.Income += Data[i - 1].Income;
					foreach (var expense in tempProject.Expenses)
					{
						if (expense.Registered == tempDate)
						{
							tempData.Expense += expense.Amount;
						}
					}
					tempData.Expense += Data[i - 1].Expense;
					tempData.ProfitLoss = tempData.Income - tempData.Expense;

					Data.Add(tempData);
				}
			}

			//JavaScriptSerializer jss = new JavaScriptSerializer();

			//string output = jss.Serialize(Data);
			string outputJ = JsonConvert.SerializeObject(Data);
			string demo = outputJ.Replace("\"", "");

			return demo;
		}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                projectRepo.Dispose();
				clientRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
