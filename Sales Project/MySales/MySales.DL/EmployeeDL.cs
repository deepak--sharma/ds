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
        #region Private constants
        private const string SelectAllEmp = "SELECT Employee.*, Designation.ID As Desig_ID,Designation.Desc As Desig_Desc,Address.ID As CID, Address.Line1 AS CLine1, Address.State AS CState, Address.City AS CCity, Address.Pincode AS CPincode,p.ID As PID,p.Line1 As PLine1,p.State As PState,p.City As PCity,p.Pincode As PPincode FROM ((Address RIGHT JOIN Employee ON Address.ID = Employee.CAddressId) LEFT JOIN Designation ON Employee.Designation = Designation.ID) LEFT JOIN Address AS p ON Employee.PAddressId = p.ID;";

        private const string AddEmp = @"Insert into Employee (FirstName,MiddleName,LastName,FathersName,Gender,DateOfBirth,MobileNo,OtherNo,DateOfJoining,Designation,ModifiedBy,CAddressId,PAddressId,EmpCode,IsActive,CreateDate) values
                                         (@FirstName,@MiddleName,@LastName,@FathersName,@Gender,@DateOfBirth,@MobileNo,@OtherNo,@DateOfJoining,@Designation,@ModifiedBy,@CAddressId,@PAddressId,@EmpCode,@IsActive,@CreateDate)";

        private const string SelectEmpById =
            "SELECT Employee.*, Designation.ID As Desig_ID,Designation.Desc As Desig_Desc,Address.ID As CID, Address.Line1 AS CLine1, Address.State AS CState, Address.City AS CCity, Address.Pincode AS CPincode,p.ID As PID,p.Line1 As PLine1,p.State As PState,p.City As PCity,p.Pincode As PPincode FROM ((Address RIGHT JOIN Employee ON Address.ID = Employee.CAddressId) LEFT JOIN Designation ON Employee.Designation = Designation.ID) LEFT JOIN Address AS p ON Employee.PAddressId = p.ID WHERE EMPLOYEE.ID = @empid;";

        private const string SelectEmpAdvDetails =
            "SELECT Employee.ID,Employee.EmpCode,Employee.FirstName,Employee.MiddleName,Employee.LastName, Emp_Attendance.ID As AttID,Emp_Attendance.WorkDays,Emp_Attendance.LeaveDays,Emp_Attendance.Overtime, Emp_Advance_Details.ID As AdvID,Emp_Advance_Details.TotalAdvance,Emp_Advance_Details.AdvanceDeduction,Emp_Advance_Details.Balance FROM (Employee LEFT JOIN Emp_Advance_Details ON Employee.ID = Emp_Advance_Details.EmpID) LEFT JOIN Emp_Attendance ON Employee.ID = Emp_Attendance.EmpID WHERE Emp_Attendance.PayrollMonth=@pm And Emp_Attendance.PayrollYear=@yr;";
        private const string SelectAllActiveEmployees = "SELECT ID FROM EMPLOYEE WHERE ISACTIVE=TRUE";
        private const string UpdateEmployee = "Update Employee set FirstName=@FirstName,MiddleName=@MiddleName,LastName=@LastName,FathersName=@FathersName,Gender=@Gender,DateOfBirth=@DateOfBirth,MobileNo=@MobileNo,OtherNo=@OtherNo,DateOfJoining=@DateOfJoining,Designation=@Designation,ModifiedBy=@ModifiedBy,CAddressId=@CAddressId,PAddressId=@PAddressId,ModifiedDate=@ModifiedDate where ID=@ID";
        #endregion

        #region Public Methods
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
                                    Id = DBNull.Value != dr["ID"] && string.Empty != dr["ID"].ToString().Trim() ? long.Parse(dr["ID"].ToString().Trim()) : 0,
                                    EmpCode = DBNull.Value != dr["EmpCode"] ? dr["EmpCode"].ToString() : string.Empty,
                                    FirstName = DBNull.Value != dr["FirstName"] ? dr["FirstName"].ToString() : string.Empty,
                                    MiddleName = DBNull.Value != dr["MiddleName"] ? dr["MiddleName"].ToString() : string.Empty,
                                    LastName = DBNull.Value != dr["LastName"] ? dr["LastName"].ToString() : string.Empty,
                                    Gender = DBNull.Value != dr["Gender"] ? dr["Gender"].ToString() : string.Empty,
                                    DateOfBirth = DBNull.Value != dr["DateOfBirth"] ? DateTime.Parse(Convert.ToString(dr["DateOfBirth"])) : DateTime.Now,
                                    AddressC = new Address()
                                                   {
                                                       Id = DBNull.Value != dr["CID"] ? Convert.ToInt64(dr["CID"]) : 0,
                                                       Line1 = Convert.ToString(dr["CLine1"]),
                                                       CityId = DBNull.Value != dr["CState"] ? Convert.ToInt64(dr["CCity"]) : 0,
                                                       StateId = DBNull.Value != dr["CState"] ? Convert.ToInt64(dr["CState"]) : 0,
                                                       Pincode = DBNull.Value != dr["CPincode"] ? Convert.ToInt64(dr["CPincode"]) : 0
                                                   },
                                    AddressP = new Address()
                                    {
                                        Id = DBNull.Value != dr["PID"] ? Convert.ToInt64(dr["PID"]) : 0,
                                        Line1 = Convert.ToString(dr["PLine1"]),
                                        CityId = DBNull.Value != dr["PState"] ? Convert.ToInt64(dr["PCity"]) : 0,
                                        StateId = DBNull.Value != dr["PState"] ? Convert.ToInt64(dr["PState"]) : 0,
                                        Pincode = DBNull.Value != dr["PPincode"] ? Convert.ToInt64(dr["PPincode"]) : 0
                                    },
                                    DateOfJoining = DBNull.Value != dr["DateOfJoining"] ? DateTime.Parse(Convert.ToString(dr["DateOfJoining"])) : DateTime.Now,
                                    IsActive = Convert.ToBoolean(dr["IsActive"]),
                                    Designation = new Designation
                                    {
                                        ID = string.IsNullOrEmpty(Convert.ToString(dr["Desig_ID"])) ? long.Parse(dr["Desig_ID"].ToString().Trim()) : 0,
                                        Desc = DBNull.Value != dr["Desig_Desc"] ? Convert.ToString(dr["Desig_Desc"]) : string.Empty
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

        public List<Employee> GetEmpAdvanceDetails(int payrollMonth, int payrollYear)
        {
            var lstEmployee = new List<Employee>();
            try
            {
                using (var con = DBManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(SelectEmpAdvDetails, con))
                    {
                        cmd.Parameters.Add(new OleDbParameter { ParameterName = "@pm", Value = payrollMonth });
                        cmd.Parameters.Add(new OleDbParameter { ParameterName = "@yr", Value = payrollYear });

                        var dr = cmd.ExecuteReader();
                        if (dr != null && dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var theEmployee = new Employee
                                {
                                    Id = DBNull.Value != dr["ID"] && string.Empty != dr["ID"].ToString().Trim() ? long.Parse(dr["ID"].ToString().Trim()) : 0,
                                    EmpCode = DBNull.Value != dr["EmpCode"] ? dr["EmpCode"].ToString() : string.Empty,
                                    FirstName = DBNull.Value != dr["FirstName"] ? dr["FirstName"].ToString() : string.Empty,
                                    MiddleName = DBNull.Value != dr["MiddleName"] ? dr["MiddleName"].ToString() : string.Empty,
                                    LastName = DBNull.Value != dr["LastName"] ? dr["LastName"].ToString() : string.Empty,
                                    FullName = GetFullName(Convert.ToString(dr["FirstName"]), Convert.ToString(dr["MiddleName"]), Convert.ToString(dr["LastName"]))
                                    ,
                                    AdvanceDetails = new AdvanceDetail()
                                                         {
                                                             ID = dr["AdvID"] != DBNull.Value && !string.IsNullOrEmpty(dr["AdvID"].ToString()) ? long.Parse(dr["AdvID"].ToString().Trim()) : 0,
                                                             TotalAdvance = dr["TotalAdvance"] != DBNull.Value ? decimal.Parse(dr["TotalAdvance"].ToString().Trim()) : 0,
                                                             AdvanceDeduction = dr["AdvanceDeduction"] != DBNull.Value ? decimal.Parse(dr["AdvanceDeduction"].ToString().Trim()) : 0,
                                                             Balance = dr["Balance"] != DBNull.Value ? decimal.Parse(dr["Balance"].ToString().Trim()) : 0,
                                                             AdvAction = dr["AdvID"] == DBNull.Value ? 'I' : 'U'
                                                         }
                                    ,
                                    Attendance = new EmpAttendance()
                                                     {
                                                         ID = dr["AttID"] != DBNull.Value ? long.Parse(dr["AttID"].ToString().Trim()) : 0,
                                                         WorkDays = dr["WorkDays"] != DBNull.Value ? long.Parse(dr["WorkDays"].ToString().Trim()) : 0,
                                                         LeaveDays = dr["LeaveDays"] != DBNull.Value ? long.Parse(dr["LeaveDays"].ToString().Trim()) : 0,
                                                         Overtime = dr["Overtime"] != DBNull.Value ? long.Parse(dr["Overtime"].ToString().Trim()) : 0,
                                                         AttAction = dr["AttID"] == DBNull.Value ? 'I' : 'U'
                                                     }
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


        public Utility.ActionStatus AddUpdateEmployee(Employee employee, Int64 userId)
        {
            var result = Utility.ActionStatus.SUCCESS;
            try
            {
                using (var con = DBManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter
                                               {
                                                   ParameterName = "@FirstName",
                                                   Value = employee.FirstName,
                                                   OleDbType = OleDbType.VarChar
                                               });
                        cmd.Parameters.Add(new OleDbParameter
                                               {
                                                   ParameterName = "@MiddleName",
                                                   Value = employee.MiddleName,
                                                   OleDbType = OleDbType.VarChar
                                               });
                        cmd.Parameters.Add(new OleDbParameter
                                               {
                                                   ParameterName = "@LastName",
                                                   Value = employee.LastName,
                                                   OleDbType = OleDbType.VarChar
                                               });
                        cmd.Parameters.Add(new OleDbParameter
                                               {
                                                   ParameterName = "@FathersName",
                                                   Value = employee.FathersName,
                                                   OleDbType = OleDbType.VarChar
                                               });
                        cmd.Parameters.Add(new OleDbParameter
                                               {
                                                   ParameterName = "@Gender",
                                                   Value = employee.Gender,
                                                   OleDbType = OleDbType.VarChar
                                               });
                        cmd.Parameters.Add(new OleDbParameter
                                               {
                                                   ParameterName = "@DateOfBirth",
                                                   Value = employee.DateOfBirth,
                                                   OleDbType = OleDbType.Date
                                               });
                        cmd.Parameters.Add(new OleDbParameter
                                               {
                                                   ParameterName = "@MobileNo",
                                                   Value = employee.MobileNo,
                                                   OleDbType = OleDbType.VarChar
                                               });
                        cmd.Parameters.Add(new OleDbParameter
                                               {
                                                   ParameterName = "@OtherNo",
                                                   Value = employee.OtherNo,
                                                   OleDbType = OleDbType.VarChar
                                               });
                        cmd.Parameters.Add(new OleDbParameter
                                               {
                                                   ParameterName = "@DateOfJoining",
                                                   Value = employee.DateOfJoining,
                                                   OleDbType = OleDbType.Date
                                               });
                        cmd.Parameters.Add(new OleDbParameter
                                               {
                                                   ParameterName = "@Designation",
                                                   Value = employee.Designation.ID,
                                                   OleDbType = OleDbType.BigInt
                                               });
                        cmd.Parameters.Add(new OleDbParameter
                                               {
                                                   ParameterName = "@ModifiedBy",
                                                   Value = userId,
                                                   OleDbType = OleDbType.BigInt
                                               });
                        cmd.Parameters.Add(new OleDbParameter
                                               {
                                                   ParameterName = "@CAddressId",
                                                   Value = employee.AddressC.Id
                                               });
                        cmd.Parameters.Add(new OleDbParameter
                                               {
                                                   ParameterName = "@PAddressId",
                                                   Value = employee.AddressP.Id
                                               });
                        cmd.Connection = con;
                        if (employee.Id > 0)
                        {
                            //Update employee record.
                            cmd.CommandText = UpdateEmployee;
                            cmd.Parameters.Add(new OleDbParameter
                            {
                                ParameterName = "@ModifiedDate",
                                Value = DateTime.Now,
                                OleDbType = OleDbType.Date
                            });
                            cmd.Parameters.Add(new OleDbParameter
                                                   {
                                                       ParameterName = "@ID",
                                                       Value = employee.Id
                                                       //OleDbType = OleDbType.BigInt
                                                   });

                        }
                        else
                        {
                            //Add new employee record.
                            cmd.CommandText = AddEmp;
                            cmd.Parameters.Add(new OleDbParameter
                                                   {
                                                       ParameterName = "@EmpCode",
                                                       Value = employee.EmpCode
                                                   });
                            cmd.Parameters.Add(new OleDbParameter
                                                   {
                                                       ParameterName = "@IsActive",
                                                       Value = employee.IsActive
                                                   });
                            cmd.Parameters.Add(new OleDbParameter
                                                   {
                                                       ParameterName = "@CreateDate",
                                                       Value = DateTime.Now,
                                                       OleDbType = OleDbType.Date
                                                   });
                        }

                        var queryResult = cmd.ExecuteNonQuery();
                        if (queryResult <= 0)
                        {
                            result = Utility.ActionStatus.FAILURE;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = Utility.ActionStatus.FAILURE;
                throw ex;
            }
            return result;
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
                using (var con = DBManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(SelectEmpById, con))
                    {
                        cmd.Parameters.Add(new OleDbParameter { ParameterName = "@empid", Value = empId });
                        var dr = cmd.ExecuteReader();
                        if (dr != null && dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var tempDT = DateTime.MinValue;
                                theEmployee = new Employee
                                {
                                    Id = DBNull.Value != dr["ID"] && string.Empty != dr["ID"].ToString().Trim() ? long.Parse(dr["ID"].ToString().Trim()) : 0,
                                    EmpCode = DBNull.Value != dr["EmpCode"] ? dr["EmpCode"].ToString() : string.Empty,
                                    FirstName = DBNull.Value != dr["FirstName"] ? dr["FirstName"].ToString() : string.Empty,
                                    MiddleName = DBNull.Value != dr["MiddleName"] ? dr["MiddleName"].ToString() : string.Empty,
                                    LastName = DBNull.Value != dr["LastName"] ? dr["LastName"].ToString() : string.Empty,
                                    FathersName = DBNull.Value != dr["FathersName"] ? dr["FathersName"].ToString() : string.Empty,
                                    Gender = DBNull.Value != dr["Gender"] ? dr["Gender"].ToString() : string.Empty,
                                    MobileNo = DBNull.Value != dr["MobileNo"] ? dr["MobileNo"].ToString() : string.Empty,
                                    OtherNo = DBNull.Value != dr["OtherNo"] ? dr["OtherNo" +
                                                                                 "" +
                                                                                 ""].ToString() : string.Empty,
                                    DateOfBirth = DBNull.Value != dr["DateOfBirth"] ? DateTime.Parse(Convert.ToString(dr["DateOfBirth"])) : DateTime.Now,
                                    AddressC = new Address()
                                    {
                                        Id = DBNull.Value != dr["CID"] ? Convert.ToInt64(dr["CID"]) : 0,
                                        Line1 = Convert.ToString(dr["CLine1"]),
                                        CityId = DBNull.Value != dr["CState"] ? Convert.ToInt64(dr["CCity"]) : 0,
                                        StateId = DBNull.Value != dr["CState"] ? Convert.ToInt64(dr["CState"]) : 0,
                                        Pincode = DBNull.Value != dr["CPincode"] ? Convert.ToInt64(dr["CPincode"]) : 0
                                    },
                                    AddressP = new Address()
                                    {
                                        Id = DBNull.Value != dr["PID"] ? Convert.ToInt64(dr["PID"]) : 0,
                                        Line1 = Convert.ToString(dr["PLine1"]),
                                        CityId = DBNull.Value != dr["PState"] ? Convert.ToInt64(dr["PCity"]) : 0,
                                        StateId = DBNull.Value != dr["PState"] ? Convert.ToInt64(dr["PState"]) : 0,
                                        Pincode = DBNull.Value != dr["PPincode"] ? Convert.ToInt64(dr["PPincode"]) : 0
                                    },
                                    DateOfJoining = DBNull.Value != dr["DateOfJoining"] ? DateTime.Parse(Convert.ToString(dr["DateOfJoining"])) : DateTime.Now,
                                    IsActive = Convert.ToBoolean(dr["IsActive"]),
                                    Designation = new Designation
                                    {
                                        ID = string.IsNullOrEmpty(Convert.ToString(dr["Desig_ID"])) ? long.Parse(dr["Desig_ID"].ToString().Trim()) : 0,
                                        Desc = DBNull.Value != dr["Desig_Desc"] ? Convert.ToString(dr["Desig_Desc"]) : string.Empty
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

        public Utility.ActionStatus InsertAttentancePlaceholder(int payrollMonth, int payrollYear)
        {
            var result = Utility.ActionStatus.SUCCESS;
            try
            {
                using (var con = DBManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(SelectAllActiveEmployees, con))
                    {
                        var dr = cmd.ExecuteReader();
                        if (dr != null && dr.HasRows)
                        {
                            while (dr.Read())
                            {

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result = Utility.ActionStatus.FAILURE;
            }

            return result;
        }
        #endregion
    }
}
