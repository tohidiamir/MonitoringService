using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace MonitoringService.Classes
{
    class ConnectToDatabase
    {

        public static SqlConnection NewConnectToDatabase()
        {
            SqlConnection cn = new SqlConnection();
            try
            {
              

                cn.ConnectionString = readFromAppConfig("stringConnect");

            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "new connect databas e");
            }

            return cn;

        }

        public static SqlCommand NewSqlCommand(CommandType type, string commandText, SqlConnection cn)
        {
            SqlCommand cmd = new SqlCommand();
            try {
                
                cmd.CommandType = type;
                cmd.CommandText = commandText;
                cmd.Connection = cn;
                cmd.CommandTimeout = 20000;
                
            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "new sql command");
            }

            return cmd;
        }

        public static string readFromAppConfig(string key)
        {
            try {
                System.Configuration.AppSettingsReader ar = new System.Configuration.AppSettingsReader();

                return ar.GetValue(key, typeof(string)).ToString();
            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex , "read from config ");
                return "";
            }
        }

        public static void FreeLog()
        {
            //SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();
            //SqlCommand cmd = ConnectToDatabase.NewSqlCommand(CommandType.Text, "BACKUP LOG [DB_Monitoring] WITH TRUNCATE_ONLY", cn);

            //cn.Open();

            //cmd.ExecuteNonQuery();

            //cn.Close();
        }


        public static void FreeSpace()
        {
            SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();
            SqlCommand cmd = ConnectToDatabase.NewSqlCommand(CommandType.Text, "DBCC ShrinkDataBase (DB_Monitoring, 0)", cn);

            cn.Open();

            cmd.ExecuteNonQuery();

            cn.Close();

        }

    }
}
