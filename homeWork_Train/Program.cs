using System;
using System.Collections.Generic;

namespace homeWork_Train
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> nameCities = new List<string>()
            {
                "Екатеринбург",
                "Москва",
                "Санкт-Петербург",
                "Омск",
                "Яросав",
            };

            CashRegister cashRegister = new CashRegister(100, 400);
            RouteCreator pathCreator = new RouteCreator(nameCities);
            Station station = new Station(cashRegister, pathCreator);
            station.Work();
        }
    }

    class Station
    {
        private const string MassegePress = "Press enter for create {0}";
        private const int WagonsCapacity = 30;

        private const string CommadnCreateTrain = "1";
        private const string CommadnExit = "0";

        private CashRegister _cashRegister;
        private RouteCreator _routeCreator;

        private Path _path = new Path();
        private int _amountTickets;
        private int _amountWagons;
        private Train _train;


        public Station(CashRegister cashRegister, RouteCreator routeCreator)
        {
            _cashRegister = cashRegister;
            _routeCreator = routeCreator;
        }


        public void Work()
        {
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("Input:");
                Console.WriteLine($"{CommadnCreateTrain} - crate train");
                Console.WriteLine($"{CommadnExit} - exit");
                string input = Console.ReadLine();

                switch (input)
                {
                    case CommadnCreateTrain:
                        _train = CreateTreain();
                        break;

                    case CommadnExit:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Error Input");
                        break;
                }

                Console.Clear();
            }
        }

        private Train CreateTreain()
        {
            ShowInfo("Path");
            _path = _routeCreator.CreatePath();

            ShowInfo("Tickets");
            _amountTickets = _cashRegister.CreateTickets();

            ShowInfo("Wagons");
            _amountWagons = (int)Math.Ceiling((double)_amountTickets / WagonsCapacity);
            Console.WriteLine("Wagons create: " + _amountWagons);
            Console.ReadKey();

            ShowInfo("Train");
            return new Train(_path, _amountTickets, _amountWagons);
        }

        private void ShowInfo(string element)
        {
            Console.Clear();
            PrintTrainInfo();
            Console.WriteLine(String.Format(MassegePress, element));
            Console.ReadKey();
        }

        private void PrintTrainInfo()
        {
            Console.WriteLine($"Path: {_path.FirstCity} / {_path.SecondCity}");
            Console.WriteLine("Amount tickets: " + _amountTickets);
            Console.WriteLine("Amount wagons: " + _amountWagons );
            Console.WriteLine();
        }
    }

    class Train
    {
        private Path _path;
        private int _amountTickets;
        private int _amountWagons;

        public Train(Path path, int amountTickets, int amountWagons)
        {
            _path = path;
            _amountTickets = amountTickets;
            _amountWagons = amountWagons;
        }
    }

    class CashRegister
    {
        private static readonly Random _random = new Random();

        private int _minTickets;
        private int _maxTickets;

        public CashRegister(int minTickets, int maxTikets)
        {
            _minTickets = minTickets;
            _maxTickets = maxTikets;
        }

        public int CreateTickets()
        {
            int result = _random.Next(_minTickets, _maxTickets);
            Console.WriteLine("Tickets sold: " + result);
            Console.ReadKey();
            return result;
        }
    }

    class Path
    {
        public Path(string firstCity = "Null", string secondCity = "Null")
        {
            FirstCity = firstCity;
            SecondCity = secondCity;
        }

        public string FirstCity { get; private set; }
        public string SecondCity { get;private set; }
    }

    class RouteCreator
    {
        private const string MessagePattern = "Choose {0} city: ";
        private List<string> _namesCities;

        public RouteCreator(List<string> namesCities)
        {
            _namesCities = namesCities;
        }

        public Path CreatePath()
        {
            string firstMessage = String.Format(MessagePattern,"first");
            string secondMessage = String.Format(MessagePattern,"second");
            string firstCity = SelectCity(_namesCities, firstMessage);
            string secondCity = String.Empty;
            Console.Clear();

            do
            {
                secondCity = SelectCity(_namesCities, secondMessage);
                Console.Clear();
            }
            while (secondCity == firstCity);

            Path path = new Path(firstCity, secondCity);
            return path;
        }

        private string SelectCity(List<string> namesCities, string message)
        {
            ShowCities(_namesCities);
            Console.WriteLine(message);
            int numberCity = ReadNumber(0,_namesCities.Count + 1) - 1;
            return namesCities[numberCity];
        }

        private void ShowCities(List<string> namesCities)
        {
            for (int i = 0; i < namesCities.Count; i++)
                Console.WriteLine($"{i + 1}." + namesCities[i]);

            Console.WriteLine();
        }

        private int ReadNumber( int min = int.MinValue, int max = int.MaxValue)
        {
            int result = 0;
            bool isWrok = false;

            while (!isWrok)
            {
                string number = Console.ReadLine();

                while (int.TryParse(number, out result) == false)
                {
                    Console.WriteLine("Input error.Re-enter the number");
                    number = Console.ReadLine();
                }

                if (result <= max && result >= min)
                    isWrok = true;
                else
                    Console.WriteLine("Input error.Number is out for range");
            }

            return result;
        }

    }
}
