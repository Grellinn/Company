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
				new Project { Title="Prentun á 100 stk umslög", ClientID=1, Details ="Lýsing...", FinishDate=DateTime.Parse("2014/07/06"), OfferPrice=1500000, TotalIncome=1500000, Address="Hátún 6B", ZipCode="105 RVK", Status="Lokið", RegisteredDate=DateTime.Now },
				new Project { Title="Prentun á 100 stk umslög", ClientID=1, Details ="Lýsing...", FinishDate=DateTime.Parse("2014/07/06"), OfferPrice=1500000, TotalIncome=1500000, Address="Hátún 6B", ZipCode="105 RVK", Status="Lokið", RegisteredDate=DateTime.Now },
				new Project { Title="Prentun á 100 stk umslög", ClientID=2, Details ="Lýsing...", FinishDate=DateTime.Parse("2014/07/06"), OfferPrice=1500000, TotalIncome=1500000, Address="Hátún 6B", ZipCode="105 RVK", Status="Lokið", RegisteredDate=DateTime.Now },
				new Project { Title="Prentun á 100 stk umslög", ClientID=3, Details ="Lýsing...", FinishDate=DateTime.Parse("2014/07/06"), OfferPrice=1500000, TotalIncome=1500000, Address="Hátún 6B", ZipCode="105 RVK", Status="Lokið", RegisteredDate=DateTime.Now }
			};

			projects.ForEach(p => dbContext.Projects.Add(p));
			dbContext.SaveChanges();

			var incomes = new List<Income>
			{
				new Income { Description="Greiðsla fyrir reikning #123 frá húsfélagi Kleppsvegar 126", ProjectID=1, Registered=DateTime.Now, Title="Húsfélag Kleppsvegar 126 greiddi reikning", Amount=50000},
				new Income { Description="Greiðsla fyrir reikning #123 frá húsfélagi Kleppsvegar 126", ProjectID=2, Registered=DateTime.Now, Title="Húsfélag Kleppsvegar 126 greiddi reikning", Amount=50000},
				new Income { Description="Greiðsla fyrir reikning #123 frá húsfélagi Kleppsvegar 126", ProjectID=3, Registered=DateTime.Now, Title="Húsfélag Kleppsvegar 126 greiddi reikning", Amount=50000},
				new Income { Description="Greiðsla fyrir reikning #123 frá húsfélagi Kleppsvegar 126", ProjectID=4, Registered=DateTime.Now, Title="Húsfélag Kleppsvegar 126 greiddi reikning", Amount=50000},
			};

			incomes.ForEach(i => dbContext.Incomes.Add(i));
			dbContext.SaveChanges();

			var expenses = new List<Expense>
			{
				new Expense { Title="BM Vallá", Amount=30000, Description="Útimúr", ProjectID=1, Registered=DateTime.Now},
				new Expense { Title="BM Vallá", Amount=30000, Description="Útimúr", ProjectID=2, Registered=DateTime.Now},
				new Expense { Title="BM Vallá", Amount=30000, Description="Útimúr", ProjectID=3, Registered=DateTime.Now},
				new Expense { Title="BM Vallá", Amount=30000, Description="Útimúr", ProjectID=4, Registered=DateTime.Now}
			};

			expenses.ForEach(e => dbContext.Expenses.Add(e));
			dbContext.SaveChanges();
		}
	}
}