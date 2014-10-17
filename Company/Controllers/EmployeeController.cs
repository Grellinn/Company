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
    public class EmployeeController : Controller
    {
		private IEmployeeRepository employeeRepo;
 
		public EmployeeController()
		{
			this.employeeRepo = new EmployeeRepository(new CompanyContext());
		}

        // GET: /Employee/
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
			var employees = employeeRepo.GetEmployees();

			#region leitarvél
			if (!String.IsNullOrEmpty(searchString))
			{
				employees = employees.Where(e => e.Name.ToUpper().Contains(searchString.ToUpper()) || e.Name.ToUpper().Contains(searchString.ToUpper())).ToList();
			}
			#endregion

			#region ViewBags
			ViewBag.CurrentSort = sortOrder;
			ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewBag.PhoneSortParm = sortOrder == "Phone" ? "phone_desc" : "Phone";
			ViewBag.EmailSortParm = sortOrder == "Email" ? "email_desc" : "Email";
			ViewBag.AddressSortParm = sortOrder == "Address" ? "address_desc" : "Address";
			ViewBag.RoleSortParm = sortOrder == "Role" ? "role_desc" : "Role";
			ViewBag.InfoSortParm = sortOrder == "Info" ? "info_desc" : "Info";

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
					employees = employees.OrderByDescending(e => e.Name).ToList();
					break;
				case "Phone":
					employees = employees.OrderBy(e => e.Phone).ToList();
					break;
				case "phone_desc":
					employees = employees.OrderByDescending(e => e.Phone).ToList();
					break;
				case "Email":
					employees = employees.OrderBy(e => e.Email).ToList();
					break;
				case "email_desc":
					employees = employees.OrderByDescending(e => e.Email).ToList();
					break;
				case "Address":
					employees = employees.OrderBy(e => e.Address).ToList();
					break;
				case "address_desc":
					employees = employees.OrderByDescending(e => e.Address).ToList();
					break;
				case "Role":
					employees = employees.OrderBy(e => e.Role).ToList();
					break;
				case "role_desc":
					employees = employees.OrderByDescending(e => e.Role).ToList();
					break;
				case "Info":
					employees = employees.OrderBy(e => e.Details).ToList();
					break;
				case "company_desc":
					employees = employees.OrderByDescending(e => e.Details).ToList();
					break;
				default:
					employees = employees.OrderBy(c => c.Name).ToList();
					break;
			}
			#endregion

			int pageSize = 10;
			int pageNumber = (page ?? 1);
			return View(employees.ToPagedList(pageNumber, pageSize));
		}

        // GET: /Employee/Details/5
        public ActionResult Details(int id)
        {
			Employee employee = employeeRepo.GetEmployeeByID(id);
            return View(employee);
        }

        // GET: /Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Name,Address,Details,SSN,AccountNr,Phone,Email,Role,WPH")] Employee employee)
        {		
			if (ModelState.IsValid)
            {
				employee.Started = DateTime.Now;
				employee.Quit = employee.Started;
				TryUpdateModel(employee);
				
				employeeRepo.InsertEmployee(employee);
				employeeRepo.Save();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: /Employee/Edit/5
        public ActionResult Edit(int id)
        {
            Employee employee = employeeRepo.GetEmployeeByID(id);
            return View(employee);
        }

        // POST: /Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Name,Address,Details,SSN,AccountNr,Phone,Email,Role,Started,Quit,WPH")] Employee employee)
        {
            if (ModelState.IsValid)
            {
				employeeRepo.UpdateEmployee(employee);
				employeeRepo.Save();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: /Employee/Delete/5
		public ActionResult Delete(bool? saveChangesError = false, int id = 0)
        {
			if (saveChangesError.GetValueOrDefault())
			{
				ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
			}
            Employee employee = employeeRepo.GetEmployeeByID(id);
            return View(employee);
        }

        // POST: /Employee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
			try 
			{
				Employee employee = employeeRepo.GetEmployeeByID(id);
				employeeRepo.DeleteEmployee(id);
				employeeRepo.Save();
			}
			catch (DataException /* dex */)
			{
				//Log the error (uncomment dax variable name after DataException and add a line here to write a log.
				return RedirectToAction("Delete", new { id = id, saveChangesError = true });
			}
			
			return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
			employeeRepo.Dispose();
            base.Dispose(disposing);
        }
    }
}
