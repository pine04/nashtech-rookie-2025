using System.Globalization;

class CarApplication
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();

            string? make;
            while (true)
            {
                Console.Write("Enter car make: ");
                make = Console.ReadLine()?.Trim();

                if (!string.IsNullOrEmpty(make))
                {
                    break;
                }

                Console.WriteLine("Please specify the car make.");
            }

            Console.WriteLine();

            string? model;
            while (true)
            {
                Console.Write("Enter car model: ");
                model = Console.ReadLine()?.Trim();

                if (!string.IsNullOrEmpty(model))
                {
                    break;
                }

                Console.WriteLine("Please specify the car model.");
            }

            Console.WriteLine();

            int year;
            while (true)
            {
                Console.Write("Enter car year (e.g., 2020): ");
                string? input = Console.ReadLine()?.Trim();

                if (int.TryParse(input, out year) && year >= 1886 && year <= DateTime.Now.Year)
                {
                    break;
                }

                Console.WriteLine("Invalid year! Please enter a valid year between 1886 and the current year.");
            }

            Console.WriteLine();

            DateTime lastMaintenanceDate;
            while (true)
            {
                Console.Write("Enter last maintenance date (yyyy-MM-dd): ");
                string? input = Console.ReadLine()?.Trim();

                if (DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out lastMaintenanceDate))
                {
                    break;
                }

                Console.WriteLine("Invalid date format! Please enter a valid date.");
            }

            Console.WriteLine();

            string? carType;
            while (true)
            {
                Console.Write("Is this a fuel car or electric car? (F/E): ");
                carType = Console.ReadLine()?.Trim().ToLower();

                if (!string.IsNullOrEmpty(carType) && (carType.Equals("f") || carType.Equals("e")))
                {
                    break;
                }

                Console.WriteLine("Invalid input! Please enter 'F' for fuel car or 'E' for electric car.");
            }

            Console.WriteLine();

            Car car;
            if (carType.Equals("f"))
            {
                car = new FuelCar(make, model, year, lastMaintenanceDate);
            }
            else
            {
                car = new ElectricCar(make, model, year, lastMaintenanceDate);
            }

            car.DisplayDetails();

            Console.WriteLine();

            string? choice;
            while (true)
            {
                Console.Write("Do you want to refuel/charge? (Y/N): ");
                choice = Console.ReadLine()?.Trim().ToLower();

                if (!string.IsNullOrEmpty(choice) && (choice.Equals("y") || choice.Equals("n")))
                {
                    break;
                }

                Console.WriteLine("Invalid input! Please enter 'Y' for yes or 'N' for no.");
            }

            if (choice.Equals("n"))
            {
                Console.WriteLine("Goodbye. Press ENTER to continue.");
                Console.ReadLine();
                continue;
            }

            Console.WriteLine();

            DateTime refuelOrChargeTime;
            while (true)
            {
                Console.Write("Enter refuel/charge date and time (yyyy-MM-dd HH:mm): ");
                string? input = Console.ReadLine()?.Trim();

                if (DateTime.TryParseExact(input, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out refuelOrChargeTime))
                {
                    break;
                }

                Console.WriteLine("Invalid date and time format! Please enter a valid date and time.");
            }

            if (car is FuelCar fuelCar)
            {
                fuelCar.Refuel(refuelOrChargeTime);
            }
            else if (car is ElectricCar electricCar)
            {
                electricCar.Charge(refuelOrChargeTime);
            }

            Console.WriteLine("All done. Press ENTER to continue.");
            Console.ReadLine();
        }
    }
}