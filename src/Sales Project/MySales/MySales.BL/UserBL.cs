using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.BO;
using MySales.DL;
namespace MySales.BL
{
    public class UserBl
    {
        public static long UserId;
        /*
            MODE Values
         *  1. ADD
         *  2. UPDATE
         */
        public static int Mode = 1;
        public bool IsUserValid(string userName, string password, out long userId)
        {
            var isUserValid = false;
            userId = 1;
            var theUser = new UserDl().GetUserByUsername(userName);
            if (theUser != null)
            {
                userId = theUser.UserId;
                if (password.Equals(UtilityClass.Decrypt(theUser.Password)))
                { isUserValid = true; }
            }
            return isUserValid;
        }


        public User GetUserByuserName(string un)
        {
            User theUser = null;
            theUser = new UserDl().GetUserByUsername(un);
            return theUser;
        }

        public string CreateUser(User theUser)
        {
            var status = "START";
            try
            {
                var code = (new UserDl()).CreateUser(theUser);
                status = code < 1 ? "ERROR" : "SUCCESS";
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return status;
        }

        public string ChangePassword(User theUser)
        {
            var status = "START";
            try
            {
                theUser.UserId = UserId;
                var code = (new UserDl()).UpdateUser(theUser);
                status = code < 1 ? "ERROR" : "SUCCESS";
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return status;
        }


    }
}
