using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Company.Models
{
	public class Project
	{
		public int ID { get; set; }

		#region properties for POST FORM
		[Display(Name = "Titill")]
		[Required(ErrorMessage = "Sláðu inn titil")] // Verður að taka gildi, gefur annars villumeldingu
		[StringLength(60, MinimumLength = 3)]
		public string Title { get; set; }

		[Display(Name = "Heimilisfang")]
		[StringLength(60, MinimumLength = 3)]
		public string Address { get; set; }

		[Display(Name = "Póstnúmer")]
		public string ZipCode { get; set; }

		[Display(Name = "Mynd")]
		public string PhotoPath { get; set; }

		[Display(Name = "Viðskiptavinur")]
		[Required(ErrorMessage="Veldu viðskiptavin")]
		public int ClientID { get; set; }

		[Display(Name = "Upplýsingar")]
		public string Details { get; set; }

		[Display(Name = "Tilboðsverð")]
		public int? OfferPrice { get; set; }

		[Display(Name = "Tímaverð")]
		public int? HourPrice { get; set; }
		#endregion

		#region Properties that the system uses
		[Display(Name = "Heildarverð")]
		public int? TotalPrice { get; set; }

		[Display(Name = "Heildarfjöldi tíma")]
		public int? NumberOfHours { get; set; }
		
		[Display(Name = "Staða")]
		public string Status { get; set; }

		[Display(Name = "Skráð")]
		public DateTime? RegisteredDate { get; set; }

		[Display(Name = "Lokið þann")]
		public DateTime? FinishDate { get; set; }
		#endregion

		[Display(Name = "Viðskiptavinur")]
		public virtual Client Client { get; set; }

		[Display(Name = "Tekjur frá verkefni")]
		public virtual ICollection<Income> Incomes { get; set; }
	}
}