using System;
using System.Text;

namespace Utils
{
    public static class CSDateTimeHelper
    {
        static public String ConvertDateToString(DateTime date)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(date.Day).Append("-").Append(date.Month).Append("-").Append(date.Year);
            return builder.ToString();
        }

        static public DateTime ConvertStringToDate(String stringDate)
        {
            String[] strArray = stringDate.Split('-');
            Int32[] intArray = new Int32[3];

            for (Int32 i = 0; i < strArray.Length; i++)
                intArray[i] = Int32.Parse(strArray[i]);

            DateTime date = new DateTime(intArray[2], intArray[1], intArray[0]);
            return date;
        }
    }
}
