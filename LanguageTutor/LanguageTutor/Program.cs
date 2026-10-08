using System;
using System.Windows.Forms;
using LanguageTutor.Forms;   // so MainForm resolves

namespace LanguageTutor
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());   //  starts MainForm
        }
    }
}
