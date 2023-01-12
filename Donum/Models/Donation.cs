using System;

namespace Donum.Models;

public class Donation : Entity
{
	public DateTime CreateDate     { get; set; }
	public string   CheckNo        { get; set; }
	public string   Scope          { get; set; }
	public double   Sum            { get; set; }
	public DateTime DateOfReceived { get; set; }

	public Guid   MemberId { get; set; }
	public Member Member   { get; set; }
}