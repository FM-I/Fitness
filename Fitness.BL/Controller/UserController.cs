using Fitness.BL.Model;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Fitness.BL.Controller
{
    /// <summary>
    /// Контролер пользователя.
    /// </summary>
    public class UserController
    {
        /// <summary>
        /// Пользователь.
        /// </summary>
        public User User { get; }

        /// <summary>
        /// Сохранить данные пользователя.
        /// </summary>
        public void Save() 
        {
            var formattor = new BinaryFormatter();

            using (var fs = new FileStream("users.dat", FileMode.OpenOrCreate))
            {
                formattor.Serialize(fs, User);
            }
        }
        /// <summary>
        /// Получть данные пользователя.
        /// </summary>
        /// <returns> Польлзователь. </returns>
        public UserController()
        {
            var formattor = new BinaryFormatter();

            using (var fs = new FileStream("users.dat", FileMode.OpenOrCreate))
            {
                if(formattor.Deserialize(fs) is User user)
                {
                    User = user;
                }
            }

        }

        /// <summary>
        /// Создание контролера пользователя.
        /// </summary>
        /// <param name="userName"> Пользователь. </param>
        public UserController(string userName, string genderName, DateTime birthDate, double weight, double height)
        {
            var gender = new Gender(genderName);
            User = new User(userName, gender, birthDate, weight, height);
        }
    }
}
