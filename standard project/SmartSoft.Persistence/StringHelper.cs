using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
namespace SmartSoft.Persistence
{
    public class StringHelper
    {
        public static bool IsValidDBInt(object obj)
        {
            try
            {
                if (obj == null)
                    return false;

                string str = obj.ToString();
                if (str == "" || int.Parse(str) == 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidDBDate(object obj)
        {
            try
            {
                if (obj == null)
                    return false;

                DateTime date = DateTime.Parse(obj.ToString());

                if (date == new DateTime())
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidDBFloat(object obj)
        {
            try
            {
                if (obj == null)
                    return false;

                string str = obj.ToString();
                if (str == "" || float.Parse(str) == 0f)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetFormatID(object id)
        {
            return GetFormatID(id, 10);
        }

        public static string GetFormatID(object id, int length)
        {
            return id.ToString().PadLeft(length).Replace(" ", "0");
        }

        public static int GetOriginalID(object id)
        {
            return int.Parse(id.ToString());
        }
    }
}
