using System.Globalization;
using Donum.Data;
using Donum.Helper;
using Donum.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Donum.Services;

public class ImporterService : IImporterService
{
	private readonly DataContext _dbContext;

	public ImporterService(DataContext dbContext) => _dbContext = dbContext;

	public void Import()
	{
		DonumDto   payload  = LoadJson();
		Collection members  = payload.Collections.FirstOrDefault(c => c.Name == "member");
		Collection donation = payload.Collections.FirstOrDefault(c => c.Name == "donation");

		if (members != null) {
			foreach (JObject memberJson in members.Docs) {
				var      memberDto = memberJson.ToObject<MemberDto>();
				TextInfo textInfo  = new CultureInfo("en-US", false).TextInfo;
				var member = new Member
				{
					Address   = textInfo.ToTitleCase(memberDto.Address ?? ""),
					FirstName = textInfo.ToTitleCase(memberDto.FirstName ?? ""),
					LastName  = textInfo.ToTitleCase(memberDto.LastName ?? ""),
					PhoneNumber = string.Empty
				};
				List<JObject>? donations = donation?.Docs.Where(d => d["memberId"].ToString() == memberDto.Id).ToList();
				if (donations != null && donations.Any()) {
					List<Donation> listDonation = donations.Select(d => new Donation
					{
						Member         = member,
						Sum            = Convert.ToInt32(d["sum"]?.ToString() ?? ""),
						Scope          = textInfo.ToTitleCase(d["scope"]?.ToString() ?? ""),
						CheckNo        = d["checkNo"]?.ToString() ?? string.Empty,
						DateOfReceived = DateTime.Parse(d["dateOfReceived"]?.ToString() ?? ""),
						CreateDate     = (d["createdDate"]?.ToString() ?? "").UnixTimeStampToDateTime()
					}).ToList();


					if (listDonation.Any()) {
						_dbContext.Donations.AddRange(listDonation);
					}
				}
			}

			_dbContext.SaveChanges();
		}
	}

	private DonumDto LoadJson()
	{
		using var r =
			new StreamReader(
				@"C:\Users\Emanuel.Mahalean\RiderProjects\Donum\Donum\Data\Import\export_1673197150625.json");
		var json = r.ReadToEnd();
		return JsonConvert.DeserializeObject<DonumDto>(json);
	}
}