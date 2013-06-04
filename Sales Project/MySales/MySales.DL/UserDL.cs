using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.BO;
using System.Data;
using System.Data.OleDb;
using MySales.Utils;

namespace MySales.DL
{
    public class UserDL
    {

        #region Constants
        private const string SELECT_USER_QUERY = "select * from [User Account] where Username=@un";
        private const string SELECT_MAX_USER = "Select MAX(ID) FROM [User Account]";
        private const string INSERT_USER_QUERY = "Insert into [User Account] ([ID],[Username],[Password]) Values (@ID,@un,@pwd)";
        private const string UPDATE_USER_QUERY = "UPDATE [User Account] SET  [User Account].Password = @pwd WHERE  [User Account].ID=@id"; 
        #endregion
        public User GetUserByUsername(string un)
        {
            //bool _isUserValid = false;            
            User theUser = null;
            
            using (OleDbConnection con = DBManager.GetConnection())
            {
                con.Open();
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = SELECT_USER_QUERY;
                    cmd.Connection = con;
                    cmd.Parameters.Add(new OleDbParameter("@un", un));
                    //using (OleDbDataReader drUsers = new OleDbDataReader(cmd))
                    OleDbDataReader drUsers = cmd.ExecuteReader();
                    if (drUsers.HasRows)
                    {
                        theUser = new User();
                        while (drUsers.Read())
                        {
                            long userID = -1;
                            long.TryParse(drUsers["ID"].ToString().Trim(), out userID);                            
                            theUser.UserID = userID;
                            theUser.Username = drUsers["Username"].ToString().Trim();
                            theUser.Password = drUsers["Password"].ToString().Trim();                            
                        }
                    }

                    //_isUserValid = ds.Tables[0].Rows[0]["Password"].ToString().Equals(this.txtPassword.Text);

                }
            }


            return theUser;
        }
                
        public int CreateUser(User theUser)
        {
            int statusCode = -1;
            try
            {
                theUser.UserID = GetNextUserID();
                using (OleDbConnection con = DBManager.GetConnection())
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(INSERT_USER_QUERY, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@id", theUser.UserID));
                        cmd.Parameters.Add(new OleDbParameter("@un", theUser.Username));
                        cmd.Parameters.Add(new OleDbParameter("@pwd", theUser.Password));                        
                        statusCode = cmd.ExecuteNonQuery();
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return statusCode;

        }

        public int UpdateUser(User theUser)
        {
            int statusCode = -1;
            try
            {                
                using (OleDbConnection con = DBManager.GetConnection())
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(UPDATE_USER_QUERY, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@id", theUser.UserID));
                        cmd.Parameters.Add(new OleDbParameter("@pwd", theUser.Password));
                        statusCode = cmd.ExecuteNonQuery();
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return statusCode;

        }

        private long GetNextUserID()
        {
            long _productID = 1;
            try
            {
                using (OleDbConnection con = DBManager.GetConnection())
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(SELECT_MAX_USER, con))
                    {
                        Object statusCode = cmd.ExecuteScalar();
                        if (statusCode != DBNull.Value)
                        {
                            //_productID = (long)statusCode + 1;
                            long.TryParse(statusCode.ToString(), out _productID);
                            _productID += 1;
                        }
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _productID;
        }
    }
}
