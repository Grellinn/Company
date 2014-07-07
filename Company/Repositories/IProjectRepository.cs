﻿using Company.Models;
using Company.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Repositories
{
	public interface IProjectRepository : IDisposable
	{
		List<Project> GetProjects();
		Project GetProjectByID(int projectId);
		void InsertProject(Project project);
		void DeleteProject(int projectID);
		void UpdateProject(Project project);
		void UpdateProjectTotalIncome(int id);
		void UpdateProjectTotalExpense(int id);
		void Save();
	}
}