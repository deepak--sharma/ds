using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySales.Utils
{
    public static class Utility
    {
        public enum PayrollStatus
        {
            INPROCESS,
            CALCULATED,
            VERIFIED,
            REJECTED,
            APPROVED
        }

        public enum ActionStatus
        {
            SUCCESS,
            FAILURE
        }

        public static string EncryptString(string strPlainText)
        {
            string strEncrypted = string.Empty;
            strEncrypted = EnCryptDecrypt.CryptorEngine.Encrypt(strPlainText, false);
            return strEncrypted;
        }
        public static string DecryptString(string strEncText)
        {
            string strPlainText = string.Empty;
            strPlainText = EnCryptDecrypt.CryptorEngine.Decrypt(strEncText, false);
            return strPlainText;
        }
        public static string MonthNameFromInt(int month)
        {
            switch (month)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
                default:
                    return string.Empty;
            }
        }
        public static int MonthFromName(string month)
        {
            switch (month.ToUpper())
            {
                case "JANUARY":
                    return 1;
                case "FEBRUARY":
                    return 2;
                case "MARCH":
                    return 3;
                case "APRIL":
                    return 4;
                case "MAY":
                    return 5;
                case "JUNE":
                    return 6;
                case "JULY":
                    return 7;
                case "AUGUST":
                    return 8;
                case "SEPTEMBER":
                    return 9;
                case "OCTOBER":
                    return 10;
                case "NOVEMBER":
                    return 11;
                case "DECEMBER":
                    return 12;
                default:
                    return 0;
            }
        }
    }
}
