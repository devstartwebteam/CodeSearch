using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CodeSearch.Helpers
{
    public class ProjectHelpers
    {
        public static byte[] ConvertToByteArray(string str, Encoding encoding)
        {
            return encoding.GetBytes(str);
        }

        public static String ToBinary(Byte[] data)
        {
            return string.Join(" ", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
        }
    }
}