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
        private const string SelectAllEmp = "SELECT Employee.*, Designation.ID As Desig_ID,Designation.Desc As Desig_Desc, Address.Line1 AS CLine1, Address.State AS CState, Address.City AS CCity, Address.Pincode AS CPincode,p.Line1 As PLine1,p.State As PState,p.City As PCity,p.Pincode As PPincode FROM ((Address RIGHT JOIN Employee ON Address.ID = Employee.CAddressId) LEFT JOIN Designation ON Employee.Designation = Designation.ID) LEFT JOIN Address AS p ON Employee.PAddressId = p.ID;";
        /*@"Select EMPLOYEE.ID,EmpCode,FirstName,MiddleName,LastName,Gender,DateOfBirth,CAddressId,PAddressId,
        DateOfJoining,IsActive,Designation,ModifiedBy,CreateDate,ModifiedDate,Designation.ID As Desig_ID,Designation.Desc 
        As Desig_Desc,c.Line1 As CLine1,c.State As CState,c.City As CCity,c.Pincode As CPinCode,
        p.Line1 As PLine1,p.State As PState,p.City As PCity,p.Pincode As PPinCode
        FROM ((EMPLOYEE INNER JOIN 
        Designation ON Employee.Designation = Designation.ID)
        INNER JOIN Address As c ON c.ID=Employee.CAddressId)
        INNER JOIN Address As p ON p.ID=Employee.PAddressId";*/

        private const string AddEmp = @"Insert into Employee (EmpCode,FirstName,MiddleName,LastName,FathersName,Gender,DateOfBirth,CAddressId,PAddressId,DateOfJoining,IsActive,Designation,ModifiedBy,CreateDate) values
                                         (@EmpCode,@FirstName,@MiddleName,@LastName,@FathersName,@Gender,@DateOfBirth,@CAddressId,@PAddressId,@DateOfJoining,@IsActive,@Designation,@ModifiedBy,@CreateDate)";

        private const string SelectEmpById =
            "SELECT Employee.*, Designation.ID As Desig_ID,Designation.Desc As Desig_Desc, Address.Line1 AS CLine1, Address.State AS CState, Address.City AS CCity, Address.Pincode AS CPincode,p.Line1 As PLine1,p.State As PState,p.City As PCity,p.Pincode As PPincode FROM ((Address RIGHT JOIN Employee ON Address.ID = Employee.CAddressId) LEFT JOIN Designation ON Employee.Designation = Designation.ID) LEFT JOIN Address AS p ON Employee.PAddressId = p.ID WHERE EMPLOYEE.ID = @empid;";
        /*@"Select EMPLOYEE.ID,EmpCode,FirstName,MiddleName,LastName,FathersName,Gender,DateOfBirth,AddressC,AddressP,
                            DateOfJoining,IsActive,Designation,ModifiedBy,CreateDate,ModifiedDate,Designation.ID As Desig_ID,Designation.Desc As Desig_Desc
                            FROM EMPLOYEE INNER JOIN Designation ON Employee.Designation = Designation.ID
                            WHERE EMPLOYEE.ID = @empid";
        */
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
                                    DateOfBirth = Convert.ToString(dr["DateOfBirth"]),
                                    AddressC = new Address()
                                                   {
                                                       Line1 = Convert.ToString(dr["CLine1"]),
                                                       CityId = DBNull.Value != dr["CState"] ? Convert.ToInt64(dr["CCity"]) : 0,
                                                       StateId = DBNull.Value != dr["CState"] ? Convert.ToInt64(dr["CState"]) : 0,
                                                       Pincode = DBNull.Value != dr["CPincode"] ? Convert.ToInt64(dr["CPincode"]) : 0
                                                   },
                                    AddressP = new Address()
                                    {
                                        Line1 = Convert.ToString(dr["PLine1"]),
                                        CityId = DBNull.Value != dr["PState"] ? Convert.ToInt64(dr["PCity"]) : 0,
                                        StateId = DBNull.Value != dr["PState"] ? Convert.ToInt64(dr["PState"]) : 0,
                                        Pincode = DBNull.Value != dr["PPincode"] ? Convert.ToInt64(dr["PPincode"]) : 0
                                    },
                                    DateOfJoining = Convert.ToString(dr["DateOfJoining"]),
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
                        command.Parameters.Add(new OleDbParameter { ParameterName = "@CAddressId", Value = employee.AddressC.Id });
                        command.Parameters.Add(new OleDbParameter { ParameterName = "@PAddressId", Value = employee.AddressP.Id });
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
                using (var con = DBManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(SelectEmpById, con))
                    {
                        cmd.Parameters.Add(new OleDbParameter { ParameterName = "@empid", Value = empId });
                        var dr = cmd.ExecuteReader();
                        if (dr.HasRows)
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
                                    DateOfBirth = Convert.ToString(dr["DateOfBirth"]),
                                    AddressC = new Address()
                                    {
                                        Line1 = Convert.ToString(dr["CLine1"]),
                                        CityId = DBNull.Value != dr["CState"] ? Convert.ToInt64(dr["CCity"]) : 0,
                                        StateId = DBNull.Value != dr["CState"] ? Convert.ToInt64(dr["CState"]) : 0,
                                        Pincode = DBNull.Value != dr["CPincode"] ? Convert.ToInt64(dr["CPincode"]) : 0
                                    },
                                    AddressP = new Address()
                                    {
                                        Line1 = Convert.ToString(dr["PLine1"]),
                                        CityId = DBNull.Value != dr["PState"] ? Convert.ToInt64(dr["PCity"]) : 0,
                                        StateId = DBNull.Value != dr["PState"] ? Convert.ToInt64(dr["PState"]) : 0,
                                        Pincode = DBNull.Value != dr["PPincode"] ? Convert.ToInt64(dr["PPincode"]) : 0
                                    },
                                    DateOfJoining = Convert.ToString(dr["DateOfJoining"]),
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
    }
}
