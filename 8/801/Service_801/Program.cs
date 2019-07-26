﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Service_801
{
    class Program
    {
        static void Main(string[] args)
        {

            using (ServiceHost host = new ServiceHost(typeof(CalculatorService)))
            {
                host.Opened += delegate
                {
                    Console.WriteLine("CalculaorService have started, press any key to stop it!");
                };

                host.Open();
                Console.Read();
            }
        }
    }
}
