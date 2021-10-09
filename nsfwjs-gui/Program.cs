using System;
using System.Windows.Forms;

namespace nsfwjs_gui
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new frmMain());
            Application.Exit(); 
        }
    }
}
