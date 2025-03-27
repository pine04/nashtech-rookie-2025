class ElectricCar : Car, IChargable
{
    public ElectricCar(string make, string model, int year, DateTime lastMaintenanceDate) : base(make, model, year, lastMaintenanceDate) { }

    public void Charge(DateTime timeOfCharge)
    {
        Console.WriteLine($"Electric car {Make} {Model} charged on {timeOfCharge:yyyy-MM-dd HH:mm}");
    }
}