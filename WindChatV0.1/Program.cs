using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindChat
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new Session());
            //Application.Run(new WindChat.Session());

            LoginForm lf = new LoginForm();
            lf.ShowDialog();
            if (lf.DialogResult == DialogResult.OK)
            {
                lf.Close();
                MainForm mf = new MainForm();
                Application.Run(mf);
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
