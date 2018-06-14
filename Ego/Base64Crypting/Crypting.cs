using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base64Crypting
{
    public class Base64Crypting
    {

        public static byte[] StringtoByteArray(string plainText)//koduje do base64 i wydala byte[]
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            var temp = Convert.ToBase64String(plainTextBytes).Replace("\0","");
            return System.Text.Encoding.UTF8.GetBytes(temp);
        }

        public static string ByteArrayToStrign(byte[] source)//bierze byte[] z base64 i wydala string
        {
            string base64EncodedData = System.Text.Encoding.UTF8.GetString(source).Replace("\0","");//);
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }

}
