using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using Microsoft.Owin.Host.HttpListener;

namespace MockEC2InstanceMetaData
{
    class Program
    {
        private static string _port = ConfigurationManager.AppSettings["Port"] ?? "9000";
        static void Main(string[] args)
        {
            using (Microsoft.Owin.Hosting.WebApp.Start<Startup1>("http://127.0.0.1:"+_port))
            {
                Console.WriteLine("Press [enter] to quit...");
                Console.ReadLine();
            }
        }
    }
}