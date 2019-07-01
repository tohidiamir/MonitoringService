using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace MonitoringService.Classes
{
    class PublicMehotd
    {
        #region Time() similar TIme in Php

        public static string time_php()
        {
            return ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString();
        }

        #endregion

        #region Convert Array Hexadecimal To Float

        public static float hextoFloat(string hex)
        {

            float dataRet = new float();
            float hex10 = Int16.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            dataRet = float.Parse(hex10.ToString()) / 10;

            return dataRet;
        }//convert hex to float 

        #endregion

        #region Show int To H : M : D

        public static string show_time(int intNo)
        {

            int H = intNo / 3600;
            int modH = intNo - (H * 3600);
            int M = modH / 60;
            int modM = modH - (M * 60);
            int S = modM;

            return H.ToString("00") + ":" + M.ToString("00") + ":" + S.ToString("00");

        }//end func shwo time

        #endregion



        #region Check Number

        public static bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle, System.Globalization.CultureInfo.CurrentCulture, out result);
        }


        #endregion 

        #region Check Number With Minus
        public static bool isNumericWithMinus(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Decimal result;
            return Decimal.TryParse(val, NumberStyle, System.Globalization.CultureInfo.CurrentCulture, out result);
        }


        #endregion 

        #region Return 1389/02/10

        public static string RetStringPersianCalender()
        {

            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();



            string str = pc.GetYear(DateTime.Today).ToString("0000") + "/" + pc.GetMonth(DateTime.Today).ToString("00") + "/" + pc.GetDayOfMonth(DateTime.Today).ToString("00");

            return str;
        }

        #endregion

        #region Show Local Time 10:08:10

        public static string RetStringLocalTime()
        {

            return DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00");
        }

        public static string RetStringLocalTime(int second)
        {

            return DateTime.Now.AddSeconds(second).Hour.ToString("00") + ":" + DateTime.Now.AddSeconds(second).Minute.ToString("00") + ":" + DateTime.Now.AddSeconds(second).Second.ToString("00");
        }

        #endregion

        #region Show Datetime 13890210100820

        public static string retStringShowDatetime()
        {

            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();

            return pc.GetYear(DateTime.Today).ToString("0000") + pc.GetMonth(DateTime.Today).ToString("00") + pc.GetDayOfMonth(DateTime.Today).ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
        }

        #endregion

        #region Return Year persian

        public static string retStringYearPersian()
        {

            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();

            return pc.GetYear(DateTime.Today).ToString("0000");
        }

        #endregion

        #region Return Month Persian

        public static string retStringMonthPersian()
        {

            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();

            return pc.GetMonth(DateTime.Today).ToString("00");
        }

        #endregion

        #region Return Day Persian

        public static string retStringDayPersian()
        {

            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();

            return pc.GetDayOfMonth(DateTime.Today).ToString("00");
        }

        #endregion

        #region Active All Device

        public static bool ActiveAllDevice()
        {

            try
            {
                //Baking.ActiveAllBaking();
                Autoclav.ActiveAllAutoclav();
                DevOther.activeAll();
                return true;
            }
            catch (Exception ex)
            {
                Enum.ClassEnumError.ShowErrorInMessageBox(Enum.EnumError.ProblemDeactiveAll);
                //MessageBox.Show(ex.Message);
                log_system.saveLogSystem(ex, "active-all-device");
                return false;
            }


        }

        #endregion

        #region Deactive All Device

        public static bool DeactiveAllDevice()
        {
            try
            {
                //Baking.DeactiveAllBaking();
                Autoclav.DeactiveAllAutoclav();
                DevOther.deactiveAll();
                return true;
            }
            catch (Exception ex)
            {
                Enum.ClassEnumError.ShowErrorInMessageBox(Enum.EnumError.ProblemDeactiveAll);
                //MessageBox.Show(ex.Message);
                log_system.saveLogSystem(ex, "deactive-all-device");
                return false;
            }
        }

        #endregion

        #region Convert 0 - 12 to name 

        public static string RetStringConvertIntToNameMonth(int Month)
        {

            switch (Month)
            {
                case 1:
                    return "فروردین";

                case 2:
                    return "اردیبهشت";

                case 3:
                    return "خرداد";

                case 4:
                    return "تیر";

                case 5:
                    return "مرداد";

                case 6:
                    return "شهریور";

                case 7:
                    return "مهر";

                case 8:
                    return "آبان";

                case 9:
                    return "آذر";

                case 10:
                    return "دی";

                case 11:
                    return "بهمن";

                case 12:
                    return "اسفند";

                default:
                    return "";
            }
        }

        #endregion 

        #region Check Char is digit

        public static bool checkCharIsDigit(char x)
        {
            if (char.IsDigit(x))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion

        #region Convert Date & time to 13890210221031

        public static string RetStringDateAndTimeTo(string year, string month, string day, string hour, string minute, string second)
        {
            return year + month + day + hour + minute + second;
        }

        #endregion

        #region 13890210221031 -

        public static string RetStringDateAndTimeToAdd(string dat, int add)
        {
            //if (isNumeric(dat, System.Globalization.NumberStyles.Integer))
            //{
            //    long dat_int = Convert.ToInt64(dat);

            //    string date = dat.Substring(0 ,8);

            //    string sec = dat.Substring(12, 2);
            //    int sec_int = Convert.ToInt32(sec);



            //    string min = dat.Substring(10, 2);
            //    int min_int = Convert.ToInt32(min);

            //    string hour = dat.Substring(8, 2);
            //    int hour_int = Convert.ToInt32(hour);

            //    sec_int -= add;

            //    if ((sec_int < 60) && (sec_int >= 0))
            //    {
            //        return date + hour + min +sec_int.ToString();
            //    }
            //    else if(sec_int > 59)
            //    {
            //        sec_int -= 60;
            //        min_int += 1;
            //        if((min_int >) )
            //    }
            //    else if (sec_int < 0)
            //    { 

            //    }


            //}

            //else
            return dat;
        }

        #endregion

        #region Status Period Convert To persian

        public static string retStringStatusPeriodToPersian(string st)
        {
            switch (st.Trim())
            {
                case "end":
                    return "کامل ";

                case "start":
                    return "ناتمام";

                case "naghes":
                    return "ناقص ";
            }
            return "نامعلوم";
        }

        #endregion

        #region Convert String To enumKind

        public static Enum.EnumKindDevice RetStringConvertToEnumKind(string kind)
        {
            switch (kind)
            {
                case "autoclav":
                    return Enum.EnumKindDevice.autoclav;

                case "baking":
                    return Enum.EnumKindDevice.baking;

                case "frig_1":
                    return Enum.EnumKindDevice.frig_1;

                case "frig_2":
                    return Enum.EnumKindDevice.frig_2;

                case "oil":
                    return Enum.EnumKindDevice.oil;

                case "system":
                    return Enum.EnumKindDevice.system;

                default:
                    return Enum.EnumKindDevice.system;
            }
        }

        #endregion

        #region Convert String to Enum Status Period 

        public static Enum.EnumStatusPeriod RetStringConvertToStatusPeriod(string st)
        {
            switch (st)
            {
                case "end":
                    return Enum.EnumStatusPeriod.end;

                case "naghes":
                    return Enum.EnumStatusPeriod.naghes;

                case "start":
                    return Enum.EnumStatusPeriod.start;

                default:
                    return Enum.EnumStatusPeriod.start;
            }
        }

        #endregion

        #region Connect Device Serial Port

        public static bool ConnectToDeviceSerialPort()
        {

            try
            {

                if (CreateSerialPort.serialPort_1.IsOpen)
                {
                    //ShowMessage.Text = "ارتباط برقرار است.";
                    return false;
                }
                else if (CreateSerialPort.OpenSerialPort_1())
                {
                    //ManageTimer.tmrFromDevice.Start();
                    //ShowMessage.Text = "اتصال با موفقیت انجام شد";
                    //frmIndex.st_lbl_comment.Text = "ارتباط برقرار است";
                    return true;
                }
                else
                {
                    //ShowMessage.Text = "قادر به برقراری ارتباط نیست";
                    return false;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //MessageBox.Show(ex.Source);
                //MessageBox.Show(ex.TargetSite.ToString());
                //MessageBox.Show(ex.StackTrace);
                log_system.saveLogSystem(ex, "connect to device serial port");

                return false;
            }
        }

        #endregion

        #region Disconnect Device Serial Port 

        public static bool DisconnectToDeviceSerialPort()
        {

            if (CreateSerialPort.CloseSerialPort_1())
            {
                //ManageTimer.tmrFromDevice.Stop();
                //ShowMessage.Text = "قطع اتصال با موفقیت انجام شد";
                //frmIndex.st_lbl_comment.Text = "عدم ارتباط";
                PublicMehotd.DeactiveAllDevice();
                return true;

            }
            else
            {
                //ShowMessage.Text = "قادر به قطع اتصال نیست";
                return false;
            }
        }

        #endregion

        #region Convert TO parit

        public static System.IO.Ports.Parity ConvertStringToParity(string par)
        {
            switch (par)
            {
                case "Even":
                    return System.IO.Ports.Parity.Even;

                case "Mark":
                    return System.IO.Ports.Parity.Mark;

                case "None":
                    return System.IO.Ports.Parity.None;

                case "Odd":
                    return System.IO.Ports.Parity.Odd;

                case "Space":
                    return System.IO.Ports.Parity.Space;

                default:
                    return System.IO.Ports.Parity.None;
            }
        }

        #endregion

        #region Convert String kind to name kind persian

        public static string ConvertEnumStringTOpersianName(string name)
        {
            Enum.EnumKindDevice kind = PublicMehotd.RetStringConvertToEnumKind(name.Trim());

            switch (kind)
            {
                case Enum.EnumKindDevice.autoclav:
                    return "اتوکلاو";

                case Enum.EnumKindDevice.baking:
                    return "پخت";

                case Enum.EnumKindDevice.frig_1:
                    return "دماسنج 1";

                case Enum.EnumKindDevice.frig:
                    return "دماسنج ";

                case Enum.EnumKindDevice.frig_2:
                    return "دماسنج 2";

                case Enum.EnumKindDevice.oil:
                    return "روغن ریز";

                default:
                    return "سیستم";
            }
        }

        #endregion


        public static byte LRC(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("Tham số truyền vào không tồn tại phần tử nào");
            byte lrc = 0;
            foreach (byte b in data)
                lrc += b;
            lrc = (byte)((lrc ^ 0xFF) + 1);
            return lrc;
        }

        public static Int64 convertHextoInt(string hex)
        {
            Int64 hex10 = Int64.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            return hex10;
        }


        public static long fetchData(int devicenumber, string type, string datep)
        {
            string sql = "select top 1* from Data where device_no = '" + devicenumber + "' and kind='" + Enum.EnumKindDevice.counter.ToString() + "' and datep='" + datep + "' ORDER BY intTime " + type + " ";
            SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();
            SqlCommand cmd = ConnectToDatabase.NewSqlCommand(System.Data.CommandType.Text,
                sql, cn);

            //MessageBox.Show(sql);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return Int64.Parse(dr["value"].ToString());

            }
            else
            {
                return 0;
            }



            cn.Close();
        }


        public static bool SetDataToDatabase(string name, string value)
        {
            try
            {
                SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();
                SqlCommand cmd = ConnectToDatabase.NewSqlCommand(CommandType.Text, "update Master_variable set value='" + value + "' where name='" + name + "' ", cn);

                cn.Open();


                cmd.ExecuteNonQuery();

                cn.Close();

                return true;

            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "");
                return false;
            }
        }

        public static string RetStringPersianCalenderwithDash()
        {
            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();

            string str = pc.GetYear(DateTime.Today).ToString("0000") + "-" + pc.GetMonth(DateTime.Today).ToString("00") + "-" + pc.GetDayOfMonth(DateTime.Today).ToString("00");

            return str;
        }

    }
}
