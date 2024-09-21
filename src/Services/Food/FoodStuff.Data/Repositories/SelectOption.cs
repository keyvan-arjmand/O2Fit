using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
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

        public SelectOption(string text, T value,string _id)
        {
            this.Text = text;
            this.Value = value;
            this._id =_id;
        }

        /// <summary>
        /// </summary>
        public string Text { get; set; }
        public string _id { get; set; }

        /// <summary>
        /// </summary>
        public T Value { get; set; }
        public object ExtraData { get; set; }
    }
   
}
