using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Company.Models
{
	public class Income
	{
		public int ID { get; set; }

		[Display(Name = "Titill")]
		[Required(ErrorMessage = "Sláðu inn titil")] // Verður að taka gildi, gefur annars villumeldingu
		[StringLength(60, MinimumLength = 3)]
		public string Title { get; set; }

		[Display(Name = "Lýsing")]
		[Required(ErrorMessage = "Sláðu inn lýsingu")] // Verður að taka gildi, gefur annars villumeldingu
		[StringLength(100, MinimumLength = 3)]
		public string Description { get; set; }
		
		[Display(Name = "Skráð")]
		public DateTime Registered { get; set; }

		[Display(Name = "Verkefni")]
		[Required(ErrorMessage = "Veldu verkefni")]
		public int ProjectID { get; set; }


		[Display(Name = "Verkefni")]
		public virtual Project Project { get; set; }
	}
}