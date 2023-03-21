using Donum.Data;
using Donum.Services;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;

namespace DonumTest;

public class ReportServiceTest
{
	[Fact]
	public void RunAllMembersTest()
	{
		DbContextOptionsBuilder<DataContext> optionsBuilder = new();
		optionsBuilder.UseSqlite("DataSource=app.db");
		DbContextOptions<DataContext> options = optionsBuilder.Options;

		var operationalStoreOptions = new OperationalStoreOptions();

		Mock<IOptions<OperationalStoreOptions>> operationalStoreOptionsMock = new();
		operationalStoreOptionsMock.Setup(x => x.Value).Returns(operationalStoreOptions);

		var reportService = new ReportService(new DataContext(options, operationalStoreOptionsMock.Object));

		reportService.AllMembersReport(new DateTime(2022, 1, 1));
	}
}