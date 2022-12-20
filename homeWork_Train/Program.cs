using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homeWork_Train
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Train train = new Train();
            train.Work();
        }
    }

    class Train
    {
        private int _amountWagons;
        private string _path;

        public Train()
        {

        }

        public void Work() 
        {
            const string CommandCreatTrain = "1";
            const string CommandExit = "2";

            bool isExit = false;

            while (isExit == false) 
            {
                Console.WriteLine($"Нажмите\n" +
                    $"{CommandCreatTrain} - создание поезда\n" +
                    $"{CommandExit} - выход из программы\n");
                string userInput = Console.ReadLine();

                switch (userInput) 
                {
                    case CommandCreatTrain:
                        CreatTrain();
                        break;

                    case CommandExit:

                        break;

                    default:
                        isExit = true;
                        break;

                }
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void CreatTrain() 
        {
            CreatPath();
            CreatWagons(CreatPeople());
            Console.WriteLine($"Поезд - {_path} отправился в путь");
        }

        private int CreatPeople() 
        {
            Random random = new Random();
            int minAmounrPeople = 20;
            int maxAmounrPeople = 200;
            int amountPeople = random.Next(minAmounrPeople, maxAmounrPeople + 1);
            Console.WriteLine($"На напровление {_path} было приобритено {amountPeople} билетов \n");
            return amountPeople;
        }

        private void CreatWagons(int amountPeople)
        {
            List<Wagon> wagons = new List<Wagon>()
            {
                new Wagon("Купе Vip",20),
                new Wagon("Купе",30),
                new Wagon("Сидячий",40),
                new Wagon("Двухэтажный сидячий",60)
            };
            Console.WriteLine("Какой тип вагона вы хотите выбрать?\n");

            for (int i = 0; i < wagons.Count; i++) 
            {
                Console.Write($"{ i + 1}.");
                wagons[i].ShowInfo();
            }

            Wagon wagon = wagons[ReadNumber() - 1 ];

            if (amountPeople % wagon.GetPeopleCapacity() == 0)
            {
                _amountWagons = amountPeople / wagon.GetPeopleCapacity();
            }
            else 
            {
                _amountWagons = (amountPeople / wagon.GetPeopleCapacity()) + 1;
            }

            Console.WriteLine($"Было создан(-о) {_amountWagons} выгон(-а/-ов)");
            Console.ReadKey();
            Console.Clear();
        }

        private void CreatPath()
        {
            List<string> nameOfCities = new List<string>() { "Екатеринбург", "Москва", "Санкт-Петербург", "Омск", "Челябинск", "Новосибирск" };

            Console.WriteLine("Выберите город из которого должен выезжать поезд");
            ShowList(nameOfCities);
            _path = nameOfCities[ReadNumber() - 1];
            nameOfCities.Remove(_path);
            Console.WriteLine("Выберите гороод в который приехать поезд");
            ShowList(nameOfCities);
            _path += " - " + nameOfCities[ReadNumber() - 1];
            Console.Clear();
        }

        private void ShowList(List<string> array)
        {
            for (int i = 0; i < array.Count; i++)
            {
                Console.WriteLine($"{i + 1}.{array[i]}");
            }
        }

        private int ReadNumber()
        {
            int result;
            string number = Console.ReadLine();

            while (int.TryParse(number, out result) == false)
            {
                Console.WriteLine("Ошибка ввода.Введите число повторно");
                number = Console.ReadLine();
            }

            return result;
        }
    }

    class Wagon
    {
        private string _name;
        private int _peopleCapacity;

        public Wagon(string name, int peopleCapacity) 
        {
            _name = name;
            _peopleCapacity = peopleCapacity;
        }

        public void ShowInfo() 
        {
            Console.WriteLine($"Наименование вагона - {_name}\nЕго вместимость - {_peopleCapacity}\n");
        }

        public int GetPeopleCapacity() 
        {
            return _peopleCapacity;
        }
    }
}
