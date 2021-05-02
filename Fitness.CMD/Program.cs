using Fitness.BL.Controller;
using Fitness.BL.Model;
using System;
using System.Globalization;
using System.Resources;

namespace Fitness.CMD
{
    class Program
    {
        static void Main(string[] args)
        {

            var culture = CultureInfo.CreateSpecificCulture("en-us");
            var resourceManager = new ResourceManager("Fitness.CMD.Languages.Messages", typeof(Program).Assembly);


            Console.WriteLine(resourceManager.GetString("Welcome", culture));
            Console.Write(resourceManager.GetString("EnterName", culture));
            var name = Console.ReadLine();

            var userController = new UserController(name);
            var eatingController = new EatingController(userController.CurrentUser);

            if (userController.IsNewUser)
            {
                Console.Write("Введите пол : ");
                var gender = Console.ReadLine();

                DateTime birthDate;
                double weight = ParseDouble("вес");
                double height = ParseDouble("рост");

                birthDate = PareseDateTime();

                userController.SetNewUserData(gender, birthDate, weight, height);
            }

            Console.WriteLine(userController.CurrentUser);


            Console.WriteLine("Что вы хотите сделать?");
            Console.WriteLine("E - ввести прием пищи");

            var key = Console.ReadKey();
            Console.WriteLine();

            if(key.Key == ConsoleKey.E)
            {
                var food = EnterEating();
                eatingController.Add(food.Food, food.weight);

                foreach (var item in eatingController.Eating.Foods)
                {
                    Console.WriteLine($"\t{item.Key.Name} - {item.Value}");
                }

            }
            Console.ReadLine();
        }

        private static (Food Food, double weight) EnterEating()
        {
            Console.Write("Введите имя продукта : ");
            var food = Console.ReadLine();
            var calories = ParseDouble("калорийность");
            var prot = ParseDouble("белки");
            var fats = ParseDouble("жиры");
            var carbs = ParseDouble("углеводы");
            var weight = ParseDouble("вес порции");

            return (new Food(food, prot, fats, carbs, calories), weight);
        }

        private static DateTime PareseDateTime()
        {
            DateTime birthDate;
            while (true)
            {
                Console.Write("Введите дану рождения (dd.MM.yyyy) : ");

                if (DateTime.TryParse(Console.ReadLine(), out birthDate))
                    break;
                else
                    Console.WriteLine("Неверный формат даты.");
            }

            return birthDate;
        }

        private static double ParseDouble(string name)
        {
            while (true)
            {
                Console.Write($"Введите {name} : ");

                if (Double.TryParse(Console.ReadLine(), out double value))
                    return value;
                else
                    Console.WriteLine($"Неверный {name}.");
            }
        }
    }
}
