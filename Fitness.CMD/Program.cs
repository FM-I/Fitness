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
            var exercisesController = new ExerciseController(userController.CurrentUser);


            if (userController.IsNewUser)
            {
                Console.Write("Введите пол : ");
                var gender = Console.ReadLine();

                DateTime birthDate;
                double weight = ParseDouble("вес");
                double height = ParseDouble("рост");

                birthDate = PareseDateTime("дану рождения (dd.MM.yyyy)");

                userController.SetNewUserData(gender, birthDate, weight, height);
            }

            Console.WriteLine(userController.CurrentUser);

            while (true)
            {

                Console.WriteLine("Что вы хотите сделать?");
                Console.WriteLine("E - ввести прием пищи");
                Console.WriteLine("A - ввести упражнение");
                Console.WriteLine("Q - выход");

                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.E:
                        var food = EnterEating();
                        eatingController.Add(food.Food, food.weight);

                        foreach (var item in eatingController.Eating.Foods)
                        {
                            Console.WriteLine($"\t{item.Key.Name} - {item.Value}");
                        }
                        break;
                    case ConsoleKey.A:
                        var exe = EnterExercise();

                        exercisesController.Add(exe.Activity, exe.Begin, exe.End);

                        foreach (var item in exercisesController.Exercises)
                        {
                            Console.WriteLine($"{item.Activity} c {item.Start.ToShortTimeString()} до {item.End.ToShortTimeString()}");
                        }


                        break;
                    case ConsoleKey.Q:
                        Environment.Exit(0);
                        break;
                }

            }
        }

        private static (DateTime Begin, DateTime End, Activity Activity) EnterExercise()
        {
            Console.Write("Введите название упражнения: ");
            var exerciseName = Console.ReadLine();

            var energy = ParseDouble("расход енергии в минуту");

            var begin = PareseDateTime("начало упражнения");
            var end = PareseDateTime("конец упражнения");

            var activity = new Activity(exerciseName, energy);

            return (begin, end, activity);
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

        private static DateTime PareseDateTime(string name)
        {
            DateTime birthDate;
            while (true)
            {
                Console.Write($"Введите {name} : ");

                if (DateTime.TryParse(Console.ReadLine(), out birthDate))
                    break;
                else
                    Console.WriteLine("Неверный формат.");
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
