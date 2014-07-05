﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Company.Models
{
	public class Client
	{
		public int ID { get; set; }

		[Display(Name = "Nafn")]
		[Required(ErrorMessage = "Vinsamlegast sláðu inn nafn")] // Verður að taka gildi, gefur annars villumeldingu
		[StringLength(60, MinimumLength = 3)]
		public string Name { get; set; }

		[Display(Name = "Heimilisfang")]
		[Required(ErrorMessage = "Vinsamlegast sláðu inn heimilisfang")] // Verður að taka gildi, gefur annars villumeldingu
		[StringLength(60, MinimumLength = 3)]
		public string Address { get; set; }

		[Display(Name = "Sími")]
		[Required(ErrorMessage = "Vinsamlegast sláðu inn símanúmer")] // Verður að taka gildi, gefur annars villumeldingu
		public int Phone { get; set; }

		[Display(Name = "Tölvupóstur")]
		[EmailAddress]
		[Required(ErrorMessage = "Vinsamlegast sláðu inn tölvupóst")] // Verður að taka gildi, gefur annars villumeldingu
		[StringLength(60, MinimumLength = 3)]
		public string Email { get; set; }

		[Display(Name = "Skráður")]
		public DateTime Registered { get; set; }

		[Display(Name = "Fyrirtæki")]
		[StringLength(20, MinimumLength = 2)]
		public string Company { get; set; }

		public virtual ICollection<Project> Projects { get; set; }
	}
}