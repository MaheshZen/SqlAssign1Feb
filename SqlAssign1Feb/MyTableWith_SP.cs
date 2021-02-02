using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlAssign1Feb
{
    
    class MyTableWith_SP
    {
        MyTable mt = new MyTable();
        SqlConnection con = null;
        SqlCommand cmd = null;
        int i = 0;
        public int InsertWithSp()
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
                cmd = new SqlCommand("sp_InsertRow", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@empname", SqlDbType.VarChar, 20).Value = ename;
                cmd.Parameters.Add("@esal", SqlDbType.Float).Value = esal;
                cmd.Parameters.Add("@deptno", SqlDbType.Int).Value = dno;

                i = cmd.ExecuteNonQuery();
                mt.ShowData();
                return i;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return i;
            }
        }
        public int UpdateWithSp()
        {
            int i = 0;
            try
            {
                mt.ShowData();
                Console.WriteLine("Enter Employee Id");
                var eid = Convert.ToInt32(Console.ReadLine());
                SelectWithSp(eid);
                Console.WriteLine("Enter Employee Name");
                var ename = Console.ReadLine();

                Console.WriteLine("Enter Employee Salary");
                var esal = Convert.ToSingle(Console.ReadLine());

                Console.WriteLine("Enter Employee DeptNo");
                var dno = Convert.ToInt32(Console.ReadLine());

                con = new SqlConnection("Data Source=DESKTOP-8P846M1;Initial Catalog=WFA3DotNet;Integrated Security=True");
                con.Open();
                cmd = new SqlCommand("sp_UpdateEmp", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@empid", SqlDbType.VarChar, 20).Value = eid;
                cmd.Parameters.Add("@empname", SqlDbType.VarChar, 20).Value = ename;
                cmd.Parameters.Add("@esal", SqlDbType.Float).Value = esal;
                cmd.Parameters.Add("@deptno", SqlDbType.Int).Value = dno;

                i = cmd.ExecuteNonQuery();
                SelectWithSp(eid);
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

        public void SelectWithSp(int eid)
        {
            int i = 0;
            SqlDataReader dr = null;
            try
            {
                con = new SqlConnection("Data Source=DESKTOP-8P846M1;Initial Catalog=WFA3DotNet;Integrated Security=True");
                con.Open();
                cmd = new SqlCommand("sp_SelectWithId", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@empid", SqlDbType.Int).Value = eid;

                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Console.WriteLine($"Id: {dr["empid"]}\nName: {dr["empname"]}\nSalary: {dr["salary"]}\nDeptName:{dr["deptname"]}");
                    }
                }
                else
                {
                    Console.WriteLine("No Data Found");
                }
                //ShowData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public void DeleteWithSp()
        {
            int i = 0;
            
            try
            {
                mt.ShowData();
                Console.WriteLine("Enter Employee Id");
                var eid = Console.ReadLine();

                con = new SqlConnection("Data Source=DESKTOP-8P846M1;Initial Catalog=WFA3DotNet;Integrated Security=True");
                con.Open();
                cmd = new SqlCommand("sp_DeleteWithId", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@empid", SqlDbType.Int).Value = eid;
                i = cmd.ExecuteNonQuery();
                if (i>0)
                {
                    Console.WriteLine(i+" row(s) deleted");
                }
                else
                    Console.WriteLine("Error");
                mt.ShowData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

    }
    class MainMthd
    {
        static void Main()
        {
            MyTableWith_SP msp = new MyTableWith_SP();
            bool b = true;
            int res = 0;
            while (b)
            {
                Console.WriteLine("---With Params---");
                Console.WriteLine("Select an Option");
                Console.WriteLine("1)Insert\t2)Update\n3)Delete\t4)Select With Id\n5)ShowData\t6)Exit");
                int a = Convert.ToInt32(Console.ReadLine());

                switch (a)
                {
                    case 1:
                        res = msp.InsertWithSp();
                        Console.WriteLine(res + " row Inserted");
                        break;
                    case 2:
                        msp.UpdateWithSp();
                        break;
                    case 3:
                        msp.DeleteWithSp();
                        break;
                    case 4:
                        Console.WriteLine("Enter Id to Show Details");
                        int id = Convert.ToInt32(Console.ReadLine());
                        msp.SelectWithSp(id);
                        break;
                    case 5:
                        MyTable mt = new MyTable();
                        mt.ShowData();
                        break;
                    case 6:
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
