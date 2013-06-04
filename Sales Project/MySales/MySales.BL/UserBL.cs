using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.BO;
using MySales.DL;
namespace MySales.BL
{
    public class UserBL
    {
        public static long userID;
        /*
            MODE Values
         *  1. ADD
         *  2. UPDATE
         */
        public static int MODE = 1;
        public bool IsUserValid(string userName, string password, out long userID)
        {
            bool _isUserValid = false;
            userID = 1;
            User theUser = new UserDL().GetUserByUsername(userName);
            if (theUser != null)
            {
                userID = theUser.UserID;
                if (password.Equals(UtilityClass.Decrypt(theUser.Password)))
                { _isUserValid = true; }
            }
            return _isUserValid;
        }


        public User GetUserByuserName(string un)
        {
            User theUser = null;
            theUser = new UserDL().GetUserByUsername(un);
            return theUser;
        }

        public string CreateUser(User theUser)
        {
            string status = "START";
            try
            {
                int code = (new UserDL()).CreateUser(theUser);
                if (code < 1)
                {
                    status = "ERROR";
                }
                else
                {
                    status = "SUCCESS";
                }
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return status;
        }

        public string ChangePassword(User theUser)
        {
            string status = "START";
            try
            {
                theUser.UserID = userID;
                int code = (new UserDL()).UpdateUser(theUser);
                if (code < 1)
                {
                    status = "ERROR";
                }
                else
                {
                    status = "SUCCESS";
                }
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return status;
        }


    }
}
