using System;
using System.Collections.Generic;

namespace Donum.Models;

public class Member : Entity
{
	public string   FirstName      { get; set; }
	public string   LastName       { get; set; }
	public string   Address        { get; set; }
	public string   PhoneNumber    { get; set; }
	public DateTime DateOfBirthday { get; set; }

	public ICollection<Donation> Donations { get; set; }
}