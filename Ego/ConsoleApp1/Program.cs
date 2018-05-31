using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress addr = ipEntry.AddressList[1];


            Console.WriteLine($"IP Address {addr} ") ;
            Console.ReadLine();
        }
    }
}
