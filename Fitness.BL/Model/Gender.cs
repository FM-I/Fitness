using System;

namespace Fitness.BL.Model
{

    /// <summary>
    /// Пол.
    /// </summary>
    public class Gender
    {
        /// <summary>
        /// Название пола.
        /// </summary>
        public string GenderName { get; }

        /// <summary>
        /// Создать новый гендер.
        /// </summary>
        /// <param name="genderName">Имя пола.</param>
        public Gender(string genderName)
        {
            if(string.IsNullOrWhiteSpace(genderName))
            {
                throw new ArgumentNullException("Имя поля не может быть пустым или null", nameof(genderName));
            }

            GenderName = genderName;
        }

        public override string ToString()
        {
            return GenderName;
        }

    }
}
