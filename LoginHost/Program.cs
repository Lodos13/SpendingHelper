using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;

namespace LoginHost
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9002/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine($"WebAPI SelfHost started at {baseAddress}");
                Console.WriteLine("Press enter to finish");

                Console.ReadLine();
            }
        }
    }
}
