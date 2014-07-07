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
using PagedList;
using Company.Repositories;

namespace Company.Controllers
{
	public class ClientController : Controller
	{
		private IClientRepository clientRepo;

		public ClientController()
		{
			this.clientRepo = new ClientRepository(new CompanyContext());
		}

		// GET: /Client/
		public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
		{
			var clients = clientRepo.GetClients();

			#region leitarvél
			if (!String.IsNullOrEmpty(searchString))
			{
				clients = clients.Where(c => c.Name.ToUpper().Contains(searchString.ToUpper()) || c.Name.ToUpper().Contains(searchString.ToUpper())).ToList();
			}
			#endregion

			#region ViewBags
			ViewBag.CurrentSort = sortOrder;
			ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewBag.PhoneSortParm = sortOrder == "Phone" ? "phone_desc" : "Phone";
			ViewBag.EmailSortParm = sortOrder == "Email" ? "email_desc" : "Email";
			ViewBag.AddressSortParm = sortOrder == "Address" ? "address_desc" : "Address";
			ViewBag.RegisteredSortParm = sortOrder == "Registered" ? "registered_desc" : "Registered";
			ViewBag.CompanySortParm = sortOrder == "Company" ? "company_desc" : "Company";

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
				case "name_desc":
					clients = clients.OrderByDescending(c => c.Name).ToList();
					break;
				case "Phone":
					clients = clients.OrderBy(c => c.Phone).ToList();
					break;
				case "phone_desc":
					clients = clients.OrderByDescending(c => c.Phone).ToList();
					break;
				case "Email":
					clients = clients.OrderBy(c => c.Email).ToList();
					break;
				case "email_desc":
					clients = clients.OrderByDescending(c => c.Email).ToList();
					break;
				case "Address":
					clients = clients.OrderBy(c => c.Address).ToList();
					break;
				case "address_desc":
					clients = clients.OrderByDescending(c => c.Address).ToList();
					break;
				case "Registered":
					clients = clients.OrderBy(c => c.Registered).ToList();
					break;
				case "registered_desc":
					clients = clients.OrderByDescending(c => c.Registered).ToList();
					break;
				case "Company":
					clients = clients.OrderBy(c => c.Company).ToList();
					break;
				case "company_desc":
					clients = clients.OrderByDescending(c => c.Company).ToList();
					break;
				default:
					clients = clients.OrderBy(c => c.Name).ToList();
					break;
			}
			#endregion

			int pageSize = 10;
			int pageNumber = (page ?? 1);
			return View(clients.ToPagedList(pageNumber, pageSize));
		}

		// GET: /Client/Details/5
		public ActionResult Details(int id)
		{
			Client client = clientRepo.GetClientByID(id);
			return View(client);
		}

		// GET: /Client/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: /Client/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Name,Phone")] Client client)
		{
			if (ModelState.IsValid)
			{
				client.Registered = DateTime.Now;
				TryUpdateModel(client);
				clientRepo.InsertClient(client);
				clientRepo.Save();
				return RedirectToAction("Index");
			}

			return View(client);
		}

		// GET: /Client/Edit/5
		public ActionResult Edit(int id)
		{
			Client client = clientRepo.GetClientByID(id);
			return View(client);
		}

		// POST: /Client/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "ID,Name,Address,Phone,Email,Registered,Company")] Client client)
		{
			if (ModelState.IsValid)
			{
				clientRepo.UpdateClient(client);
				clientRepo.Save();
				return RedirectToAction("Index");
			}
			return View(client);
		}

		// GET: /Client/Delete/5
		public ActionResult Delete(bool? saveChangesError = false, int id = 0)
		{
			if (saveChangesError.GetValueOrDefault())
			{
				ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
			}
			Client client = clientRepo.GetClientByID(id);
			return View(client);
		}

		// POST: /Client/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id)
		{
			try
			{
				Client client = clientRepo.GetClientByID(id);
				clientRepo.DeleteClient(id);
				clientRepo.Save();
			}
			catch (DataException /* dex */)
			{
				//Log the error (uncomment dex variable name after DataException and add a line here to write a log.
				return RedirectToAction("Delete", new { id = id, saveChangesError = true });
			}
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			clientRepo.Dispose();
			base.Dispose(disposing);
		}
	}
}
