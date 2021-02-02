using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SqlAssign1Feb
{
    class MyTable
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        int i = 0;
        public int InsertRow()
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
                cmd = new SqlCommand("insert into EmployeeTab values('" + ename + "'," + esal + "," + dno + ")", con);

                i = cmd.ExecuteNonQuery();
                //ShowData();
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
        public void UpdateRow()
        {
            try
            {
                ShowData();
                Console.WriteLine("Enter Employee Id:");
                int eid = Convert.ToInt32(Console.ReadLine());
                ShowData(eid);
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
                        cmd = new SqlCommand("update EmployeeTab set empName='" + newValue + "' where empid=" + eid, con);
                        i = cmd.ExecuteNonQuery();
                        Console.WriteLine(i + " row updated");
                        ShowData(eid);
                        break;
                    case 2:
                        cmd = new SqlCommand("update EmployeeTab set salary=" + newValue + " where empid=" + eid, con);
                        i = cmd.ExecuteNonQuery();
                        Console.WriteLine(i + " row updated");
                        ShowData(eid);
                        break;
                    case 3:
                        cmd = new SqlCommand("update EmployeeTab set deptno=" + newValue + " where empid=" + eid, con);
                        i = cmd.ExecuteNonQuery();
                        Console.WriteLine(i + " row updated");
                        ShowData(eid);
                        break;
                    default:
                        Console.WriteLine("Enter 1 to 3");
                        break;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public void DeleteRow()
        {
            try
            {
                ShowData();
                Console.WriteLine("Enter Employee Id:");
                int eid = Convert.ToInt32(Console.ReadLine());

                con = new SqlConnection("Data Source=DESKTOP-8P846M1;Initial Catalog=WFA3DotNet;Integrated Security=True");
                con.Open();
                cmd = new SqlCommand("delete from EmployeeTab where empid=" + eid, con);
                i = cmd.ExecuteNonQuery();
                Console.WriteLine(i+" row deleted");
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
                //while (dr.Read())
                //{
                //    Console.WriteLine($"{dr["empid"]}\t{dr["empname"]}\t{dr["salary"]}\t{dr["deptno"]}");
                //}
                int col = dr.FieldCount;
                
                while (dr.Read())
                {
                    for (int i = 0; i < col;  i++)
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
        public void ShowData(int id)
        {
            try
            {
                Console.WriteLine("EMPLOYEE TABLE");

                con = new SqlConnection("Data Source=DESKTOP-8P846M1;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("select * from employeetab where empid="+id, con);
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
    }
    class Program
    {
        static void Main(string[] args)
        {
            MyTable t = new MyTable();
            bool b = true;
            int res = 0;
            while(b)
            {
                Console.WriteLine("Select an Option");
                Console.WriteLine("1)Insert\t2)Update\n3)Delete\t4)Select\n5)Exit");
                int a = Convert.ToInt32(Console.ReadLine());

                switch (a)
                {
                    case 1:
                        res=t.InsertRow();
                        Console.WriteLine(res+" row Inserted");
                        break;
                    case 2:
                        t.UpdateRow();
                        break;
                    case 3:
                        t.DeleteRow();
                        break;
                    case 4:
                        t.ShowData();
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
