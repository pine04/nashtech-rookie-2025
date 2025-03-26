class Car
{
    public string Make { get; }
    public string Model { get; }
    public int Year { get; }
    public CarType Type { get; }

    public Car(string make, string model, int year, CarType type)
    {
        Make = make;
        Model = model;
        Year = year;
        Type = type;
    }

    public override string ToString()
    {
        return $"Make: {Make}, Model: {Model}, Year: {Year}, Type: {Type}";
    }
}