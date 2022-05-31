using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;

namespace Motorsazan.CustomersApp.Shared.Utilities
{
    public static class Tools
    {
        public static string ConvertArabicCharacterToPersian(string word)
        {
            return word.Replace("ی", "ي")
                       .Replace("ک", "ك");
        }

        public static string ConvertDataTableToJson(DataTable table)
        {
            return JsonConvert.SerializeObject(table);
        }

        public static string ConvertToEnglishNumber(string number)
        {
            string[] arabic = new string[10] { "٠", "١", "٢", "٣", "٤", "٥", "٦", "٧", "٨", "٩" };
            string[] persian = new string[10] { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };

            const int loopLength = 10;
            for (int i = 0; i < loopLength; i++)
            {
                string iAsNum = i.ToString();
                number = number.Replace(persian[i], iAsNum)
                               .Replace(arabic[i], iAsNum);
            }

            return number;
        }

        public static DateTime ConvertToLatinDate(string shamsiDate, string format = "yyyy-MM-dd", char splitter = '/')
        {
            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            PersianCalendar pc = new PersianCalendar();
            string[] shamsiList = shamsiDate.Split(splitter);
            int year = Convert.ToInt32(shamsiList[0]);
            int month = Convert.ToInt32(shamsiList[1]);
            int day = Convert.ToInt32(shamsiList[2]);

            return new DateTime(year, month, day, pc);
        }

        public static DateTime ConvertToLatinDateTime(string shamsiDate, string time)
        {
            DateTime date = ConvertToLatinDate(shamsiDate);
            string[] splitTime = time.Split(':');
            int hour = Convert.ToInt32(splitTime[0]);
            int minute = Convert.ToInt32(splitTime[1]);
            date += new TimeSpan(hour, minute, 0);
            return date;
        }

        public static string GetShamsiYear(DateTime date)
        {
            return new PersianCalendar().GetYear(date).ToString();
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                Type type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                _ = table.Columns.Add(prop.Name, type);
            }
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }

            return table;
        }

        public static DataTable ToDataTable<T>(this T item)
        {
            Type type = typeof(T);
            return ToDataTable(item, type);
        }

        public static DataTable ToDataTable<T>(this T item, Type type)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                Type propPropertyType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                _ = table.Columns.Add(prop.Name, propPropertyType);
            }

            DataRow row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
            {
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            }
            table.Rows.Add(row);

            return table;
        }

        public static DataTable ToDataTableWithEnumerableType<T>(this T data, Type type)
        {
            bool isArray = type.IsArray;
            bool isList = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);

            if (isList || isArray)
            {
                type = isArray
                    ? type.GetElementType()
                    : type.GetGenericArguments()[0];
            }

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                Type propertyType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                _ = table.Columns.Add(prop.Name, propertyType);
            }

            if (data == null)
            {
                return table;
            }

            foreach (object item in (IEnumerable)data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }

            return table;
        }

        public static string ToShamsiDateString(this DateTime value, string separator = "/")
        {
            PersianCalendar pc = new PersianCalendar();
            int year = pc.GetYear(value);
            string month = pc.GetMonth(value).ToString("00");
            string day = pc.GetDayOfMonth(value).ToString("00");

            return year + separator + month + separator + day;
        }

        public static T[] PrependGetAllItemToArray<T>(T[] arrayToAppend, T getAllItem)
        {
            T[] newArray = new T[arrayToAppend.Length + 1];
            newArray[0] = getAllItem;
            Array.Copy(arrayToAppend, 0, newArray, 1, arrayToAppend.Length);
            return newArray;
        }
    }
}