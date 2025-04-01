using aspnetcoremvc1.Models;
using aspnetcoremvc1.Repositories;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcoremvc1.Controllers;

public class RookieController : Controller
{
    private IRookieRepository _rookieRepository;

    public RookieController(IRookieRepository rookieRepository)
    {
        _rookieRepository = rookieRepository;
    }

    public IActionResult Index()
    {
        return Json(_rookieRepository.GetAll());
    }

    public IActionResult Male()
    {
        return Json(_rookieRepository.GetMales());
    }

    public IActionResult Oldest()
    {
        return Json(_rookieRepository.GetOldest());
    }

    public IActionResult FullNames()
    {
        return Json(_rookieRepository.GetFullNames());
    }

    public IActionResult ByBirthYear(int year = 2000, string timeRelation = "In")
    {
        switch (timeRelation)
        {
            case "Before":
                return BornBefore(year);
            case "After":
                return BornAfter(year);
            case "In":
            default:
                return BornIn(year);
        }
    }

    public IActionResult BornIn(int year = 2000)
    {
        return Json(_rookieRepository.GetRookiesBornInYear(year));
    }

    public IActionResult BornBefore(int year = 2000)
    {
        return Json(_rookieRepository.GetRookiesBornBeforeYear(year));
    }

    public IActionResult BornAfter(int year = 2000)
    {
        return Json(_rookieRepository.GetRookiesBornAfterYear(year));
    }

    public IActionResult Excel()
    {
        using XLWorkbook workbook = new XLWorkbook();
        IXLWorksheet worksheet = workbook.AddWorksheet("Rookies");

        string[] headers = { "First name", "Last name", "Gender", "Date of Birth", "Phone Number", "Birthplace", "Is Graduated" };
        worksheet.Cell("A1").InsertData(headers, true);

        List<Person> rookies = _rookieRepository.GetAll();

        for (int i = 0; i < rookies.Count; i++)
        {
            Person rookie = rookies[i];

            worksheet.Cell($"A{i + 2}")
                .InsertData(new string[] {
                    rookie.FirstName,
                    rookie.LastName,
                    rookie.Gender.ToString(),
                    rookie.DateOfBirth.ToString(),
                    rookie.PhoneNumber,
                    rookie.BirthPlace,
                    rookie.IsGraduated ? "Yes" : "No"
                }, true);
        }

        MemoryStream stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;

        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Rookies.xlsx");
    }
}



