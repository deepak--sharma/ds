using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MySales.BO;
using MySales.BL;
namespace MySales
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            User theUser = new UserBL().GetUserByuserName(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            if (theUser != null)
            {
                Application.Run(new MySales.Login());
                UserBL.MODE = 2;
            }
            else
            {
                UserBL.MODE = 1;
                Application.Run(new Register());
            }
        }
    }
}
