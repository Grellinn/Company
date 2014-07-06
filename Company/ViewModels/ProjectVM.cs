using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Company.Models;
using Company.Repositories;

namespace Company.ViewModels
{
	public class ProjectVM
	{
		public Project Project { get; set; }
		public List<Client> Clients { get; set; }

		private IClientRepository clientRepo;
		private IProjectRepository projectRepo;

		public ProjectVM()
		{
			this.clientRepo = new ClientRepository(new DAL.CompanyContext());
			this.projectRepo = new ProjectRepository(new DAL.CompanyContext());
			this.Clients = clientRepo.GetClients().ToList();
			this.Project = new Project();
			this.Project.Client = new Client();
		}
	}
}