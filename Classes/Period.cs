using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace MonitoringService.Classes
{
    class Period
    {

        #region Varibale & Property 

        private Enum.EnumKindDevice kind;
        private Enum.EnumStatusPeriod status;
        private string start;
        private int dev_no;

        public Enum.EnumKindDevice Kind
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

        public Enum.EnumStatusPeriod Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        public string Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value;
            }
        }

        public int Dev_no
        {
            get
            {
                return dev_no;
            }
            set
            {
                dev_no = value;
            }
        }

        #endregion

        public Period(Enum.EnumKindDevice c_kind, Enum.EnumStatusPeriod c_status, int c_dev_no)
        {

            Kind = c_kind;
            Dev_no = c_dev_no;
            
            Status = c_status;

            Select_Method();
        }

        private void Select_Method()
        {

            switch (Kind)
            {

                #region Autoclav

                case Enum.EnumKindDevice.autoclav:
                    if (Status == Enum.EnumStatusPeriod.start)
                    {
                        PeriodAutoclavStart();
                    }

                    else if (Status == Enum.EnumStatusPeriod.naghes)
                    {
                        PeriodAutoclavNaghes(Dev_no);
                    }

                    else if (Status == Enum.EnumStatusPeriod.end)
                    {
                        PeriodAutoclavEnd(Dev_no);
                    }
                    break;

                #endregion

                #region Baking 

                case Enum.EnumKindDevice.baking:
                    if (Status == Enum.EnumStatusPeriod.start)
                    {
                        PeriodBakingStart();
                    }

                    else if (Status == Enum.EnumStatusPeriod.naghes)
                    {
                        PeriodBakingNaghes(Dev_no);
                    }

                    else if (Status == Enum.EnumStatusPeriod.end)
                    {
                        PeriodBakingEnd(Dev_no);
                    }
                    break;

                #endregion
            }
        }


        #region Autoclav

        public bool PeriodAutoclavStart()
        {
            try
            {
                string timeStartAutoclav = PublicMehotd.time_php();

                //MessageBox.Show((Convert.ToInt32(timeStartAutoclav) - Varibale.Timer_Autoclav[Dev_no]).ToString() + " X " + timeStartAutoclav);

                Varibale.Status_Autoclav[Dev_no] = (Convert.ToInt32(timeStartAutoclav) - Varibale.Timer_Autoclav[Dev_no]).ToString();

                SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();
                SqlCommand cmd = ConnectToDatabase.NewSqlCommand(System.Data.CommandType.StoredProcedure, "Insert_start_New_Period", cn);

                cmd.Parameters.AddWithValue("dev_no", Dev_no);

                cmd.Parameters.AddWithValue("status", Enum.EnumStatusPeriod.start.ToString());

                cmd.Parameters.AddWithValue("kind", Enum.EnumKindDevice.autoclav.ToString());

                cmd.Parameters.AddWithValue("intTimestart", (Convert.ToInt32(timeStartAutoclav) - Varibale.Timer_Autoclav[Dev_no]).ToString());

                cmd.Parameters.AddWithValue("intTimeend", 0);

                cmd.Parameters.AddWithValue("timestart", PublicMehotd.RetStringLocalTime(-1 * Varibale.Timer_Autoclav[Dev_no]));

                cmd.Parameters.AddWithValue("timeend", "00:00:00");

                cmd.Parameters.AddWithValue("date", PublicMehotd.RetStringPersianCalender());

                cmd.Parameters.AddWithValue("year", PublicMehotd.retStringYearPersian());

                cmd.Parameters.AddWithValue("month", PublicMehotd.retStringMonthPersian());

                cmd.Parameters.AddWithValue("day", PublicMehotd.retStringDayPersian());

                cmd.Parameters.AddWithValue("intTimezero", "0");

                cmd.Parameters.AddWithValue("timezero", "00:00:00");

                cmd.Parameters.AddWithValue("time_st", Varibale.Data_Timer_Max[Dev_no].ToString());

                cmd.Parameters.AddWithValue("temp_low_st", Varibale.Data_Temp_Min[Dev_no]);

                cmd.Parameters.AddWithValue("temp_high_st", Varibale.Data_Temp_Max[Dev_no]);

                cmd.Parameters.AddWithValue("pres_st", Varibale.Data_Pres_Min[Dev_no]);

                cn.Open();
                object obj = cmd.ExecuteScalar();
                cn.Close();

                return true;
            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "period-start-autoclav");
                Enum.ClassEnumError.ShowErrorInMessageBox(Enum.EnumError.insertnewstartautoclavinsertdatabaseerror);
                return false;
            }
        }

        public static bool PeriodAutoclavZeroTimer(int deviceNumber)
        {

            try
            {
                SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();
                SqlCommand cmd = ConnectToDatabase.NewSqlCommand(System.Data.CommandType.StoredProcedure, "Update_Zero_Autoclav", cn);

                cmd.Parameters.AddWithValue("intTimeStart",Convert.ToInt32( Varibale.Status_Autoclav[deviceNumber]));

                cmd.Parameters.AddWithValue("intTimezero", PublicMehotd.time_php());

                cmd.Parameters.AddWithValue("timezero",PublicMehotd.RetStringLocalTime());

                cmd.Parameters.AddWithValue("kind",Enum.EnumKindDevice.autoclav.ToString());

                cmd.Parameters.AddWithValue("dev_no", deviceNumber);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                     
                return true;
            }
            catch(Exception ex)
            {
                log_system.saveLogSystem(ex, "period-time zero autoclav");
                Enum.ClassEnumError.ShowErrorInMessageBox(Enum.EnumError.updatePeriodAutoclavZeroError);
                return false;
            }
        }

        // zakhire sefr shodan timer zamani ke shoro be khonak kardan mishe 
        public static bool PeriodAutoclavZeroTimerWithTemp(int deviceNumber)
        {

            try
            {
                SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();
                SqlCommand cmd = ConnectToDatabase.NewSqlCommand(System.Data.CommandType.StoredProcedure, "Update_Zero_Autoclav_Temp", cn);

                cmd.Parameters.AddWithValue("intTimeStart", Convert.ToInt32(Varibale.Status_Autoclav[deviceNumber]));

                cmd.Parameters.AddWithValue("intTimezero", PublicMehotd.time_php());

                cmd.Parameters.AddWithValue("timezero", PublicMehotd.RetStringLocalTime());

                cmd.Parameters.AddWithValue("kind", Enum.EnumKindDevice.autoclav.ToString());

                cmd.Parameters.AddWithValue("dev_no", deviceNumber);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

              

                return true;
            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "period-zero-temp autoclav with temp");
                Enum.ClassEnumError.ShowErrorInMessageBox(Enum.EnumError.updatePeriodAutoclavZeroError);
                return false;
            }
        }


        public static bool PeriodAutoclavNaghes(int deviceNumber)
        {
            try
            {
                if (Varibale.Status_Autoclav[deviceNumber] != "")
                {
                    SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();
                    SqlCommand cmd = ConnectToDatabase.NewSqlCommand(System.Data.CommandType.StoredProcedure, "Update_End_autoclav", cn);

                    cmd.Parameters.AddWithValue("intTimeStart", Varibale.Status_Autoclav[deviceNumber]);

                    cmd.Parameters.AddWithValue("kind", Enum.EnumKindDevice.autoclav.ToString());

                    cmd.Parameters.AddWithValue("dev_no", deviceNumber);

                    cmd.Parameters.AddWithValue("intTimeend", PublicMehotd.time_php());

                    cmd.Parameters.AddWithValue("timeend", PublicMehotd.RetStringLocalTime());

                    cmd.Parameters.AddWithValue("status", Enum.EnumStatusPeriod.naghes.ToString());

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();

                   
                    

                    Varibale.Status_Autoclav[deviceNumber] = "";

                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "period-autoclav naghes");
                Enum.ClassEnumError.ShowErrorInMessageBox(Enum.EnumError.updatePeriodAutoclavEndNaghes);
                return false;
            }
        }

        public static bool PeriodAutoclavEnd(int DeviceNumber)
        {
            try
            {

                if (Varibale.Status_Autoclav[DeviceNumber] != "")
                {
                    SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();
                    SqlCommand cmd = ConnectToDatabase.NewSqlCommand(System.Data.CommandType.StoredProcedure, "Update_End_autoclav", cn);

                    cmd.Parameters.AddWithValue("intTimeStart", Varibale.Status_Autoclav[DeviceNumber]);

                    cmd.Parameters.AddWithValue("kind", Enum.EnumKindDevice.autoclav.ToString());

                    cmd.Parameters.AddWithValue("dev_no", DeviceNumber);

                    cmd.Parameters.AddWithValue("intTimeend", PublicMehotd.time_php());

                    cmd.Parameters.AddWithValue("timeend", PublicMehotd.RetStringLocalTime());

                    cmd.Parameters.AddWithValue("status", Enum.EnumStatusPeriod.end.ToString());

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    
                    Varibale.Status_Autoclav[DeviceNumber] = "";

                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "period-autoclav-end");
                Enum.ClassEnumError.ShowErrorInMessageBox(Enum.EnumError.updatePeriodAutoclavEndError);
                return false;
            }
        }

        #endregion


        #region Baking

        public bool PeriodBakingStart()
        {
            try
            {
                string timeStartBaking = PublicMehotd.time_php();

                Varibale.Status_Baking[Dev_no] = timeStartBaking.ToString();

                SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();
                SqlCommand cmd = ConnectToDatabase.NewSqlCommand(System.Data.CommandType.StoredProcedure, "Insert_start_New_Period", cn);

                cmd.Parameters.AddWithValue("dev_no", Dev_no);

                cmd.Parameters.AddWithValue("status", Enum.EnumStatusPeriod.start.ToString());

                cmd.Parameters.AddWithValue("kind", Enum.EnumKindDevice.baking.ToString());

                cmd.Parameters.AddWithValue("intTimestart", timeStartBaking);

                cmd.Parameters.AddWithValue("intTimeend", 0);

                cmd.Parameters.AddWithValue("timestart", PublicMehotd.RetStringLocalTime());

                cmd.Parameters.AddWithValue("timeend", "00:00:00");

                cmd.Parameters.AddWithValue("date", PublicMehotd.RetStringPersianCalender());

                cmd.Parameters.AddWithValue("year", PublicMehotd.retStringYearPersian());

                cmd.Parameters.AddWithValue("month", PublicMehotd.retStringMonthPersian());

                cmd.Parameters.AddWithValue("day", PublicMehotd.retStringDayPersian());

                cmd.Parameters.AddWithValue("intTimezero", "0");

                cmd.Parameters.AddWithValue("timezero", "00:00:00");

                cn.Open();
                object obj = cmd.ExecuteScalar();
                cn.Close();


               

                return true;
            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "period-baking-start");
                Enum.ClassEnumError.ShowErrorInMessageBox(Enum.EnumError.insertnewstartautoclavinsertdatabaseerror);
                return false;
            }
        }

        public static bool PeriodBakingZeroTimer(int devicenumber)
        {
            try
            {
                SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();
                SqlCommand cmd = ConnectToDatabase.NewSqlCommand(System.Data.CommandType.StoredProcedure, "Update_Zero_Autoclav", cn);

                cmd.Parameters.AddWithValue("intTimeStart", Convert.ToInt32(Varibale.Status_Baking[devicenumber]));

                cmd.Parameters.AddWithValue("intTimezero", PublicMehotd.time_php());

                cmd.Parameters.AddWithValue("timezero", PublicMehotd.RetStringLocalTime());

                cmd.Parameters.AddWithValue("kind", Enum.EnumKindDevice.baking.ToString());

                cmd.Parameters.AddWithValue("dev_no", devicenumber);

                cn.Open();
                object obj = cmd.ExecuteScalar();
                cn.Close();

               


                return true;
            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "period-baking-zero timer");
                Enum.ClassEnumError.ShowErrorInMessageBox(Enum.EnumError.updatePeriodAutoclavZeroError);
                return false;
            }
        }

        public static bool PeriodBakingNaghes(int devicenumber)
        {
            try
            {

                SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();
                SqlCommand cmd = ConnectToDatabase.NewSqlCommand(System.Data.CommandType.StoredProcedure, "Update_End_autoclav", cn);

                cmd.Parameters.AddWithValue("intTimeStart", Varibale.Status_Baking[devicenumber]);

                cmd.Parameters.AddWithValue("kind", Enum.EnumKindDevice.baking.ToString());

                cmd.Parameters.AddWithValue("dev_no", devicenumber);

                cmd.Parameters.AddWithValue("intTimeend", PublicMehotd.time_php());

                cmd.Parameters.AddWithValue("timeend", PublicMehotd.RetStringLocalTime());

                cmd.Parameters.AddWithValue("status", Enum.EnumStatusPeriod.naghes.ToString());

                cn.Open();
                object obj = cmd.ExecuteScalar();
                cn.Close();

               

                Varibale.Status_Baking[devicenumber] = "";

                return true;
            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "period-baking-naghes");
                Enum.ClassEnumError.ShowErrorInMessageBox(Enum.EnumError.updatePeriodAutoclavEndNaghes);
                return false;
            }
        }

        public static bool PeriodBakingEnd(int devicenumber)
        {
            try
            {


                SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();
                SqlCommand cmd = ConnectToDatabase.NewSqlCommand(System.Data.CommandType.StoredProcedure, "Update_End_autoclav", cn);

                cmd.Parameters.AddWithValue("intTimeStart", Varibale.Status_Baking[devicenumber]);

                cmd.Parameters.AddWithValue("kind", Enum.EnumKindDevice.baking.ToString());

                cmd.Parameters.AddWithValue("dev_no", devicenumber);

                cmd.Parameters.AddWithValue("intTimeend", PublicMehotd.time_php());

                cmd.Parameters.AddWithValue("timeend", PublicMehotd.RetStringLocalTime());

                cmd.Parameters.AddWithValue("status", Enum.EnumStatusPeriod.end.ToString());

                cn.Open();
                object obj = cmd.ExecuteScalar();
                cn.Close();


               

                Varibale.Status_Baking[devicenumber] = "";

                return true;
            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "period-baking-end");
                Enum.ClassEnumError.ShowErrorInMessageBox(Enum.EnumError.updatePeriodAutoclavEndError);
                return false;
            }
        }

        #endregion
    }
}
