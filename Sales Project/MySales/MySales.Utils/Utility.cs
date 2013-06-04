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
    }
}
