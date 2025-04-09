namespace efcore1.Models;

public class Salary
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }

    public double Amount { get; set; }
}