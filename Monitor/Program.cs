using CommandLine;
using System;
using System.Diagnostics;
using System.Threading;

namespace Monitor
{
    public class MainOptions
    {
        [Option(shortName: 'n', longName: "name", Required = true, HelpText = "Process name to monitor (provide without extension)")]
        public string process_name { get; set; }

        [Option(shortName: 'l', longName: "lifespan", Required = true, HelpText = "How much time monitored process allowed to live (in minutes)")]
        public double lifespan { get; set; }

        [Option(shortName: 'f', longName: "frequency", Required = true, HelpText = "How frequent monitored process is checked (in minutes)")]
        public double frequency { get; set; }

    }
    class Program
    {
        static void Main(string[] args)
        {
            string name;
            double lifespan, frequency;
            CommandLine.Parser.Default.ParseArguments<MainOptions>(args)
            .WithParsed<MainOptions>(o =>
            {
                name = o.process_name;
                lifespan = o.lifespan;
                frequency = o.frequency;

                Monitor my_monitor = new(name, lifespan, frequency);
                my_monitor.Watch();

            });

        }
    }
}
