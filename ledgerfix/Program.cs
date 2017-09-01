

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ledgerfix
{
    class Program
    {
        static void Main(string[] args)
        {
            const string pathToLedger = @"C:\Data\ledger_3.1.1_win_bin\ledger.exe";

            var newArgs = args.Select(arg => arg.StartsWith("C:\\", StringComparison.InvariantCultureIgnoreCase) ? Path.GetFileName(arg) : arg)
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
                    WorkingDirectory = Directory.GetCurrentDirectory(),
                    StandardOutputEncoding = Encoding.UTF8,
                    StandardErrorEncoding = Encoding.UTF8,
                }
            };

            Console.OutputEncoding = Encoding.ASCII;

            process.OutputDataReceived += (sender, eventArgs) =>
            {
                if (eventArgs?.Data != null)
                {
                    Console.WriteLine(eventArgs.Data);
                    
                }
            };

            process.ErrorDataReceived += (sender, eventArgs) =>
            {
                if (eventArgs.Data != null)
                {
                    Console.Error.WriteLine(eventArgs.Data);
                }
            };

            process.Start();

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            process.WaitForExit();
        }
    }
}
