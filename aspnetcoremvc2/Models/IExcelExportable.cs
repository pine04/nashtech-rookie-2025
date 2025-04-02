namespace aspnetcoremvc2.Models;

public interface IExcelExportable
{
    public string[] GetHeaders();

    public string[] ToRow();
}