using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Base
    {

        #region If Using MySQL DB
        //protected static MySqlConnection Create()
        //{
        //    RepoDb.MySqlBootstrap.Initialize();
        //    String connection = Environment.GetEnvironmentVariable("MYSQL").ToString();
        //    if (String.IsNullOrEmpty(connection)) throw new Exception("Database Connection Error: Something wrong when connecting to database. Empty connection.");

        //    MySqlConnection myConnection = new MySqlConnection();
        //    myConnection.ConnectionString = connection;
        //    if (myConnection.State == ConnectionState.Closed)
        //        myConnection.Open();
        //    return myConnection;
        //}
        #endregion

        #region If Using MS SQL Server DB
        protected static SqlConnection Create()
        {
            RepoDb.SqlServerBootstrap.Initialize();
            String connection = Environment.GetEnvironmentVariable("MSSQL").ToString();
            if (String.IsNullOrEmpty(connection)) throw new Exception("Database Connection Error: Something wrong when connecting to database. Empty connection.");

            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connection;
            if (myConnection.State == ConnectionState.Closed)
                myConnection.Open();
            return myConnection;
        }
        #endregion

        protected static String ConvertNull(String input)
        {
            return Microsoft.VisualBasic.Information.IsNothing(input) ? String.Empty : input;
        }
    }
}
