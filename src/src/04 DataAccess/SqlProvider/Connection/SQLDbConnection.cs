using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace MYDiary.SQLProvider.Connection
{
   public static class SQLDbConnection
    {
       //private static string ConnectionString = "Server:USER-PC\\SQLEXPRESS;initial catalog=MyApps;UserName:sa;Password:sa1234;";

       public static SqlConnection GetNewSqlConnectionObject()
       {
           string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDiaryConnection"].ConnectionString;
           SqlConnection conn =new SqlConnection();
           conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyDiaryConnection"].ConnectionString.ToString();
           return conn;
       }

       public static SqlCommand GetNewSqlCommandObject(SqlConnection conn,string sp)
       {
           SqlCommand cmd = new SqlCommand(sp, conn);
           cmd.CommandType = CommandType.StoredProcedure;
           //cmd.Connection = conn;
           return cmd;
       }

       public static SqlDataAdapter GetNewSqlDataAdapterObject(SqlCommand cmd)
       {          
           return new SqlDataAdapter(cmd);

       }

       public static DataTable FillDataSetFromDataAdapter(SqlDataAdapter dataAdapter)
       {
           DataSet ds = new DataSet();
           dataAdapter.Fill(ds);
           return ds.Tables[0];   
        
           
       }


    }
}
