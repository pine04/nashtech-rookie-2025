using aspnetcoremvc2.Models;

namespace aspnetcoremvc2.Services;

public interface IExcelExportService<T> where T : IExcelExportable
{
    public Stream ExportToExcel(IEnumerable<T> data);
}