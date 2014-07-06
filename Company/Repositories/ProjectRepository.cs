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
			context.Entry(project).State = EntityState.Modified;
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