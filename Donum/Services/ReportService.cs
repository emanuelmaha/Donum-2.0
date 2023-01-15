using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Donum.Data;
using Donum.Models;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace Donum.Services;

public class ReportService : IReportService
{
	private readonly DataContext _dbContext;

	public ReportService(DataContext dbContext) => _dbContext = dbContext;

	public void ProcessDonations(IEnumerable<Donation> data, DateTime reportDate)
	{
		var sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
		var basePath          = Path.Combine(sCurrentDirectory, @"..\Reports");
		// string       sFilePath         = Path.GetFullPath(sFile);
		// const string basePath          = "C:\\Users\\Emanuel Mahalean\\RiderProjects\\donum\\donum\\Reports\\";

		var result = new PdfDocument(new PdfWriter(basePath + "2022_Report_All_Members.pdf"));
		var merger = new PdfMerger(result);

		IOrderedEnumerable<IGrouping<Guid, Donation>> group = data
			.Where(d => d.DateOfReceived.Year == reportDate.Year).GroupBy(d => d.MemberId)
			.OrderBy(grp => grp.Select(d => d.Sum).Sum());

		foreach (IGrouping<Guid, Donation> memberDonations in group) {
			var dest   = basePath + memberDonations.Key + ".pdf";
			var writer = new PdfWriter(dest);
			var pdfDoc = new PdfDocument(writer);
			var doc    = new Document(pdfDoc);
			Image img = new Image(ImageDataFactory
					.Create(@"C:\Users\Emanuel Mahalean\RiderProjects\donum\donum\wwwroot\grpc_logo.PNG"))
				.SetTextAlignment(TextAlignment.LEFT).Scale(0.3F, 0.3F);
			img.SetRelativePosition(10, 20, 10, 10);
			doc.Add(img);

			var newline = new Paragraph(new Text("\n"));
			doc.SetTopMargin(30);
			doc.SetBottomMargin(50);
			doc.ShowTextAligned(new Paragraph("Grace Romanian Pentecostal Church").SetFontSize(20), 135,
				775, 1, TextAlignment.LEFT, VerticalAlignment.TOP, 0);
			doc.Add(new Paragraph("Pastor, Vasile Streango").SetTextAlignment(TextAlignment.RIGHT).SetFontSize(16));
			doc.Add(newline);
			doc.Add(newline);
			doc.Add(new Paragraph(new Text("Donation Report for year " + reportDate.Year))
				.SetTextAlignment(TextAlignment.CENTER)
				.SetFontSize(16));
			doc.Add(newline);
			//todo get the member name
			doc.Add(new Paragraph(memberDonations.Key.ToString()).SetTextAlignment(TextAlignment.LEFT));
			doc.Add(new LineSeparator(new SolidLine()));
			// doc.Add(line);
			float[] pointColumnWidths = { 150F, 150F, 150F, 150F, 150F };
			var     table             = new Table(pointColumnWidths);
			table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("No")));
			table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Date")));
			table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Note")));
			table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Check No")));
			table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Amount")));

			//write Title
			var            count     = 1;
			List<Donation> donations = memberDonations.ToList();
			donations.Sort((x, y) => DateTime.Compare(x.DateOfReceived, y.DateOfReceived));
			foreach (Donation donation in donations) {
				table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER)
					.Add(new Paragraph(count.ToString())));
				table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER)
					.Add(new Paragraph(donation.DateOfReceived.ToString("d"))));
				table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER)
					.Add(new Paragraph(donation.Scope)));
				table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER)
					.Add(new Paragraph(donation.CheckNo)));
				table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER)
					.Add(new Paragraph("$" + donation.Sum.ToString("N"))));
				//write donation
				count++;
			}

			doc.Add(table);


			doc.Add(newline);
			doc.Add(
				new Paragraph("Total Amount: $" + memberDonations.Select(d => d.Sum).Sum().ToString("N"))
					.SetTextAlignment(TextAlignment.RIGHT));

			// Page numbers
			var n = pdfDoc.GetNumberOfPages();
			for (var i = 1; i <= n; i++) {
				doc.ShowTextAligned(new Paragraph(string.Format("page " + i + " of " + n)), 580, 25, i,
					TextAlignment.RIGHT,
					VerticalAlignment.BOTTOM, 0);
				doc.ShowTextAligned(
					new Paragraph("2809 Milroy Ln. Houston, TX. 77066 ~ www.graceromanianchurch.org ~ © GRPC "), 35,
					25, i,
					TextAlignment.LEFT, VerticalAlignment.BOTTOM, 0);
			}

			doc.Close();

			var copyToAll = new PdfDocument(new PdfReader(dest));
			merger.Merge(copyToAll, 1, copyToAll.GetNumberOfPages());
			copyToAll.Close();
		}


		result.Close();
	}
}