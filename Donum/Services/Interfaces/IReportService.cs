using Donum.Models;

namespace Donum.Services;

public interface IReportService
{
	void ProcessDonations(IEnumerable<Donation> data, DateTime reportDate);
}