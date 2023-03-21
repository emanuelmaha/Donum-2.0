using Donum.Models;

namespace Donum.Services.Interfaces;

public interface IReportService
{
	string ProcessDonations(IEnumerable<Donation> data, DateTime reportDate);
	string AllMembersReport(DateTime reportDate);
}