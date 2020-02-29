using System;

namespace Lib
{
    /// <summary>
    /// Can be used to store Name/Value (or Key/Value) pairs.
    /// </summary>
    [Serializable]
    public class NameValue : NameValue<string>
    {
        /// <summary>
        /// Creates a new <see cref="NameValue"/>.
        /// </summary>
        public NameValue()
        {

        }

        /// <summary>
        /// Creates a new <see cref="NameValue"/>.
        /// </summary>
        public NameValue(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }

    /// <summary>
    /// Can be used to store Name/Value (or Key/Value) pairs.
    /// </summary>
    [Serializable]
    public class NameValue<T>
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Creates a new <see cref="NameValue"/>.
        /// </summary>
        public NameValue()
        {

        }

        /// <summary>
        /// Creates a new <see cref="NameValue"/>.
        /// </summary>
        public NameValue(string name, T value)
        {
            Name = name;
            Value = value;
        }
    }



    /// <summary>
    /// Can be used to store Name/Value (or Key/Value) pairs.
    /// </summary>
    [Serializable]
    public class NameValueID : NameValueID<string,int>
    {
        /// <summary>
        /// Creates a new <see cref="NameValue"/>.
        /// </summary>
        public NameValueID()
        {

        }

        /// <summary>
        /// Creates a new <see cref="NameValue"/>.
        /// </summary>
        public NameValueID(string name, string value,int id)
        {
            Name = name;
            Value = value;
            ID = id;
        }
    }


    /// <summary>
    /// Can be used to store Name/Value (or Key/Value) pairs.
    /// </summary>
    [Serializable]
    public class NameValueID<T,M>
    {
        /// <summary>
        /// ID
        /// </summary>
        public M ID { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Creates a new <see cref="NameValue"/>.
        /// </summary>
        public NameValueID()
        {

        }

        /// <summary>
        /// Creates a new <see cref="NameValue"/>.
        /// </summary>
        public NameValueID(string name, T value,M id)
        {
            Name = name;
            Value = value;
            ID = id;
        }
    }

}
