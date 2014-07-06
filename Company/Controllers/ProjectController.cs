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
					projects = projects.OrderBy(p => p.TotalPrice).ToList();
					break;
				case "totalPrice_desc":
					projects = projects.OrderByDescending(p => p.TotalPrice).ToList();
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
        public ActionResult Create([Bind(Include="ID,Title,Address,ZipCode,PhotoPath,ClientID,Details,OfferPrice,HourPrice,TotalPrice,NumberOfHours,Status,RegisteredDate,FinishDate")] Project project)
        {
            if (ModelState.IsValid)
            {
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
        public ActionResult Edit([Bind(Include="ID,Title,Address,ZipCode,PhotoPath,ClientID,Details,OfferPrice,HourPrice,TotalPrice,NumberOfHours,Status,RegisteredDate,FinishDate")] Project project)
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                projectRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
