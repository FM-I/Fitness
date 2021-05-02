using System;

namespace Fitness.BL.Model
{
    [Serializable]
    public class Activity
    {
        public string Name { get; }
        public double CaloriesPerMinutes { get; }

        public Activity(string name, double CaloriesPerMinutes)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            this.CaloriesPerMinutes = CaloriesPerMinutes;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
