using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoAmong
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string hash = "";
            if (args.Length > 0) {
                if (Uri.TryCreate(args[0], UriKind.Absolute, out var uri) &&
                                string.Equals(uri.Scheme, UriScheme, StringComparison.OrdinalIgnoreCase)) {
                    hash = uri.AbsoluteUri.Split(new string[] { "autoamong://sync/" }, StringSplitOptions.None)[1].Trim();
                }
            } else
                RegisterUriScheme();
            CloseIfOpened();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(hash));
        }

        const string UriScheme = "autoamong";
        const string FriendlyName = "AutoAmong Hash Link";

        private static void CloseIfOpened() {
            int nProcessID = Process.GetCurrentProcess().Id;
            string nProcessName = Process.GetCurrentProcess().ProcessName;
            foreach (var process in Process.GetProcessesByName(nProcessName))
                if (process.Id!= nProcessID)
                    process.Kill();
        }

        private static void RegisterUriScheme()
        {
            Console.WriteLine("Registering Scheme...");
            using (var key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Classes\\" + UriScheme))
            {
                string applicationLocation = typeof(Program).Assembly.Location;

                key.SetValue("", "URL:" + FriendlyName);
                key.SetValue("URL Protocol", "");

                using (var defaultIcon = key.CreateSubKey("DefaultIcon"))
                {
                    defaultIcon.SetValue("", applicationLocation + ",1");
                }

                using (var commandKey = key.CreateSubKey(@"shell\open\command"))
                {
                    commandKey.SetValue("", "\"" + applicationLocation + "\" \"%1\"");
                }
            }
        }
    }
}
