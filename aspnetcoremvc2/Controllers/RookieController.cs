using aspnetcoremvc2.Models;
using aspnetcoremvc2.Services;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcoremvc2.Controllers;

public class RookieController : Controller
{
    private IRookieService _rookieService;
    private IExcelExportService<Person> _rookieExcelExportService;

    public RookieController(IRookieService rookieService, IExcelExportService<Person> rookieExcelExportService)
    {
        _rookieService = rookieService;
        _rookieExcelExportService = rookieExcelExportService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Person> rookies = _rookieService.GetAll();
        return View(rookies);
    }

    [HttpGet]
    public IActionResult GetDetails(int id)
    {
        Person rookie = _rookieService.GetById(id);

        if (rookie == null)
        {
            return NotFound();
        }

        return View(rookie);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("FirstName,LastName,Gender,DateOfBirth,PhoneNumber,BirthPlace,IsGraduated")] Person rookie)
    {
        if (ModelState.IsValid)
        {
            _rookieService.Create(rookie);
            return RedirectToAction("Index");
        }

        return View(rookie);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        Person rookie = _rookieService.GetById(id);

        if (rookie == null)
        {
            return NotFound();
        }

        return View(rookie);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,FirstName,LastName,Gender,DateOfBirth,PhoneNumber,BirthPlace,IsGraduated")] Person rookie)
    {
        if (id != rookie.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _rookieService.Update(rookie);
            return RedirectToAction("Index");
        }

        return View(rookie);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        Person deletedRookie = _rookieService.Delete(id);

        if (deletedRookie == null)
        {
            return View("DeleteError");
        }

        return View("DeleteSuccess", deletedRookie);
    }

    [HttpGet]
    public IActionResult Male()
    {
        List<Person> maleRookies = _rookieService.GetMales();
        return View(maleRookies);
    }

    [HttpGet]
    public IActionResult Oldest()
    {
        Person oldestRookie = _rookieService.GetOldest();
        List<Person> data = new List<Person>();

        if (oldestRookie != null)
        {
            data.Add(oldestRookie);
        }

        return View(data);
    }

    [HttpGet]
    public IActionResult FullNames()
    {
        return View(_rookieService.GetFullNames());
    }

    [HttpGet]
    public IActionResult BornIn(int year = 2000)
    {
        List<Person> rookies = _rookieService.GetRookiesBornInYear(year);
        return View(new GetRookiesByYearViewModel { Rookies = rookies, Year = year });
    }

    [HttpGet]
    public IActionResult BornBefore(int year = 2000)
    {
        List<Person> rookies = _rookieService.GetRookiesBornBeforeYear(year);
        return View(new GetRookiesByYearViewModel { Rookies = rookies, Year = year });
    }

    [HttpGet]
    public IActionResult BornAfter(int year = 2000)
    {
        List<Person> rookies = _rookieService.GetRookiesBornAfterYear(year);
        return View(new GetRookiesByYearViewModel { Rookies = rookies, Year = year });
    }

    [HttpGet]
    public IActionResult Excel()
    {
        List<Person> rookies = _rookieService.GetAll();
        Stream excelFile = _rookieExcelExportService.ExportToExcel(rookies);

        return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Rookies.xlsx");
    }
}



