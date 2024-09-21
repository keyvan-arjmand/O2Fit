using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutTracker.Data.Repositories
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SelectOption<T>
    {
        public SelectOption()
        {
        }

        public SelectOption(string text, T value)
        {
            this.Text = text;
            this.Value = value;

        }

        /// <summary>
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// </summary>
        public T Value { get; set; }
        public object ExtraData { get; set; }
    }
   
}
