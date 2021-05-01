using Fitness.BL.Controller;
using Fitness.BL.Model;
using System;

namespace Fitness.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Fitness");

            Console.Write("Введите имя пользователя : ");
            var name = Console.ReadLine();

            var userController = new UserController(name);

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
            Console.ReadLine();
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
