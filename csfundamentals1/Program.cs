using System.Text.RegularExpressions;

class CarApplication
{
    static List<Car> cars = new List<Car>();

    public static void Main(string[] args)
    {
        // cars.Add(new Car("Tesla", "Model S", 2020, CarType.Electric));
        // cars.Add(new Car("Tesla", "Model X", 2023, CarType.Electric));
        // cars.Add(new Car("Mercedes", "Model ABC", 2024, CarType.Fuel));
        // cars.Add(new Car("Mercedes", "Model abc", 2025, CarType.Fuel));

        while (true)
        {
            Console.Clear();

            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add a car");
            Console.WriteLine("2. View all cars");
            Console.WriteLine("3. Search car by Make");
            Console.WriteLine("4. Filter cars by Type");
            Console.WriteLine("5. Remove cars by Model");
            Console.WriteLine("6. Exit");

            int choice;
            while (true)
            {
                Console.WriteLine("Enter your choice:");
                Console.Write("> ");

                string? input = Console.ReadLine()?.Trim();
                if (input == null || !int.TryParse(input, out choice))
                {
                    Console.WriteLine("Choice must be an integer.");
                    continue;
                }

                if (choice < 1 || choice > 6)
                {
                    Console.WriteLine(choice);
                    Console.WriteLine("Please select an integer from 1 to 6.");
                }
                else
                {
                    break;
                }
            }

            switch (choice)
            {
                case 1:
                    AddCar();
                    break;
                case 2:
                    ViewAllCars();
                    break;
                case 3:
                    SearchByMake();
                    break;
                case 4:
                    FilterByType();
                    break;
                case 5:
                    RemoveByModel();
                    break;
                case 6:
                    Console.WriteLine("Bye");
                    return;
            }
        }
    }

    public static void AddCar()
    {
        CarType type;
        while (true)
        {
            Console.WriteLine("Enter car type (Fuel/Electric):");
            Console.Write("> ");

            string? input = Console.ReadLine()?.Trim();

            if (!Enum.TryParse(input, true, out type) || !Enum.IsDefined(typeof(CarType), type))
            {
                Console.WriteLine("Invalid value. Must be Fuel or Electric.");
            }
            else
            {
                break;
            }
        }

        string? make;
        while (true)
        {
            Console.WriteLine("Enter Make:");
            Console.Write("> ");

            make = Console.ReadLine()?.Trim();

            if (!string.IsNullOrEmpty(make))
            {
                break;
            }
        }

        string? model;
        while (true)
        {
            Console.WriteLine("Enter Model:");
            Console.Write("> ");

            model = Console.ReadLine()?.Trim();

            if (!string.IsNullOrEmpty(model))
            {
                break;
            }
        }

        int year;
        Regex yearRegex = new Regex("^\\d{4}$");
        while (true)
        {
            Console.WriteLine("Enter Year:");
            Console.Write("> ");

            string? input = Console.ReadLine()?.Trim();

            if (input != null && yearRegex.IsMatch(input))
            {
                year = int.Parse(input);
                break;
            }
            else
            {
                Console.WriteLine("Invalid year. Please enter a 4-digit integer.");
            }
        }

        cars.Add(new Car(make, model, year, type));
        Console.WriteLine("Car added successfully. Press ENTER to continue.");
        Console.ReadLine();
    }

    public static void ViewAllCars()
    {
        Console.WriteLine("===LIST OF CARS===");

        if (cars.Count == 0)
        {
            Console.WriteLine("Empty.");
        }
        else
        {
            foreach (Car car in cars)
            {
                Console.WriteLine(car.ToString());
            }
        }

        Console.WriteLine("Press ENTER to continue.");
        Console.ReadLine();
    }

    public static void SearchByMake()
    {
        Console.WriteLine("Makes in list:");

        HashSet<string> uniqueMakes = new HashSet<string>(cars.Select((car) => car.Make.ToLower()));
        if (uniqueMakes.Count != 0)
        {
            foreach (string m in uniqueMakes)
            {
                Console.WriteLine($"  {m}");
            }
        }
        else
        {
            Console.WriteLine("  No makes in the list yet.");
        }

        string? make;
        while (true)
        {
            Console.WriteLine("Enter Make to search by:");
            Console.Write("> ");

            make = Console.ReadLine()?.Trim();

            if (!string.IsNullOrEmpty(make))
            {
                break;
            }
        }

        IEnumerable<Car> results = cars.Where((car) => car.Make.Equals(make, StringComparison.CurrentCultureIgnoreCase));

        Console.WriteLine("===RESULT LIST===");
        if (!results.Any())
        {
            Console.WriteLine("Empty.");
        }
        else
        {
            foreach (Car car in results)
            {
                Console.WriteLine(car.ToString());
            }
        }

        Console.WriteLine("Press ENTER to continue.");
        Console.ReadLine();
    }

    public static void FilterByType()
    {
        CarType type;
        while (true)
        {
            Console.WriteLine("Enter car type (Fuel/Electric):");
            Console.Write("> ");

            string? input = Console.ReadLine()?.Trim();

            if (!Enum.TryParse(input, true, out type) || !Enum.IsDefined(typeof(CarType), type))
            {
                Console.WriteLine("Invalid value. Must be Fuel or Electric.");
            }
            else
            {
                break;
            }
        }

        IEnumerable<Car> results = cars.Where((car) => car.Type == type);

        Console.WriteLine("===RESULT LIST===");
        if (!results.Any())
        {
            Console.WriteLine("Empty.");
        }
        else
        {
            foreach (Car car in results)
            {
                Console.WriteLine(car.ToString());
            }
        }

        Console.WriteLine("Press ENTER to continue.");
        Console.ReadLine();
    }

    public static void RemoveByModel()
    {
        string? model;
        while (true)
        {
            Console.WriteLine("Enter Model to remove:");
            Console.Write("> ");

            model = Console.ReadLine()?.Trim();

            if (!string.IsNullOrEmpty(model))
            {
                break;
            }
        }

        int oldCount = cars.Count;

        cars.RemoveAll((car) => car.Model.Equals(model, StringComparison.CurrentCultureIgnoreCase));

        int newCount = cars.Count;

        Console.WriteLine($"Successfully removed {oldCount - newCount} cars with model {model}. Press ENTER to continue.");
        Console.ReadLine();
    }
}