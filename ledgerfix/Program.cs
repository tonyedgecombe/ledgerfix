

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ledgerfix
{
    class Program
    {
        static void Main(string[] args)
        {
            const string pathToLedger = @"C:\Data\ledger_3.1.1_win_bin\ledger.exe";

            var newArgs = args.Select(arg => arg.StartsWith("C:\\") ? Path.GetFileName(arg) : arg)
                              .Select(arg => arg.Contains(" ") ? $"\"{arg}\"" : arg);


            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = pathToLedger,
                    Arguments = string.Join(" ", newArgs),
                    RedirectStandardOutput = true,
                    RedirectStandardError= true,
                    UseShellExecute = false,
                }
            };

            process.Start();

            process.OutputDataReceived += (sender, eventArgs) =>
            {
                Console.Write(eventArgs.Data);
            };

            process.ErrorDataReceived += (sender, eventArgs) =>
            {
                Console.Error.WriteLine(eventArgs.Data);
            };

            process.WaitForExit();
        }
    }
}
