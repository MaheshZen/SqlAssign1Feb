using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlAssign1Feb
{
    class MyTableWithParams
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        int i = 0;
        public int InsertWithParam()
        {
            try
            {
                Console.WriteLine("Enter Employee Name");
                var ename = Console.ReadLine();
                Console.WriteLine("Enter Employee Salary");
                var esal = Convert.ToSingle(Console.ReadLine());
                Console.WriteLine("Enter Employee DeptNo");
                var dno = Convert.ToInt32(Console.ReadLine());

                con = new SqlConnection("Data Source=DESKTOP-8P846M1;Initial Catalog=WFA3DotNet;Integrated Security=True");
                con.Open();
                cmd = new SqlCommand("insert into employeetab values(@empname,@salary,@deptno)", con);
                cmd.Parameters.Add("@empname", SqlDbType.VarChar, 20).Value = ename;
                cmd.Parameters.Add("@salary", SqlDbType.Float).Value = esal;
                cmd.Parameters.Add("@deptno", SqlDbType.Int).Value = dno;

                i = cmd.ExecuteNonQuery();
                ShowData();
                return i;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return i;
            }
            finally
            {
                con.Close();
            }
        }
        public void UpdateWithParam()
        {
            try
            {
                ShowData();
                Console.WriteLine("Enter Employee Id:");
                int eid = Convert.ToInt32(Console.ReadLine());
                ShowDataWithParam(eid);
                Console.WriteLine("Select Which column you want to update");
                Console.WriteLine("1)Name\t2)Salary\t3)DeptNo");
                int a = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter New Value");
                var newValue = Console.ReadLine();

                con = new SqlConnection("Data Source=DESKTOP-8P846M1;Initial Catalog=WFA3DotNet;Integrated Security=True");
                con.Open();
                switch (a)
                {
                    case 1:
                        cmd = new SqlCommand("update EmployeeTab set empName=@empname where empid=@empid", con);
                        cmd.Parameters.Add("@empname", SqlDbType.VarChar, 20).Value = newValue;
                        cmd.Parameters.Add("@Empid", SqlDbType.Int).Value = eid;

                        i = cmd.ExecuteNonQuery();
                        Console.WriteLine(i + " row updated");
                        ShowDataWithParam(eid);
                        break;
                    case 2:
                        cmd = new SqlCommand("update EmployeeTab set salary=@salary where empid=@empid", con);
                        cmd.Parameters.Add("@salary", SqlDbType.Float).Value = newValue;
                        cmd.Parameters.Add("@Empid", SqlDbType.Int).Value = eid;
                        i = cmd.ExecuteNonQuery();
                        Console.WriteLine(i + " row updated");
                        ShowDataWithParam(eid);
                        break;
                    case 3:
                        cmd = new SqlCommand("update EmployeeTab set deptno=@deptno where empid=@empid", con);
                        cmd.Parameters.Add("@deptno", SqlDbType.Int).Value = newValue;
                        cmd.Parameters.Add("@Empid", SqlDbType.Int).Value = eid;

                        i = cmd.ExecuteNonQuery();
                        Console.WriteLine(i + " row updated");
                        ShowDataWithParam(eid);
                        break;
                    default:
                        Console.WriteLine("Enter 1 to 3");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public void DeleteWithParam()
        {
            try
            {
                ShowData();
                Console.WriteLine("Enter Employee Id:");
                int eid = Convert.ToInt32(Console.ReadLine());

                con = new SqlConnection("Data Source=DESKTOP-8P846M1;Initial Catalog=WFA3DotNet;Integrated Security=True");
                con.Open();
                cmd = new SqlCommand("delete from EmployeeTab where empid=@empid", con);
                
                cmd.Parameters.Add("@Empid", SqlDbType.Int).Value = eid;

                i = cmd.ExecuteNonQuery();
                Console.WriteLine(i + " row deleted");
                ShowData();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public void ShowData()
        {
            try
            {
                Console.WriteLine("EMPLOYEE TABLE");

                con = new SqlConnection("Data Source=DESKTOP-8P846M1;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("select * from employeetab", con);
                con.Open();
                dr = cmd.ExecuteReader();
                
                int col = dr.FieldCount;

                while (dr.Read())
                {
                    for (int i = 0; i < col; i++)
                    {
                        if (dr.HasRows)
                        {
                            Console.Write(dr[i] + "\t");
                        }
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
                Console.ReadLine();
            }
        }
        public void ShowDataWithParam(int id)
        {
            try
            {
                Console.WriteLine("EMPLOYEE TABLE");

                con = new SqlConnection("Data Source=DESKTOP-8P846M1;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("select * from employeetab where empid=@empid", con);
                con.Open();
                cmd.Parameters.Add("@empid", SqlDbType.Int).Value = id;

                dr = cmd.ExecuteReader();

                int col = dr.FieldCount;

                while (dr.Read())
                {
                    for (int i = 0; i < col; i++)
                    {
                        if (dr.HasRows)
                        {
                            Console.Write(dr[i] + "\t");
                        }
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
                Console.ReadLine();
            }
        }
    }
    class MainMethod
    {
        static void Main(string[] args)
        {
            MyTableWithParams mt = new MyTableWithParams();
            bool b = true;
            int res = 0;
            while (b)
            {
                Console.WriteLine("---With Params---");
                Console.WriteLine("Select an Option");
                Console.WriteLine("1)Insert\t2)Update\n3)Delete\t4)Select\n5)Exit");
                int a = Convert.ToInt32(Console.ReadLine());

                switch (a)
                {
                    case 1:
                        res = mt.InsertWithParam();
                        Console.WriteLine(res + " row Inserted");
                        break;
                    case 2:
                        mt.UpdateWithParam();
                        break;
                    case 3:
                        mt.DeleteWithParam();
                        break;
                    case 4:
                        mt.ShowData();
                        break;
                    case 5:
                        b = false;
                        break;
                    default:
                        Console.WriteLine("Enter between 1 to 5");
                        break;
                }
            }
        }
    }
}
