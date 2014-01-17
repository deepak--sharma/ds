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
    public class EmployeeDl
    {
        private const string SelectAllEmp = @"Select EMPLOYEE.ID,EmpCode,FirstName,MiddleName,LastName,Gender,DateOfBirth,AddressC,AddressP,
                            DateOfJoining,IsActive,Designation,ModifiedBy,CreateDate,ModifiedDate,Designation.ID As Desig_ID,Designation.Desc As Desig_Desc
                            FROM EMPLOYEE INNER JOIN Designation ON Employee.Designation = Designation.ID";
        private const string AddEmp = @"Insert into Employee (EmpCode,FirstName,MiddleName,LastName,FathersName,Gender,DateOfBirth,AddressC,AddressP,DateOfJoining,IsActive,Designation,ModifiedBy,CreateDate) values
                                         (@EmpCode,@FirstName,@MiddleName,@LastName,@FathersName,@Gender,@DateOfBirth,@AddressC,@AddressP,@DateOfJoining,@IsActive,@Designation,@ModifiedBy,@CreateDate)";
        private const string SelectEmpById = @"Select EMPLOYEE.ID,EmpCode,FirstName,MiddleName,LastName,FathersName,Gender,DateOfBirth,AddressC,AddressP,
                            DateOfJoining,IsActive,Designation,ModifiedBy,CreateDate,ModifiedDate,Designation.ID As Desig_ID,Designation.Desc As Desig_Desc
                            FROM EMPLOYEE INNER JOIN Designation ON Employee.Designation = Designation.ID
                            WHERE EMPLOYEE.ID = @empid";

        public List<Employee> GetAllEmployees()
        {
            var lstEmployee = new List<Employee>();
            try
            {
                using (var con = DBManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(SelectAllEmp, con))
                    {
                        var dr = cmd.ExecuteReader();
                        if (dr != null && dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var theEmployee = new Employee
                                {
                                    Id = null != dr["ID"] && string.Empty != dr["ID"].ToString().Trim() ? long.Parse(dr["ID"].ToString().Trim()) : 0,
                                    EmpCode = null != dr["EmpCode"] ? dr["EmpCode"].ToString() : string.Empty,
                                    FirstName = null != dr["FirstName"] ? dr["FirstName"].ToString() : string.Empty,
                                    MiddleName = null != dr["MiddleName"] ? dr["MiddleName"].ToString() : string.Empty,
                                    LastName = null != dr["LastName"] ? dr["LastName"].ToString() : string.Empty,
                                    Gender = null != dr["Gender"] ? dr["Gender"].ToString() : string.Empty,
                                    DateOfBirth = Convert.ToString(dr["DateOfBirth"]),
                                    AddressC = null != dr["AddressC"] ? dr["AddressC"].ToString() : string.Empty,
                                    AddressP = null != dr["AddressP"] ? dr["AddressP"].ToString() : string.Empty,
                                    DateOfJoining = Convert.ToString(dr["DateOfJoining"]),
                                    IsActive = Convert.ToBoolean(dr["IsActive"]),
                                    Designation = new Designation
                                    {
                                        ID = string.IsNullOrEmpty(Convert.ToString(dr["Desig_ID"])) ? long.Parse(dr["Desig_ID"].ToString().Trim()) : 0,
                                        Desc = null != dr["Desig_Desc"] ? Convert.ToString(dr["Desig_Desc"]) : string.Empty
                                    },
                                    FullName = GetFullName(Convert.ToString(dr["FirstName"]), Convert.ToString(dr["MiddleName"]), Convert.ToString(dr["LastName"]))

                                };
                                lstEmployee.Add(theEmployee);
                            }
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
            return lstEmployee;
        }


        public int AddEmployee(Employee employee, Int64 userId)
        {
            try
            {
                using (var con = DBManager.GetConnection())
                {
                    con.Open();
                    using (var command = new OleDbCommand(AddEmp, con))
                    {
                        command.Parameters.Add(new OleDbParameter { ParameterName = "@EmpCode", Value = employee.EmpCode });
                        command.Parameters.Add(new OleDbParameter { ParameterName = "@FirstName", Value = employee.FirstName });
                        command.Parameters.Add(new OleDbParameter { ParameterName = "@MiddleName", Value = employee.MiddleName });
                        command.Parameters.Add(new OleDbParameter { ParameterName = "@LastName", Value = employee.LastName });
                        command.Parameters.Add(new OleDbParameter { ParameterName = "@FathersName", Value = employee.FathersName });
                        command.Parameters.Add(new OleDbParameter { ParameterName = "@Gender", Value = employee.Gender });
                        command.Parameters.Add(new OleDbParameter { ParameterName = "@DateOfBirth", Value = employee.DateOfBirth });
                        command.Parameters.Add(new OleDbParameter { ParameterName = "@AddressC", Value = employee.AddressC });
                        command.Parameters.Add(new OleDbParameter { ParameterName = "@AddressP", Value = employee.AddressP });
                        command.Parameters.Add(new OleDbParameter { ParameterName = "@DateOfJoining", Value = employee.DateOfJoining });
                        command.Parameters.Add(new OleDbParameter { ParameterName = "@IsActive", Value = employee.IsActive });
                        command.Parameters.Add(new OleDbParameter { ParameterName = "@Designation", Value = employee.Designation.ID });
                        command.Parameters.Add(new OleDbParameter { ParameterName = "@ModifiedBy", Value = userId });
                        command.Parameters.Add(new OleDbParameter { ParameterName = "@CreateDate", Value = DateTime.Now.ToString() });
                        //command.Parameters.Add(new OleDbParameter { ParameterName = "@ModifiedDate", Value = DBNull.Value });

                        return command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private string GetFullName(string fn, string mn, string ln)
        {
            string fullname = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append(fn.Trim());
            if (!string.IsNullOrEmpty(mn))
            {
                sb.Append(" ");
                sb.Append(mn.Trim());
            }
            if (!string.IsNullOrEmpty(ln))
            {
                sb.Append(" ");
                sb.Append(ln.Trim());
            }
            fullname = sb.ToString();
            return fullname;
        }



        public Employee GetSingleEmployee(long empId)
        {
            Employee theEmployee = null;
            try
            {
                using (OleDbConnection con = DBManager.GetConnection())
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(SelectEmpById, con))
                    {
                        cmd.Parameters.Add(new OleDbParameter { ParameterName = "@empid", Value = empId });
                        OleDbDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                DateTime tempDT = DateTime.MinValue;
                                theEmployee = new Employee
                                {
                                    Id = null != dr["ID"] && string.Empty != dr["ID"].ToString().Trim() ? long.Parse(dr["ID"].ToString().Trim()) : 0,
                                    EmpCode = null != dr["EmpCode"] ? dr["EmpCode"].ToString() : string.Empty,
                                    FirstName = null != dr["FirstName"] ? dr["FirstName"].ToString() : string.Empty,
                                    MiddleName = null != dr["MiddleName"] ? dr["MiddleName"].ToString() : string.Empty,
                                    LastName = null != dr["LastName"] ? dr["LastName"].ToString() : string.Empty,
                                    FathersName = null != dr["FathersName"] ? dr["FathersName"].ToString() : string.Empty,
                                    Gender = null != dr["Gender"] ? dr["Gender"].ToString() : string.Empty,
                                    DateOfBirth = Convert.ToString(dr["DateOfBirth"]),
                                    AddressC = null != dr["AddressC"] ? dr["AddressC"].ToString() : string.Empty,
                                    AddressP = null != dr["AddressP"] ? dr["AddressP"].ToString() : string.Empty,
                                    DateOfJoining = Convert.ToString(dr["DateOfJoining"]),
                                    IsActive = Convert.ToBoolean(dr["IsActive"]),
                                    Designation = new Designation
                                    {
                                        ID = string.IsNullOrEmpty(Convert.ToString(dr["Desig_ID"])) ? long.Parse(dr["Desig_ID"].ToString().Trim()) : 0,
                                        Desc = null != dr["Desig_Desc"] ? Convert.ToString(dr["Desig_Desc"]) : string.Empty
                                    },
                                    FullName = GetFullName(Convert.ToString(dr["FirstName"]), Convert.ToString(dr["MiddleName"]), Convert.ToString(dr["LastName"]))

                                };
                            }
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
            return theEmployee;
        }
    }
}
