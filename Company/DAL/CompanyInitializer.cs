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
				new Client { Name="Grétar Már Margrétarson", Email="grellinn@gmail.com", Address="Hátún 6B", ZipCode="105 RVK", Phone=7701614, Registered=DateTime.Now, Company="Iðnlausn.is"},
				new Client { Name="Grétar Karlsson", Email="thakmalun@thakmalun.is", Address="Fannborg 2", ZipCode="200 KÓP", Phone=8992008, Registered=DateTime.Now, Company="Þakmálun.is"},
				new Client { Name="Kristján Sigurðsson", Email="kristjan@kjs.is", Address="Rofabær 3", ZipCode="XXX MOS", Phone=8966078, Registered=DateTime.Now, Company="KJS Verktakar ehf."},
				new Client { Name="Davíð Elvar Másson", Email="david@karfa.is", Address="Hjallavegur 1", ZipCode="104 RVK", Phone=8446794, Registered=DateTime.Now, Company="Karfa.is"},
				new Client { Name="Davíð Helgi Andrésson", Email="davidandresson@gmail.com", Address="Vesturbrún 33", ZipCode="104 RVK", Phone=8235200, Registered=DateTime.Now, Company="Samskip ehf."},
				new Client { Name="Jens Sigurðsson", Email="jenni@naggur.is", Address="Hörðukór 1", ZipCode="XXX KÓP", Phone=6617444, Registered=DateTime.Now, Company="Naggur ehf."}
			};

			clients.ForEach(c => dbContext.Clients.Add(c));
			dbContext.SaveChanges();

			var projects = new List<Project>
			{
				new Project { Title="Mála stein á framhlið", ClientID=1, Details ="Mála allan stein að framan með málara hvítu", StartedDate=DateTime.Parse("2014/07/06"), OfferPrice=0, TotalIncome=0, TotalExpense=0, Address="Hátún 6B", ZipCode="105 RVK", Status="Verkefni hafið", RegisteredDate=DateTime.Now },
				new Project { Title="Mála tréverk á framhlið", ClientID=1, Details ="Mála allt tréverk að framan með toskana gráu", OfferPrice=0, TotalIncome=0, TotalExpense=0, Address="Hátún 6B", ZipCode="105 RVK", Status="Ekki hafið", RegisteredDate=DateTime.Now },
				new Project { Title="Gera við og sprauta þak", ClientID=1, Details ="Þvo, gera við og sprauta þak með dökk gráu", StartedDate=DateTime.Parse("2014/07/06"), OfferPrice=0, TotalIncome=1200000, TotalExpense=240000, Address="Hátún 6B", ZipCode="105 RVK", Status="Verkefni lokið", RegisteredDate=DateTime.Now, FinishDate=DateTime.Parse("2014/07/10"), NumberOfHours=4 },
				new Project { Title="Gera við og sprauta bílskúrsþak", ClientID=1, Details ="Þvo, gera við og sprauta bílskúrsþak með dökk gráu", StartedDate=DateTime.Parse("2014/07/06"), OfferPrice=0, TotalIncome=0, TotalExpense=0, Address="Hátún 6B", ZipCode="105 RVK", Status="Verkefni lokið", RegisteredDate=DateTime.Now, FinishDate=DateTime.Parse("2014/07/10"), NumberOfHours=4 },
				new Project { Title="Mála stein á bílskúr", ClientID=1, Details ="Mála allan stein á bílskúr með málara hvítu", StartedDate=DateTime.Parse("2014/07/06"), OfferPrice=0, TotalIncome=0, Address="Hátún 6B", TotalExpense=0, ZipCode="105 RVK", Status="Verkefni hafið", RegisteredDate=DateTime.Now },
				new Project { Title="Mála tréverk á bílskúr", ClientID=1, Details ="Mála allt tréverk á bílskúr með toskana gráu", OfferPrice=0, TotalIncome=0, Address="Hátún 6B", ZipCode="105 RVK", Status="Ekki hafið", TotalExpense=0, RegisteredDate=DateTime.Now }
			};

			projects.ForEach(p => dbContext.Projects.Add(p));
			dbContext.SaveChanges();

			var incomes = new List<Income>
			{
				new Income { Description="Fyrsti hluti fyrir framkvæmdum á húsi", ProjectID=1, Registered=DateTime.Now, Title="Innborgun frá Hátúni 6B", Amount=200000 },
				new Income { Description="Annar hluti fyrir framkvæmdum á húsi", ProjectID=1, Registered=DateTime.Now, Title="Innborgun frá Hátúni 6B", Amount=200000 },
				new Income { Description="Þriðji hluti fyrir framkvæmdum á húsi", ProjectID=1, Registered=DateTime.Now, Title="Innborgun frá Hátúni 6B", Amount=200000 },
				new Income { Description="Fyrsti hluti fyrir framkvæmdum á bílskúr", ProjectID=1, Registered=DateTime.Now, Title="Innborgun frá Hátúni 6B", Amount=200000 },
				new Income { Description="Annar hluti fyrir framkvæmdum á bílskúr", ProjectID=1, Registered=DateTime.Now, Title="Innborgun frá Hátúni 6B", Amount=200000 },
				new Income { Description="Þriðji hluti fyrir framkvæmdum á bílskúr", ProjectID=1, Registered=DateTime.Now, Title="Innborgun frá Hátúni 6B", Amount=200000 }
			};

			incomes.ForEach(i => dbContext.Incomes.Add(i));
			dbContext.SaveChanges();

			var expenses = new List<Expense>
			{
				new Expense { Title="Málning ehf.", Amount=40000, Description="Málara hvítt Steintex á stein 100L", ProjectID=1, Registered=DateTime.Now },
				new Expense { Title="Málning ehf.", Amount=40000, Description="Toskana grár Kjörvari á tré 20L", ProjectID=1, Registered=DateTime.Now },
				new Expense { Title="Málning ehf.", Amount=40000, Description="Dökk grá þakmálning 40L", ProjectID=1, Registered=DateTime.Now },
				new Expense { Title="Launakostnaður", Amount=40000, Description="40 vinnustundir, Grétar, Davíð og Davíð", ProjectID=1, Registered=DateTime.Now },
				new Expense { Title="Launakostnaður", Amount=40000, Description="40 vinnustundir, Grétar, Davíð og Davíð", ProjectID=1, Registered=DateTime.Now },
				new Expense { Title="Launakostnaður", Amount=40000, Description="30 vinnustundir, Grétar, Davíð og Davíð", ProjectID=1, Registered=DateTime.Now }
			};

			expenses.ForEach(e => dbContext.Expenses.Add(e));
			dbContext.SaveChanges();
		}
	}
}