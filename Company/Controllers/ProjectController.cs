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
using Company.ViewModels;

namespace Company.Controllers
{
	public class ProjectController : Controller
	{
		private IProjectRepository projectRepo;

		public ProjectController()
		{
			this.projectRepo = new ProjectRepository(new CompanyContext());
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
			ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
			ViewBag.RegisteredSortParm = sortOrder == "Registered" ? "registered_desc" : "Registered";
			ViewBag.StatusSortParm = sortOrder == "Status" ? "status_desc" : "Status";
			ViewBag.ClientSortParm = sortOrder == "Client" ? "client_desc" : "Client";
			ViewBag.AddressSortParm = sortOrder == "Address" ? "address_desc" : "Address";
			ViewBag.PhoneSortParm = sortOrder == "Phone" ? "phone_desc" : "Phone";

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
					projects = projects.OrderByDescending(p => p.Title).ToList();
					break;
				case "Registered":
					projects = projects.OrderBy(p => p.RegisteredDate).ToList();
					break;
				case "registered_desc":
					projects = projects.OrderByDescending(p => p.RegisteredDate).ToList();
					break;
				case "Status":
					projects = projects.OrderBy(p => p.Status).ToList();
					break;
				case "status_desc":
					projects = projects.OrderByDescending(p => p.Status).ToList();
					break;
				case "Client":
					projects = projects.OrderBy(p => p.Client.Name).ToList();
					break;
				case "client_desc":
					projects = projects.OrderByDescending(p => p.Client.Name).ToList();
					break;
				case "Address":
					projects = projects.OrderBy(c => c.Address).ToList();
					break;
				case "address_desc":
					projects = projects.OrderByDescending(c => c.Address).ToList();
					break;
				case "Phone":
					projects = projects.OrderBy(p => p.Client.Phone).ToList();
					break;
				case "phone_desc":
					projects = projects.OrderByDescending(p => p.Client.Phone).ToList();
					break;
				default:
					projects = projects.OrderBy(p => p.Title).ToList();
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
			ProjectVM viewModel = new ProjectVM();
			return View(viewModel);
		}

		// POST: /Project/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Project, Project.Client, Clients, StatusList")] ProjectVM viewModel)
		{
			Project projectToAdd = projectRepo.CreateProjectToInsert(viewModel.Project);
			projectToAdd.Client = new Client();
			projectToAdd.Client.ID = viewModel.Project.Client.ID;

			#region check for errors on Client
			if (ModelState["Project.Client.Name"].Errors.Count == 1)
			{
				ModelState["Project.Client.Name"].Errors.Clear();
			}
			if (ModelState["Project.Client.Address"].Errors.Count == 1)
			{
				ModelState["Project.Client.Address"].Errors.Clear();
			}
			if (ModelState["Project.Client.Email"].Errors.Count == 1)
			{
				ModelState["Project.Client.Email"].Errors.Clear();
			}
			#endregion

			if (ModelState.IsValid)
			{
				projectRepo.InsertProject(projectToAdd);
				projectRepo.Save();
				return RedirectToAction("Index");
			}

			return View(viewModel.Project);
		}

		// GET: /Project/Edit/5
		public ActionResult Edit(int id)
		{
			ProjectVM viewModel = new ProjectVM();
			viewModel.Project = projectRepo.GetProjectByID(id);
			return View(viewModel);
		}

		// POST: /Project/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Project, Project.Client, Clients, StatusList")] ProjectVM viewModel)
		{
			/*ProjectVM tempVM = new ProjectVM();
			viewModel.Project.Client = tempVM.Clients.Where(c => c.ID == viewModel.Project.Client.ID).SingleOrDefault();*/

			#region check for errors on Client
			if (ModelState["Project.Client.Name"].Errors.Count == 1)
			{
				ModelState["Project.Client.Name"].Errors.Clear();
			}
			if (ModelState["Project.Client.Address"].Errors.Count == 1)
			{
				ModelState["Project.Client.Address"].Errors.Clear();
			}
			if (ModelState["Project.Client.Email"].Errors.Count == 1)
			{
				ModelState["Project.Client.Email"].Errors.Clear();
			}
			#endregion

			if (ModelState.IsValid)
			{
				projectRepo.UpdateProject(viewModel.Project);
				projectRepo.Save();
				return RedirectToAction("Index");
			}
			return View(viewModel);
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
	}
}
