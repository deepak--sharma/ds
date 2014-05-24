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
    public class UserDl
    {

        #region Constants
        private const string SelectUserQuery = "select * from [User Account] where Username=@un";
        private const string SelectMaxUser = "Select MAX(ID) FROM [User Account]";
        private const string InsertUserQuery = "Insert into [User Account] ([ID],[Username],[Password]) Values (@ID,@un,@pwd)";
        private const string UpdateUserQuery = "UPDATE [User Account] SET  [User Account].Password = @pwd WHERE  [User Account].ID=@id"; 
        #endregion
        public User GetUserByUsername(string un)
        {
            //bool _isUserValid = false;            
            User theUser = null;
            
            using (var con = DbManager.GetConnection())
            {
                con.Open();
                using (var cmd = new OleDbCommand())
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = SelectUserQuery;
                    cmd.Connection = con;
                    cmd.Parameters.Add(new OleDbParameter("@un", un));
                    //using (OleDbDataReader drUsers = new OleDbDataReader(cmd))
                    var drUsers = cmd.ExecuteReader();
                    if (drUsers != null && drUsers.HasRows)
                    {
                        theUser = new User();
                        while (drUsers.Read())
                        {
                            long userId = -1;
                            long.TryParse(drUsers["ID"].ToString().Trim(), out userId);                            
                            theUser.UserId = userId;
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
            var statusCode = -1;
            try
            {
                theUser.UserId = GetNextUserId();
                using (var con = DbManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(InsertUserQuery, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@id", theUser.UserId));
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
            var statusCode = -1;
            try
            {                
                using (var con = DbManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(UpdateUserQuery, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@id", theUser.UserId));
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

        private static long GetNextUserId()
        {
            long productId = 1;
            try
            {
                using (var con = DbManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(SelectMaxUser, con))
                    {
                        var statusCode = cmd.ExecuteScalar();
                        if (statusCode != DBNull.Value)
                        {
                            //_productID = (long)statusCode + 1;
                            long.TryParse(statusCode.ToString(), out productId);
                            productId += 1;
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

            return productId;
        }
    }
}
