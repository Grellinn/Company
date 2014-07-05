using Company.DAL;
using Company.Models;
using Company.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Company.Repositories
{
	public class ProjectRepository : IProjectRepository, IDisposable
	{
		private CompanyContext context;

		public ProjectRepository(CompanyContext context)
		{
			this.context = context;
		}

		public List<Project> GetProjects()
		{
			return context.Projects.ToList();
		}

		public List<Status> GetStatusList()
		{
			return context.StatusList.ToList();
		}

		public Project GetProjectByID(int id)
		{
			return context.Projects.Find(id);
		}

		public void InsertProject(Project project)
		{
			context.Projects.Add(project);
		}

		public void DeleteProject(int projectID)
		{
			Project project = context.Projects.Find(projectID);
			context.Projects.Remove(project);
		}

		public void UpdateProject(Project project)
		{
			//context.Entry(project).State = EntityState.Modified;
			Client tempClient = this.context.Clients.Where(c => c.ID == project.Client.ID).SingleOrDefault();
			Project projectToAdd = GetProjectByID(project.ID);

			projectToAdd.Title = project.Title;
			projectToAdd.Status = project.Status;
			projectToAdd.FinishDate = project.FinishDate;
			projectToAdd.Address = project.Address;

			// Skipta status ID út fyrir status Name
			int tempStatusID = Convert.ToInt32(project.Status);
			projectToAdd.Status = this.context.StatusList.Where(s => s.ID == tempStatusID).SingleOrDefault().Text;

			// Á kannski ekki að vera...
			context.Entry(projectToAdd).State = EntityState.Modified;
		}

		public Project CreateProjectToInsert(Project project)
		{
			Project projectToAdd = new Project();

			#region Fylla inn upplýsingar um verkefni
			projectToAdd.Title = project.Title;
			projectToAdd.Status = project.Status;
			projectToAdd.RegisteredDate = DateTime.Now;
			projectToAdd.FinishDate = project.FinishDate;
			projectToAdd.Address = project.Address;

			// Skipta status ID út fyrir status Name
			int tempStatusID = Convert.ToInt32(project.Status);
			projectToAdd.Status = this.context.StatusList.Where(s => s.ID == tempStatusID).SingleOrDefault().Text;
			#endregion

			return projectToAdd;
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