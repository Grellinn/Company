using Company.DAL;
using Company.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Company.Repositories
{
	public class ClientRepository : IClientRepository, IDisposable
	{
		private CompanyContext context;

		public ClientRepository(CompanyContext context)
		{
			this.context = context;
		}

		public IEnumerable<Client> GetClients()
		{
			return context.Clients.ToList();
		}

		public Client GetClientByID(int id)
		{
			return context.Clients.Find(id);
		}

		public void InsertClient(Client client)
		{
			context.Clients.Add(client);
		}

		public void DeleteClient(int clientID)
		{
			Client client = context.Clients.Find(clientID);
			context.Clients.Remove(client);
		}

		public void UpdateClient(Client client)
		{
			context.Entry(client).State = EntityState.Modified;
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