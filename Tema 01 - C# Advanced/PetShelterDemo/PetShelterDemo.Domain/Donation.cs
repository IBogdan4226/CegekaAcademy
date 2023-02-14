namespace PetShelterDemo.Domain
{
    public class Donation
    {
        private readonly Dictionary<string, double> _currencyRates = new Dictionary<string, double>
        {
            { "RON", 1.0f },
            { "EUR", 4.90f },
            { "USD", 4.56f },
            { "GBP", 5.55f }
        };

        public Dictionary<string, double> _donations { get; }

        public Donation()
        {
            _donations = new Dictionary<string, double>();
        }

        public double CalculateValue()
        {
            double total = 0;

            foreach (var donation in _donations)
            {
                total += donation.Value * _currencyRates[donation.Key];
            }

            return total;
        }

        public void AddDonation(string currency, double amount)
        {
            if (!_currencyRates.ContainsKey(currency))
            {
                throw new ArgumentException("Invalid currency type");
            }

            if (_donations.ContainsKey(currency))
            {
                _donations[currency] += amount;
            }
            else
            {
                _donations[currency] = amount;
            }
        }

        public  string GetCurrencyChoice()
        {
            Console.WriteLine("Please select a currency option:");
            int i = 1;
            foreach (var currency in _currencyRates)
            {
                Console.WriteLine($"{i}. {currency.Key}");
                i++;
            }

            int choice;
            while (true)
            {
                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out choice) && choice > 0 && choice <= _currencyRates.Count)
                {
                    break;
                }
                Console.WriteLine("Invalid choice, please try again.");
            }

            return _currencyRates.Keys.ElementAt(choice - 1);
        }

        public double GetDonationAmount()
        {
            double amount;
            while (true)
            {
                Console.Write("Enter donation amount: ");
                if (double.TryParse(Console.ReadLine(), out amount))
                {
                    break;
                }
                Console.WriteLine("Invalid amount, please try again.");
            }
            return amount;
        }

    }
}
