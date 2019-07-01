using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using MonitoringService.Enum;
using MonitoringService.Classes;
using System.Web;

namespace MonitoringService.Classes
{
    class LogManager
    {

        #region Varibale

        private static int deviceNumber;
        private static Enum.EnumLOg type;
        private static Enum.EnumKindDevice kind;
        private static Enum.EnumLevel level;
        private static string val;
        private static int Sec = 0;
        public static int DeviceNumber
        {

            get
            {
                return deviceNumber;
            }
            set
            {
                deviceNumber = value;
            }
        }

        public static Enum.EnumLOg Type
        {

            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public static Enum.EnumKindDevice Kind
        {

            get
            {
                return kind;
            }
            set
            {
                kind = value;
            }
        }

        public static Enum.EnumLevel Level
        {

            get
            {
                return level;
            }
            set
            {
                level = value;
            }

        }

        public static string Val
        {
            set
            {
                val = value;
            }
            get
            {
                return val;
            }
        }

        #endregion

        #region Method 

        public LogManager(int c_deviceNumber , Enum.EnumKindDevice c_kind , Enum.EnumLevel c_level , Enum.EnumLOg c_type , string c_val)
        {
            

            DeviceNumber = c_deviceNumber;
            Kind = c_kind;
            Level = c_level;
            Type = c_type;
            Val = c_val;
            Sec = 0;
            
            NewLogInsertToDatabase();
        }


        public LogManager(int c_deviceNumber, Enum.EnumKindDevice c_kind, Enum.EnumLevel c_level, Enum.EnumLOg c_type, string c_val ,int sec)
        {


            DeviceNumber = c_deviceNumber;
            Kind = c_kind;
            Level = c_level;
            Type = c_type;
            Val = c_val;
            Sec = sec;

            NewLogInsertToDatabase();
        }

        public LogManager()
        { }

        private void NewLogInsertToDatabase()
        {
            

            try
            {
                SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();

                SqlCommand cmd = ConnectToDatabase.NewSqlCommand(System.Data.CommandType.StoredProcedure, "Insert_Log_New", cn);

                cmd.Parameters.AddWithValue("dev_no", DeviceNumber);
                cmd.Parameters.AddWithValue("kind", Kind.ToString());
                cmd.Parameters.AddWithValue("comment", classEnumLog.ShowError(Type));
                cmd.Parameters.AddWithValue("level", Level.ToString());
                cmd.Parameters.AddWithValue("intTime", (Convert.ToInt32(PublicMehotd.time_php())+Sec));
                cmd.Parameters.AddWithValue("datep", PublicMehotd.RetStringPersianCalender());
                cmd.Parameters.AddWithValue("timep", PublicMehotd.RetStringLocalTime(Sec));
                cmd.Parameters.AddWithValue("datetimep", PublicMehotd.retStringShowDatetime());
                cmd.Parameters.AddWithValue("value", Val);
                cmd.Parameters.AddWithValue("yearp",PublicMehotd.retStringYearPersian());
                cmd.Parameters.AddWithValue("monthp",PublicMehotd.retStringMonthPersian());
                cmd.Parameters.AddWithValue("dayp",PublicMehotd.retStringDayPersian());

                cn.Open();
                object obj = cmd.ExecuteScalar();
                cn.Close();
                

            }
            catch (Exception ex)
            {
                //Enum.ClassEnumError.ShowErrorInMessageBox(EnumError.insertNewLogProbel);
                log_system.saveLogSystem(ex , "log_manager insert");
            }
        }

     

        #endregion

    }
}
