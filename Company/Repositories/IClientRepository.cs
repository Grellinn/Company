using Company.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Repositories
{
	public interface IClientRepository : IDisposable
	{
		IEnumerable<Client> GetClients();
		Client GetClientByID(int clientId);
		void InsertClient(Client client);
		void DeleteClient(int clientID);
		void UpdateClient(Client client);
		void Save();
	}
}