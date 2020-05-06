using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

namespace CoDWrangler
{
    class Program
    {
        static void Main(string[] args)
        {
            string argGrab = null;
            Dictionary<string, string> argValues = new Dictionary<string, string>
            {
                { "-t", "10000" }
            };
            foreach (string arg in args)
            {
                if (null != argGrab)
                {
                    Console.WriteLine($"{argGrab} {arg}");
                    argValues[argGrab] = arg;
                    argGrab = null;
                    continue;
                }
                if (argValues.Keys.Contains(arg))
                {
                    argGrab = arg;
                    continue;
                }
            }
            int interval = int.Parse(argValues["-t"]);

            Console.WriteLine($"Interval: {interval.ToString()}");

            for (; ; )
            {
                Process[] pname = Process.GetProcessesByName("ModernWarfare");
                if (pname.Length == 0)
                {
                    Console.WriteLine("ModernWarfare not running");
                }
                else
                {
                    Console.WriteLine($"{pname.Count()} ModernWarfares running, checking priority class");
                    foreach (Process process in Process.GetProcessesByName("ModernWarfare"))
                    {
                        Console.WriteLine($"ModernWarfare PID {process.Id} has priority class {process.PriorityClass.ToString()}");
                        if (process.PriorityClass != ProcessPriorityClass.Normal)
                        {
                            Console.WriteLine($"Setting PID {process.Id} priority Normal");
                            process.PriorityClass = ProcessPriorityClass.Normal;
                            Console.WriteLine($"PID {process.Id} now has priority {process.PriorityClass.ToString()}");
                        }
                    }
                }
                System.Threading.Thread.Sleep(interval);
            }
        }
    }
}
