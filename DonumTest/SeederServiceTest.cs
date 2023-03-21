using Donum.Data;
using Donum.Models;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;

namespace DonumTest;

public class SeederServiceTest
{
	[Fact]
	public void SeedData()
	{
		DbContextOptionsBuilder<DataContext> optionsBuilder = new();
		optionsBuilder.UseSqlite("DataSource=app.db");
		DbContextOptions<DataContext> options = optionsBuilder.Options;

		var operationalStoreOptions = new OperationalStoreOptions();

		Mock<IOptions<OperationalStoreOptions>> operationalStoreOptionsMock = new();
		operationalStoreOptionsMock.Setup(x => x.Value).Returns(operationalStoreOptions);

		var dataContext = new DataContext(options, operationalStoreOptionsMock.Object);

		Member memberMahalean =
			dataContext.Members.First(m => m.LastName == "MAHALEAN");
		memberMahalean.Donations = new List<Donation>
		{
			new() { Scope = "DONATION", DateOfReceived = DateTime.Parse("01/11/2022"), CheckNo = "1102", Sum = 1260 },
			new() { Scope = "DONATION", DateOfReceived = DateTime.Parse("02/18/2022"), CheckNo = "1106", Sum = 1270 },
			new() { Scope = "DONATION", DateOfReceived = DateTime.Parse("03/08/2022"), CheckNo = "CASH", Sum = 340 },
			new() { Scope = "DONATION", DateOfReceived = DateTime.Parse("04/25/2022"), CheckNo = "CASH", Sum = 400 },
			new() { Scope = "DONATION", DateOfReceived = DateTime.Parse("05/15/2022"), CheckNo = "CASH", Sum = 1000 },
			new() { Scope = "DONATION", DateOfReceived = DateTime.Parse("06/15/2022"), CheckNo = "1203", Sum = 580 },
			new() { Scope = "DONATION", DateOfReceived = DateTime.Parse("7/21/2022"), CheckNo  = "1206", Sum = 200 },
			new() { Scope = "DONATION", DateOfReceived = DateTime.Parse("8/22/2022"), CheckNo  = "1211", Sum = 330 },
			new() { Scope = "DONATION", DateOfReceived = DateTime.Parse("9/10/2022"), CheckNo  = "CASH", Sum = 1000 },
			new() { Scope = "DONATION", DateOfReceived = DateTime.Parse("10/30/2022"), CheckNo = "CASH", Sum = 4500 },
			new() { Scope = "DONATION", DateOfReceived = DateTime.Parse("11/21/2022"), CheckNo = "1011", Sum = 1620 },
			new() { Scope = "DONATION", DateOfReceived = DateTime.Parse("12/11/2022"), CheckNo = "CASH", Sum = 1750 }
		};
		dataContext.SaveChanges();
	}
}