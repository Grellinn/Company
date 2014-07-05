using Company.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.DAL
{
	public class CompanyInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CompanyContext>
	{
		protected override void Seed(CompanyContext dbContext)
		{
			var clients = new List<Client>
			{
				new Client { Name="Grétar Már Margrétarson", Email="grellinn@gmail.com", Address="Hátún 6B, 105 RVK", Phone=7701614, Registered=DateTime.Now, Company="Iðnlausn.is"},
				new Client { Name="Grétar Karlsson", Email="thakmalun@thakmalun.is", Address="Fannborg 2, 200 KÓP", Phone=8992008, Registered=DateTime.Now, Company="Þakmálun.is"},
				new Client { Name="Kristján Sigurðsson", Email="kristjan@kjs.is", Address="Rofabær 3, XXX MOS", Phone=8966078, Registered=DateTime.Now, Company="KJS Verktakar ehf."},
				new Client { Name="Davíð Elvar Másson", Email="david@karfa.is", Address="Hjallavegur 1, 104 RVK", Phone=8446794, Registered=DateTime.Now, Company="Karfa.is"},
				new Client { Name="Davíð Helgi Andrésson", Email="davidandresson@gmail.com", Address="Vesturbrún 33, 104 RVK", Phone=823500, Registered=DateTime.Now, Company="Samskip ehf."},
				new Client { Name="Jens Sigurðsson", Email="jenni@naggur.is", Address="Hörðukór 1, XXX KÓP", Phone=6617444, Registered=DateTime.Now, Company="Naggur ehf."}
			};

			clients.ForEach(c => dbContext.Clients.Add(c));
			dbContext.SaveChanges();

			var projects = new List<Project>
			{
				new Project { Title="Prentun á 100 stk umslög", Status="Lokið", RegisteredDate=DateTime.Now },
				new Project { Title="Prentun á 100 stk umslög", Status="Lokið", RegisteredDate=DateTime.Now },
				new Project { Title="Setja upp vefsíðu", Status="Lokið", RegisteredDate=DateTime.Now },
				new Project { Title="Setja upp vefsíðu", Status="Lokið", RegisteredDate=DateTime.Now },
				new Project { Title="Hönnun og prentun á nafnspjöld", Status="Lokið", RegisteredDate=DateTime.Now },
				new Project { Title="Hönnun og prentun á nafnspjöld", Status="Lokið", RegisteredDate=DateTime.Now }
			};

			projects.ForEach(p => dbContext.Projects.Add(p));
			dbContext.SaveChanges();

			var statusList = new List<Status>
			{
				new Status { ID=1, Text="Lokið"},
				new Status { ID=1, Text="Ólokið"},
				new Status { ID=1, Text="Ekki hafið"}
			};

			statusList.ForEach(s => dbContext.StatusList.Add(s));
			dbContext.SaveChanges();
		}
	}
}