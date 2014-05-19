#region Usings

using System;
using System.ComponentModel;

#endregion

namespace PuppyFramework.Helpers
{
    //source: http://stackoverflow.com/questions/8625/generic-type-conversion-from-string
    public static class TConverter
    {
        #region Methods

        public static T ChangeType<T>(object value)
        {
            return (T)ChangeType(typeof(T), value);
        }

        public static object ChangeType(Type t, object value)
        {
            var tc = TypeDescriptor.GetConverter(t);
            return tc.ConvertFrom(value);
        }

        public static void RegisterTypeConverter<T, TC>() where TC : TypeConverter
        {
            TypeDescriptor.AddAttributes(typeof(T), new TypeConverterAttribute(typeof(TC)));
        }

        #endregion
    }
}
