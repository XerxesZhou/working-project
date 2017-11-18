using System;

namespace SmartSoft.Component
{
    public class DataTypeConverter
    {
        public static object ChangeType(Type type, object value)
        {
            if (((value == null) || (value.ToString() == "")) && type.IsGenericType)
            {
                return Activator.CreateInstance(type);
            }

            if (value == null)
            {
                return null;
            }

            if ((value.ToString() == string.Empty) && type == typeof(int))
            {
                return Activator.CreateInstance(type);
            }

            if ((value.ToString() == string.Empty) && type == typeof(System.Decimal))
            {
                return Activator.CreateInstance(type);
            }

            if (type == value.GetType())
            {
                return value;
            }
            if (type.IsEnum)
            {
                if (value is string)
                {
                    return Enum.Parse(type, value as string);
                }
                return Enum.ToObject(type, value);
            }
            if (!type.IsInterface && type.IsGenericType)
            {
                Type type1 = type.GetGenericArguments()[0];
                object obj1 = DataTypeConverter.ChangeType(type1, value);
                return Activator.CreateInstance(type, new object[] { obj1 });
            }
            if ((value is string) && (type == typeof(Guid)))
            {
                return new Guid(value as string);
            }
            if ((value is string) && (type == typeof(Version)))
            {
                return new Version(value as string);
            }
            if (!(value is IConvertible))
            {
                return value;
            }
            return Convert.ChangeType(value, type);
        }
    }
}
