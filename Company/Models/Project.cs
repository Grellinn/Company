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

		[Display(Name = "Titill")]
		[Required(ErrorMessage = "Vinsamlegast sláðu inn titil")] // Verður að taka gildi, gefur annars villumeldingu
		[StringLength(60, MinimumLength = 3)]
		public string Title { get; set; }

		[Display(Name = "Heimilisfang")]
		[StringLength(60, MinimumLength = 3)]
		public string Address { get; set; }

		[Display(Name = "Staða")]
		[Required(ErrorMessage = "Vinsamlegast veldu stöðu")] // Verður að taka gildi, gefur annars villumeldingu
		public string Status { get; set; }

		public string PhotoPath { get; set; }

		[Display(Name = "Skrá")]
		public DateTime RegisteredDate { get; set; }

		[Display(Name = "Lokið þann")]
		public DateTime FinishDate { get; set; }

		[Display(Name = "Viðskiptavinur")]
		public virtual Client Client { get; set; }
	}
}