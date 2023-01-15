using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Donum.Models;

public class DonumDto
{
	public string           Name          { get; set; }
	public string           InstanceToken { get; set; }
	public bool             Encrypted     { get; set; }
	public object           PasswordHash  { get; set; }
	public List<Collection> Collections   { get; set; }
}

public class Collection
{
	public string       Name         { get; set; }
	public string       SchemaHash   { get; set; }
	public bool         Encrypted    { get; set; }
	public object       PasswordHash { get; set; }
	public List<JObject> Docs         { get; set; }
}

public class UserDto
{
	public string Username   { get; set; }
	public string Password   { get; set; }
	public string Name       { get; set; }
	public string Email      { get; set; }
	public int    Permission { get; set; }
	public string Id         { get; set; }
}

public class MemberDto
{
	[JsonProperty("firstName")] public string FirstName { get; set; }

	[JsonProperty("lastName")] public string LastName { get; set; }

	[JsonProperty("address")] public string Address { get; set; }

	[JsonProperty("_id")] public string Id { get; set; }
}

public class NoteDto
{
	public string Text       { get; set; }
	public bool   IsDone     { get; set; }
	public string CreateDate { get; set; }
	public string Id         { get; set; }
}

public class DonationDto
{
	public string MemberName     { get; set; }
	public string CheckNo        { get; set; }
	public string Scope          { get; set; }
	public double Sum            { get; set; }
	public string DateOfReceived { get; set; }
	public int    Id             { get; set; }
	public string MemberId       { get; set; }
	public long   CreatedDate    { get; set; }
	public string _id            { get; set; }
}
