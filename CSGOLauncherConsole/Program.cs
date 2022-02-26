using NvAPIWrapper.Display;
using System;
using System.Diagnostics;

namespace CSGOLauncherConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lets get this rankup!!");

            //Get every display connected to Nvidia GPU
            Display[] displays = Display.GetDisplays();

            //Change each display's Digital Vibrance to maximum value
            foreach (Display display in displays)
            {
                var disp = new DVCInformation(display.Handle);
                disp.CurrentLevel = 100;
            }

            //Start CSGO
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "steam://rungameid/730",
                UseShellExecute = true
            };
            Process.Start(psi);

            //Set windows power plan to Best Performance
            Process.Start("powercfg", "-setactive 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c");

            Console.WriteLine("Digital vibrance set to 100");
            Console.WriteLine("Power Plan set to Best Performance");
            Console.WriteLine("Type 'stop' to revert back to default settings");

            string command = Console.ReadLine();
            while (command != "stop")
            {
                Console.WriteLine($"Invalid command {command}");
                command = Console.ReadLine();
            }

            Console.WriteLine("reverting back to normal settings");
            Console.WriteLine("Digital vibrance set to 50");
            Console.WriteLine("Power Plan set to Balanced");

            //Digital Vibrance back to default value
            foreach (Display display in displays)
            {
                var disp = new DVCInformation(display.Handle);
                disp.CurrentLevel = 50;
            }

            //Power plan back to default ("balanced")
            Process.Start("powercfg", "-setactive 381b4222-f694-41f0-9685-ff5bb260df2e");
            Environment.Exit(0);
        }
    }
}
