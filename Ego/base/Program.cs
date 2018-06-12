using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace @base
{
    class Program
    {
        static void Main(string[] args)
        {
            string plainText = "ąćżźóść!!2223";//string który chcemy zakodować
            Console.WriteLine(plainText);
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);//zamienia na byte
            var base64EncodedData = System.Convert.ToBase64String(plainTextBytes);//funkcja koduje podany ciąg i przechowuje w zmiennej
            Console.WriteLine(base64EncodedData);

            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);//funkcja odkodowywuje ciąg i zapisuje w zmiennej
            Console.WriteLine(System.Text.Encoding.UTF8.GetString(base64EncodedBytes));//zamienia z byte na string i wyświetla
        }
    }
}
