using aspnetcoremvc2.Models;
using ClosedXML.Excel;

namespace aspnetcoremvc2.Services;

public class RookieExcelExportService : IExcelExportService<Person>
{
    public Stream ExportToExcel(IEnumerable<Person> data)
    {
        if (data == null || !data.Any())
        {
            return null;
        }

        using XLWorkbook workbook = new XLWorkbook();
        IXLWorksheet worksheet = workbook.AddWorksheet("Rookies");

        worksheet.Cell("A1").InsertData(data.First().GetHeaders(), true);

        int rowNumber = 2;

        foreach (Person rookie in data)
        {
            worksheet.Cell($"A{rowNumber}").InsertData(rookie.ToRow(), true);
            rowNumber++;
        }

        MemoryStream stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;

        return stream;
    }
}