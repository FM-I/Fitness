using Fitness.BL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Fitness.BL.Controller
{
    /// <summary>
    /// Контролер пользователя.
    /// </summary>
    public class UserController : ControllerBase
    {

        private const string USERS_FILE_NAME = "users.dat";

        /// <summary>
        /// Пользователь.
        /// </summary>
        public List<User> Users { get; }

        public User CurrentUser { get; }

        public bool IsNewUser { get; } = false;

        /// <summary>
        /// Сохранить данные пользователя.
        /// </summary>
        public void Save() 
        {
            Save(USERS_FILE_NAME, Users);
        }
        /// <summary>
        /// Получть список пользователей.
        /// </summary>
        /// <returns> Польлзователь. </returns>
        private List<User> GetUsersData()
        {
            return Load<List<User>>(USERS_FILE_NAME) ?? new List<User>();
        } 

        /// <summary>
        /// Создание контролера пользователя.
        /// </summary>
        /// <param name="userName"> Пользователь. </param>
        public UserController(string userName)
        {

            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException("Имя пользователя не может быть пустым.",nameof(userName));

            Users = GetUsersData();

            CurrentUser = Users.SingleOrDefault(u => u.Name == userName);

            if(CurrentUser == null)
            {
                CurrentUser = new User(userName);
                Users.Add(CurrentUser);
                IsNewUser = true;
                Save();
            }
        }

        public void SetNewUserData(string genderName, DateTime birthDate, double weight = 1, double height = 1)
        {
            //TODO: Проверка входных данных

            CurrentUser.Gender = new Gender(genderName);
            CurrentUser.BirthDate = birthDate;
            CurrentUser.Weight = weight;
            CurrentUser.Height = height;
            Save();
        }
    }
}
