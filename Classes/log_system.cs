using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using MonitoringService;

namespace MonitoringService.Classes
{
    class log_system
    {
        public static void saveLogSystem(String part, String msg, String stack, String source)
        {
            try
            {
                log_system.saveLogFile(part, msg, stack, source);
                /*
                SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();

                SqlCommand cmd = ConnectToDatabase.NewSqlCommand(System.Data.CommandType.StoredProcedure, "insert_log_system", cn);

                cmd.Parameters.AddWithValue("part", part);
                cmd.Parameters.AddWithValue("message", msg);
                cmd.Parameters.AddWithValue("stackTrace", stack);
                cmd.Parameters.AddWithValue("sourcecode", source);
                cmd.Parameters.AddWithValue("datep", PublicMehotd.RetStringPersianCalender());
                cmd.Parameters.AddWithValue("timep", PublicMehotd.RetStringLocalTime());

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                */

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message+data+url);
            }
        }

        public static void saveLogSystem(Exception ex , string part)
        {
            try
            {
                log_system.saveLogFile(part, ex.Message, ex.StackTrace, ex.Source);

                /*
                SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();

                SqlCommand cmd = ConnectToDatabase.NewSqlCommand(System.Data.CommandType.StoredProcedure, "insert_log_system", cn);


                cmd.Parameters.AddWithValue("part", part);
                cmd.Parameters.AddWithValue("message", ex.Message);
                cmd.Parameters.AddWithValue("stackTrace", ex.StackTrace);
                cmd.Parameters.AddWithValue("sourcecode", ex.Source);
                cmd.Parameters.AddWithValue("datep", PublicMehotd.RetStringPersianCalender());
                cmd.Parameters.AddWithValue("timep", PublicMehotd.RetStringLocalTime());

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                */

            }
            catch (Exception ex2)
            {
                //MessageBox.Show("error log system");
            }
        }
        public static void saveLogFile(string part, string msg, string stacktrace, string sourcecode)
        {

            string filepath = @"c:\log_monitoring\";
            string filename = PublicMehotd.RetStringPersianCalenderwithDash() + "-" + DateTime.Now.Hour.ToString("00") + ".txt";
            checkFileAndFolder(filename);

            using (System.IO.StreamWriter file =
            File.AppendText(filepath + filename))
            {
                file.WriteLine("\n\npart = " + part);
                file.WriteLine("\n msg = " + msg);
                file.WriteLine("\n stack tarce = " + stacktrace);
                file.WriteLine("\n source code = " + sourcecode);
                file.WriteLine("\n datep = " + PublicMehotd.RetStringPersianCalender());
                file.WriteLine("\n timep = " + PublicMehotd.RetStringLocalTime());
            }
        }

        public static void saveLogMsg(string msg)
        {
            string filepath = @"c:\log_monitoring\";
            string filename = PublicMehotd.RetStringPersianCalenderwithDash() + "-" + DateTime.Now.Hour.ToString("00") + ".txt";
            checkFileAndFolder(filename);

            using (System.IO.StreamWriter file =
            File.AppendText(filepath + filename))
            {
                file.WriteLine("\n msg = " + msg);
                file.WriteLine("\n datep = " + PublicMehotd.RetStringPersianCalender());
                file.WriteLine("\n timep = " + PublicMehotd.RetStringLocalTime());
            }
        }

        private static void checkFileAndFolder(string filename)
        {
            bool exists = System.IO.Directory.Exists(@"c:\log_monitoring\");

            if (!exists)
                System.IO.Directory.CreateDirectory(@"c:\log_monitoring\");


        }

    }
}
