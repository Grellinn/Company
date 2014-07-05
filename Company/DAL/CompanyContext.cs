using Company.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Company.DAL
{
	public class CompanyContext : DbContext
	{
		public CompanyContext()
			: base("CompanyContext")
		{
		}

		public DbSet<Client> Clients { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<Status> StatusList { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}