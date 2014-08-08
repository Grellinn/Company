using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Company.Models
{
	public class Employee
	{
		public int ID { get; set; }

		[Display(Name = "Nafn")]
		[Required(ErrorMessage = "Vinsamlegast sláðu inn nafn")] // Verður að taka gildi, gefur annars villumeldingu
		[StringLength(60, MinimumLength = 3)]
		public string Name { get; set; }

		[Display(Name = "Heimilisfang")]
		[StringLength(60, MinimumLength = 3)]
		public string Address { get; set; }

		[Display(Name = "Upplýsingar")]
		public string Details { get; set; }

		[Display(Name = "Kennitala")]
		public string SSN { get; set; }

		[Display(Name = "Bankareikningsnr.")]
		public string AccountNr { get; set; }

		[Display(Name = "Sími")]
		[Required(ErrorMessage = "Vinsamlegast sláðu inn símanúmer")] // Verður að taka gildi, gefur annars villumeldingu
		public string Phone { get; set; }

		[Display(Name = "Tölvupóstur")]
		[EmailAddress]
		[StringLength(60, MinimumLength = 3)]
		public string Email { get; set; }

		[Display(Name = "Staða innan fyrirtækis")]
		public string Role { get; set; }

		[Display(Name = "Hóf störf")]
		public DateTime Started { get; set; }

		[Display(Name = "Lauk störfum")]
		public DateTime Quit { get; set; }

		[Display(Name = "Tímakaup")]
		public int WPH { get; set; }

		[Display(Name = "Verkefni skráð á starfsmann")]
		public virtual ICollection<Project> Projects { get; set; }
		
		[Display(Name = "Tímar skráðir á starfsmann")]
		public virtual ICollection<Hour> Hours { get; set; }
	}
}