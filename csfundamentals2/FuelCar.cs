class FuelCar : Car, IFuelable
{
    public FuelCar(string make, string model, int year, DateTime lastMaintenanceDate) : base(make, model, year, lastMaintenanceDate) { }

    public void Refuel(DateTime timeOfRefuel)
    {
        Console.WriteLine($"Fuel car {Make} {Model} refueled on {timeOfRefuel:yyyy-MM-dd HH:mm}");
    }
}