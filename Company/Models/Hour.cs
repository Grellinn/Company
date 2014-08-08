using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Company.Models
{
	public class Hour
	{
		public int ID { get; set; }

		[Display(Name = "Starfsmaður")]
		[Required(ErrorMessage = "Veldu starfsmann")]
		public int EmployeeID { get; set; }

		[Display(Name = "Verkefni")]
		[Required(ErrorMessage = "Veldu verkefni")]
		public int ProjectID { get; set; }

		[Display(Name = "Stimpla inn í byrjun vinnudags")]
		public DateTime DayStart { get; set; }

		[Display(Name = "Stimpla út í morgunkaffi")]
		public DateTime MorgningBreakStop { get; set; }

		[Display(Name = "Stimpla inn eftir morgunkaffi")]
		public DateTime MorgningBreakStart { get; set; }

		[Display(Name = "Stimpla út í hádegismat")]
		public DateTime LunchStop { get; set; }

		[Display(Name = "Stimpla inn eftir hádegismat")]
		public DateTime LunchStart { get; set; }

		[Display(Name = "Stimpla út í síðdegiskaffi")]
		public DateTime DayBreakStop { get; set; }

		[Display(Name = "Stimpla inn eftir síðdegiskaffi")]
		public DateTime DayBreakStart { get; set; }

		[Display(Name = "Stimpla út í lok vinnnudags")]
		public DateTime DayStop { get; set; }

		[Display(Name = "Dagsetning stimplunar")]
		public DateTime Date { get; set; }

		[Display(Name = "Heildarfjöldi tíma dags")]
		public double WorkTime { get; set; }


		[Display(Name = "Verkefni")]
		public virtual Project Project { get; set; }

		[Display(Name = "Employee")]
		public virtual Employee Employee { get; set; }
	}
}