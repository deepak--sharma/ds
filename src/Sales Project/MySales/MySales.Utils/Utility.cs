using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MySales.Utils
{
    public static class Utility
    {
        public enum PayrollStatus
        {
            TOBECALCULATED,
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

        /// <summary>
        /// This function sets month and year value to a value prior to current month.
        /// </summary>
        /// <param name="cbMonth">Object of month combobox</param>
        /// <param name="cbYear">Object of year combobox</param>
        /// <param name="viewPayroll">Boolean variable to flag function invoked from view payroll page </param>
        public static void SetPayrollMonthYearDropdownList(ComboBox cbMonth, ComboBox cbYear, bool viewPayroll = false)
        {
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var yearCount = 0;
            if (month == 1)
            {
                yearCount = year - 1;
            }
            else if (viewPayroll)
            {
                yearCount = year - 5;
            }
            else
            {
                yearCount = year;
            }
            for (var i = yearCount; i <= year; ++i)
            {
                cbYear.Items.Add(i);
            }
            if (month == 1)
            {
                cbMonth.SelectedIndex = 11;
                cbYear.SelectedIndex = cbYear.Items.Count - 2;
            }
            else
            {
                cbMonth.SelectedIndex = month - 2;
                cbYear.SelectedIndex = cbYear.Items.Count - 1;
            }
            
        }

        public static void dgvEmp_CellFormatting(DataGridView dgvEmp, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvEmp.Rows[e.RowIndex].DataBoundItem != null &&
                dgvEmp.Columns[e.ColumnIndex].DataPropertyName.Contains("."))
            {
                e.Value = e.Value ?? BindProperty(dgvEmp.Rows[e.RowIndex].DataBoundItem, dgvEmp.Columns[e.ColumnIndex].DataPropertyName);
            }

        }
        private static string BindProperty(object property, string propertyName)
        {
            string retValue = "";
            propertyName = propertyName.ToCamelCase();
            if (propertyName.Contains("."))
            {
                string leftPropertyName;
                leftPropertyName = propertyName.Substring(0, propertyName.IndexOf("."));
                var arrayProperties = property.GetType().GetProperties();

                foreach (var propertyInfo in arrayProperties.Where(propertyInfo => propertyInfo.Name == leftPropertyName))
                {
                    retValue = BindProperty(
                        propertyInfo.GetValue(property, null),
                        propertyName.Substring(propertyName.IndexOf(".") + 1));
                    break;
                }
            }
            else
            {
                var propertyType = property.GetType();
                var propertyInfo = propertyType.GetProperty(propertyName);
                retValue = propertyInfo.GetValue(property, null).ToString();
            }
            return retValue;
        }
        //string HTML = GetMyTable(people, x => x.FirstName, x => x.LastName);
        public static string List2HtmlTable<T>(IEnumerable<T> list, params Func<T, object>[] columns)
        {
            var sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.Append("<tr>");
                foreach (var column in columns)
                {
                    sb.Append("<td>");
                    sb.Append(column(item));
                    sb.Append("</td>");
                }
                sb.Append("</tr>");
            }
            return sb.ToString();
        }
    }
}
