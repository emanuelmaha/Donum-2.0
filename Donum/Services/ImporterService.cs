using Donum.Data;
using Donum.Helper;
using Donum.Models;
using Newtonsoft.Json;

namespace Donum.Services;

public class ImporterService
{
	private readonly DataContext _dbContext;

	public ImporterService(DataContext dbContext) => _dbContext = dbContext;

	public void Import()
	{
		DonumDto   payload  = LoadJson();
		Collection members  = payload.Collections.FirstOrDefault(c => c.Name == "member");
		Collection donation = payload.Collections.FirstOrDefault(c => c.Name == "donation");

		if (members != null) {
			foreach (MemberDto memberDto in members.Docs) {
				var member = new Member
				{
					Address   = memberDto.Address,
					FirstName = memberDto.FirstName,
					LastName  = memberDto.LastName
				};

				IEnumerable<Donation> donations = (donation?.Docs as ICollection<DonationDto>)
					.Where(d => d.MemberId == memberDto.Id).Select(d => new Donation
					{
						Member         = member,
						DateOfReceived = Convert.ToDouble(d.DateOfReceived).UnixTimeStampToDateTime(),
						Sum            = d.Sum,
						Scope          = d.Scope,
						CheckNo        = d.CheckNo
					});
				_dbContext.Donations.AddRange(donations);
			}

			_dbContext.SaveChanges();
		}
	}

	public DonumDto LoadJson()
	{
		var sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
		var basePath          = Path.Combine(sCurrentDirectory, @"..\DataBase\Import\export_1673197150625.json");
		var sFilePath         = Path.GetFullPath(basePath);

		using var r    = new StreamReader("file.json");
		var       json = r.ReadToEnd();
		return JsonConvert.DeserializeObject<DonumDto>(json);
	}
}