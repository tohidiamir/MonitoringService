using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace MonitoringService.Classes
{
    class Autoclav
    {
        public static int Save_data = 0;
        public static int sendServer_count = 0;
        public static int errorCount;

        #region read from dt3

        public static void ReadFromAutoclav_old()
        {
            if (Save_data == Varibale.Save_count)
            {
                Save_data = 0;

            }
            else
            {
                Save_data++;
            }


            try
            {

                while (Varibale.i_for_autoclav < ((Varibale.totalAutoclav - 1) * 2) && Varibale.devicenumber_autoclav < Varibale.totalAutoclav)
                {
                    if (Varibale.Active_autoclav[Varibale.devicenumber_autoclav])
                    {
                        Varibale.AutoclavdeviceProblem = Varibale.devicenumber_autoclav;

                        String readerdatafromserialport = ReadData(Varibale.i_for_autoclav);

                        if (readerdatafromserialport != "amir")
                        {
                            float fRedValue = PublicMehotd.hextoFloat(readerdatafromserialport);

                            //adad red gerefte shode
                            if (Varibale.i_for_autoclav % 2 == 0)
                            {
                                Varibale.Data_Temp_autoclav[Varibale.devicenumber_autoclav] = fRedValue;
                                Autoclav_Check_Temp(Varibale.devicenumber_autoclav, Varibale.Data_Temp_autoclav[Varibale.devicenumber_autoclav]);
                            }
                            else
                            {
                                Varibale.Data_Pres_autoclav[Varibale.devicenumber_autoclav] = fRedValue;
                                Autoclav_Check_Pres(Varibale.devicenumber_autoclav, Varibale.Data_Pres_autoclav[Varibale.devicenumber_autoclav]);
                            }


                            if (Varibale.i_for_autoclav % 2 == 1)
                            {

                                if (Varibale.Data_Pres_autoclav[Varibale.devicenumber_autoclav] > Varibale.Autoclav_Pres_Low)
                                {

                                    if (Save_data == 0)
                                    {
                                        InsertToDatabase(Varibale.devicenumber_autoclav, Enum.EnumTypeData.pres, Enum.EnumKindDevice.autoclav, Varibale.Data_Pres_autoclav[Varibale.devicenumber_autoclav]);
                                        InsertToDatabase(Varibale.devicenumber_autoclav, Enum.EnumTypeData.temp, Enum.EnumKindDevice.autoclav, Varibale.Data_Temp_autoclav[Varibale.devicenumber_autoclav]);
                                    }


                                }

                                Autoclav_check_pbx_timer(Varibale.devicenumber_autoclav);
                                Autoclav_Check_For_Timer(Varibale.devicenumber_autoclav);

                                //Varibale.lst_autoclav_temp[Varibale.devicenumber_autoclav - 1].Text = Varibale.Data_Temp_autoclav[Varibale.devicenumber_autoclav].ToString("00.0");
                                //Varibale.lst_autoclav_pres[Varibale.devicenumber_autoclav - 1].Text = Varibale.Data_Pres_autoclav[Varibale.devicenumber_autoclav].ToString("00.0");
                                //Varibale.lst_autoclav_timer[Varibale.devicenumber_autoclav - 1].Text = PublicMehotd.show_time(Varibale.Timer_Autoclav[Varibale.devicenumber_autoclav]);

                                Varibale.Error_autoclav[Varibale.devicenumber_autoclav - 1] = 0;
                                Varibale.devicenumber_autoclav++;
                            }

                        }
                        else
                        {
                            Varibale.devicenumber_autoclav++;
                            Varibale.i_for_autoclav++;
                        }
                    }
                    else
                    {
                        Varibale.devicenumber_autoclav++;
                        Varibale.i_for_autoclav++;
                    }


                    Varibale.i_for_autoclav++;
                }//End while

                Varibale.i_for_autoclav = 0;
                Varibale.devicenumber_autoclav = 1;
            }
            catch (Exception ex)
            {
                Varibale.Error_autoclav[Varibale.devicenumber_autoclav - 1]++;

                if (Varibale.Error_autoclav[Varibale.devicenumber_autoclav - 1] > 4)
                {
                    LogManager log = new LogManager(Varibale.devicenumber_autoclav, Enum.EnumKindDevice.autoclav, Enum.EnumLevel.urgent, Enum.EnumLOg.Autoclav_Connection_error, "*");
                    DeactiveAutoclav(Varibale.devicenumber_autoclav);
                }
                else if (Varibale.Error_autoclav[Varibale.devicenumber_autoclav - 1] > 3)
                {
                    //Varibale.lst_autoclav_temp[Varibale.devicenumber_autoclav - 1].Text = Varibale.Error_autoclav[Varibale.devicenumber_autoclav - 1].ToString() + ".خطا.";
                    LogManager log = new LogManager(Varibale.devicenumber_autoclav, Enum.EnumKindDevice.autoclav, Enum.EnumLevel.high, Enum.EnumLOg.Autoclav_Connection_error, "خطا شماره " + Varibale.Error_autoclav[Varibale.devicenumber_autoclav - 1]);
                }
                else
                {
                    //Varibale.lst_autoclav_temp[Varibale.devicenumber_autoclav - 1].Text = "--.-";
                }


                if (Varibale.i_for_autoclav % 2 == 1)
                {
                    Varibale.devicenumber_autoclav++;
                    Varibale.i_for_autoclav++;

                }
                else
                {
                    Varibale.devicenumber_autoclav++;
                    Varibale.i_for_autoclav++;
                    Varibale.i_for_autoclav++;
                }

                //MessageBox.Show(ex.Message);
                //Enum.ClassEnumError.ShowErrorInMessageBox(Enum.EnumError.readFromAutoclacProblem);
                log_system.saveLogSystem(ex, "autoclav-error-read");

            }
        }


        public static string ReadData(int i)
        {

            CreateSerialPort.serialPort_1.Write(DeviceString.WriteDevice[i], 0, DeviceString.WriteDevice[i].Length);
            string readSTR = CreateSerialPort.serialPort_1.ReadLine();
            char[] arraySTR = readSTR.ToCharArray();

            #region CharArray To string Data From Device

            string address = arraySTR[1].ToString() + arraySTR[2].ToString();
            string reder = arraySTR[7].ToString() + arraySTR[8].ToString() + arraySTR[9].ToString() + arraySTR[10].ToString();
            string greener = arraySTR[11].ToString() + arraySTR[12].ToString() + arraySTR[13].ToString() + arraySTR[14].ToString();

            int add = Int16.Parse(address, System.Globalization.NumberStyles.HexNumber);
            //MessageBox.Show(add.ToString() + "=" + (i + 1).ToString());
            #endregion

            if (add.ToString() == (i + 1).ToString())
            {
                return reder;
            }
            else
            {
                return "amir";
            }
        }
        #endregion


        public static void ReadFromAutoclav()
        {
            if (Save_data == Varibale.Save_count)
            {
                Save_data = 0;

            }
            else
            {
                Save_data++;
            }


            try
            {
                string x = PLC.Read("0403119A0027", "readautoclav");
                if (x != "")
                {
                    errorCount = 0;

                    zarin_autoclav autozarin = new zarin_autoclav();
                    autozarin.proccessData(x);


                    if (Varibale.devicenumber_autoclav >= Varibale.totalAutoclav)
                    {
                        Varibale.devicenumber_autoclav = 1;
                    }

                    while (Varibale.devicenumber_autoclav <= Varibale.totalAutoclav)
                    {

                        if (Varibale.Active_autoclav[Varibale.devicenumber_autoclav])
                        {
                            Varibale.AutoclavdeviceProblem = Varibale.devicenumber_autoclav;
                            Autoclav.setDataForAutoclav(Save_data);
                            Varibale.devicenumber_autoclav++;

                        }
                        else
                        {
                            Varibale.devicenumber_autoclav++;
                        }


                    }//End while
                }
                else
                {
                    if (errorCount % 10 == 0)
                    {
                        LogManager loger = new LogManager(0, Enum.EnumKindDevice.autoclav, Enum.EnumLevel.high, Enum.EnumLOg.Autoclav_Connection_error, (errorCount / 10).ToString() + " خطا");
                    }


                    errorCount++;
                }


                Varibale.devicenumber_autoclav = 1;
            }
            catch (Exception ex)
            {
                if (Varibale.devicenumber_autoclav > Varibale.totalAutoclav)
                {
                    Varibale.devicenumber_autoclav = 1;
                }

                Varibale.Error_autoclav[Varibale.devicenumber_autoclav - 1]++;

                if (Varibale.Error_autoclav[Varibale.devicenumber_autoclav - 1] > 4)
                {
                    LogManager log = new LogManager(Varibale.devicenumber_autoclav, Enum.EnumKindDevice.autoclav, Enum.EnumLevel.urgent, Enum.EnumLOg.Autoclav_Connection_error, "*");
                    DeactiveAutoclav(Varibale.devicenumber_autoclav);
                }
                else if (Varibale.Error_autoclav[Varibale.devicenumber_autoclav - 1] > 3)
                {
                    //Varibale.lst_autoclav_temp[Varibale.devicenumber_autoclav - 1].Text = Varibale.Error_autoclav[Varibale.devicenumber_autoclav - 1].ToString() + ".خطا.";
                    LogManager log = new LogManager(Varibale.devicenumber_autoclav, Enum.EnumKindDevice.autoclav, Enum.EnumLevel.high, Enum.EnumLOg.Autoclav_Connection_error, "خطا شماره " + Varibale.Error_autoclav[Varibale.devicenumber_autoclav - 1]);
                }
                else
                {
                    //Varibale.lst_autoclav_temp[Varibale.devicenumber_autoclav - 1].Text = "--.-";
                }


                Varibale.devicenumber_autoclav++;


                //MessageBox.Show(ex.Message);
                //Enum.ClassEnumError.ShowErrorInMessageBox(Enum.EnumError.readFromAutoclacProblem);
                log_system.saveLogSystem(ex, "autoclav-error-read");

            }
        }

        private static void setDataForAutoclav(int savedata)
        {
            try
            {
                Autoclav_Check_Temp(Varibale.devicenumber_autoclav, Varibale.Data_Temp_autoclav[Varibale.devicenumber_autoclav]);

                Autoclav_Check_Pres(Varibale.devicenumber_autoclav, Varibale.Data_Pres_autoclav[Varibale.devicenumber_autoclav]);


                if (Varibale.Data_Pres_autoclav[Varibale.devicenumber_autoclav] >= Varibale.Data_Pres_Min[Varibale.devicenumber_autoclav])
                {

                    if (Save_data == 0)
                    {
                        InsertToDatabase(Varibale.devicenumber_autoclav, Enum.EnumTypeData.pres, Enum.EnumKindDevice.autoclav, Varibale.Data_Pres_autoclav[Varibale.devicenumber_autoclav]);
                        InsertToDatabase(Varibale.devicenumber_autoclav, Enum.EnumTypeData.temp, Enum.EnumKindDevice.autoclav, Varibale.Data_Temp_autoclav[Varibale.devicenumber_autoclav]);
                    }


                }

                Autoclav_check_pbx_timer(Varibale.devicenumber_autoclav);
                Autoclav_Check_For_Timer(Varibale.devicenumber_autoclav);

                //Varibale.lst_autoclav_temp[Varibale.devicenumber_autoclav - 1].Text = Varibale.Data_Temp_autoclav[Varibale.devicenumber_autoclav].ToString("00.0");
                //Varibale.lst_autoclav_pres[Varibale.devicenumber_autoclav - 1].Text = Varibale.Data_Pres_autoclav[Varibale.devicenumber_autoclav].ToString("00.0");

                //Varibale.lst_autoclav_timer[Varibale.devicenumber_autoclav - 1].Text = PublicMehotd.show_time(Varibale.Timer_Autoclav[Varibale.devicenumber_autoclav]);

                Varibale.Error_autoclav[Varibale.devicenumber_autoclav - 1] = 0;


            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "autoclav-error-read-plc");
            }

        }




        public static bool InsertToDatabase(int devicenumber, Enum.EnumTypeData type, Enum.EnumKindDevice kind, double value)
        {
            try
            {
                Boolean check = true;

                if (check)
                {
                    SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();

                    SqlCommand cmd = ConnectToDatabase.NewSqlCommand(System.Data.CommandType.StoredProcedure, "Insert_Data_new", cn);

                    cmd.Parameters.AddWithValue("dev_no", devicenumber);
                    cmd.Parameters.AddWithValue("type", type.ToString());
                    cmd.Parameters.AddWithValue("kind", kind.ToString());
                    cmd.Parameters.AddWithValue("value", value);
                    cmd.Parameters.AddWithValue("intTime", PublicMehotd.time_php());
                    cmd.Parameters.AddWithValue("datep", PublicMehotd.RetStringPersianCalender());
                    cmd.Parameters.AddWithValue("timep", PublicMehotd.RetStringLocalTime());
                    cmd.Parameters.AddWithValue("yearp", PublicMehotd.retStringYearPersian());
                    cmd.Parameters.AddWithValue("monthp", PublicMehotd.retStringMonthPersian());
                    cmd.Parameters.AddWithValue("dayp", PublicMehotd.retStringDayPersian());
                    cmd.Parameters.AddWithValue("datetimep", Convert.ToDouble(PublicMehotd.retStringShowDatetime()));

                    cn.Open();
                    object obj = cmd.ExecuteScalar();
                    cn.Close();

                    if (Autoclav.sendServer_count == Varibale.save_count_autoclav_server)
                    {
                        Autoclav.sendServer_count = 0;
                    }
                    else
                    {
                        Autoclav.sendServer_count++;
                    }


                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "autoclav-insert database");
                //Enum.ClassEnumError.ShowErrorInMessageBox(Enum.EnumError.insertnewDatalogforautoclav);
                return false;
            }
        }

        public static void Autoclav_Check_Temp(int devicenumber, double value)
        {


            if ((value > Varibale.Data_Temp_Max[devicenumber]) && (Varibale.Status_Normal_Autoclav_Temp[devicenumber]))
            {
                RedLightOn(devicenumber);
                LogManager log = new LogManager(devicenumber, Enum.EnumKindDevice.autoclav, Enum.EnumLevel.medium, Enum.EnumLOg.Autoclav_Temp_High, value.ToString("00.0"));
                Varibale.Status_Normal_Autoclav_Temp[devicenumber] = false;
            }

            else if ((value > Varibale.Data_Temp_Max[devicenumber]) && !(Varibale.Status_Normal_Autoclav_Temp[devicenumber]))
            {
                RedLightOn(devicenumber);
            }
            else if ((value < Varibale.Data_Temp_Min[devicenumber]) && (Varibale.Status_Normal_Autoclav_Temp[devicenumber]))
            {
                RedLightOn(devicenumber);
                LogManager log = new LogManager(devicenumber, Enum.EnumKindDevice.autoclav, Enum.EnumLevel.medium, Enum.EnumLOg.Autoclav_Temp_Low, value.ToString("00.0"));
                Varibale.Status_Normal_Autoclav_Temp[devicenumber] = false;

            }
            else if ((value < Varibale.Data_Temp_Min[devicenumber]) && !(Varibale.Status_Normal_Autoclav_Temp[devicenumber]))
            {
                RedLightOn(devicenumber);
            }
            else if ((value >= Varibale.Data_Temp_Min[devicenumber]) && (value <= Varibale.Data_Temp_Max[devicenumber]) && !(Varibale.Status_Normal_Autoclav_Temp[devicenumber]))
            {
                RedLightOff(devicenumber);
                LogManager log = new LogManager(devicenumber, Enum.EnumKindDevice.autoclav, Enum.EnumLevel.medium, Enum.EnumLOg.Autoclav_Temp_Normal, value.ToString("00.0"));
                Varibale.Status_Normal_Autoclav_Temp[devicenumber] = true;
            }
            else if ((value >= Varibale.Data_Temp_Min[devicenumber]) && (value <= Varibale.Data_Temp_Max[devicenumber]) && (Varibale.Status_Normal_Autoclav_Temp[devicenumber]))
            {
                RedLightOff(devicenumber);
            }

            if (value < (Varibale.Data_Temp_Min[devicenumber] - 10))
            {
                RedLightInvisible(devicenumber);
            }

        }

        public static void Autoclav_Check_Pres(int devicenumber, double value)
        {

            if ((value > Varibale.Autoclav_Pres_high) && (Varibale.Status_Normal_Autoclav_Pres[devicenumber]))
            {

                LogManager log = new LogManager(devicenumber, Enum.EnumKindDevice.autoclav, Enum.EnumLevel.medium, Enum.EnumLOg.Autoclav_Pres_High, value.ToString("00.0"));
                Varibale.Status_Normal_Autoclav_Pres[devicenumber] = false;

            }

            else if ((value < Varibale.Data_Pres_Min[devicenumber]) && (Varibale.Status_Normal_Autoclav_Pres[devicenumber]))
            {

                LogManager log = new LogManager(devicenumber, Enum.EnumKindDevice.autoclav, Enum.EnumLevel.medium, Enum.EnumLOg.Autoclav_Pres_Low, value.ToString("00.0"));
                Varibale.Status_Normal_Autoclav_Pres[devicenumber] = false;
            }

            else if ((value >= Varibale.Data_Pres_Min[devicenumber]) && (value <= Varibale.Autoclav_Pres_high) && !(Varibale.Status_Normal_Autoclav_Pres[devicenumber]))
            {

                LogManager log = new LogManager(devicenumber, Enum.EnumKindDevice.autoclav, Enum.EnumLevel.medium, Enum.EnumLOg.Autoclav_Pres_Normal, value.ToString("00.0"));
                Varibale.Status_Normal_Autoclav_Pres[devicenumber] = true;

            }
        }

        public static void Autoclav_Check_For_Timer(int devicenumber)
        {

            //MessageBox.Show(Varibale.Timer_Autoclav[devicenumber].ToString()+" X "+Varibale.Data_Timer_Max[devicenumber].ToString());
            //shoro be kar dastgah 


            if (!Varibale.Status_Zero_Timer_Autoclav[devicenumber] && Varibale.Autoclav_perioding[devicenumber])
            {
                #region Period Is End

                if (Varibale.Data_Temp_autoclav[devicenumber] < (Varibale.Data_Temp_Min[devicenumber] - 10) && (Varibale.Status_Zero_Temp_Timer_Autoclav[devicenumber] == true))
                {
                    Varibale.Status_Zero_Temp_Timer_Autoclav[devicenumber] = false;
                    Period.PeriodAutoclavZeroTimerWithTemp(devicenumber);
                    LogManager log = new LogManager(devicenumber, Enum.EnumKindDevice.autoclav, Enum.EnumLevel.high, Enum.EnumLOg.Autoclav_Cold, "*");
                }
                else if ((Varibale.Data_Pres_autoclav[devicenumber] < Varibale.Data_Pres_Min[devicenumber]))
                {
                    LogManager log = new LogManager(devicenumber, Enum.EnumKindDevice.autoclav, Enum.EnumLevel.medium, Enum.EnumLOg.Period_Autoclav_End, "*");
                    //Varibale.Timer_Autoclav[devicenumber] = 0;
                    Period.PeriodAutoclavEnd(devicenumber);
                    Varibale.Autoclav_perioding[devicenumber] = false;
                }
                #endregion
            }
            else if (Varibale.Timer_Autoclav[devicenumber] >= 0 && Varibale.Timer_Autoclav[devicenumber] < Varibale.Data_Timer_Max[devicenumber])
            {
                if ((Varibale.Data_Pres_autoclav[devicenumber] >= Varibale.Data_Pres_Min[devicenumber]) &&
                    (Varibale.Data_Pres_autoclav[devicenumber] <= Varibale.Autoclav_Pres_high) &&
                    (Varibale.Data_Temp_autoclav[devicenumber] >= Varibale.Data_Temp_Min[devicenumber]) &&
                    !Varibale.Autoclav_perioding[devicenumber]
                )
                {
                    //Varibale.Timer_Autoclav[devicenumber]++;
                    LogManager log = new LogManager(devicenumber, Enum.EnumKindDevice.autoclav, Enum.EnumLevel.medium, Enum.EnumLOg.Period_Autoclav_Start, "*", (-1 * Varibale.Timer_Autoclav[devicenumber]));
                    Period per = new Period(Enum.EnumKindDevice.autoclav, Enum.EnumStatusPeriod.start, devicenumber);
                    Varibale.Autoclav_perioding[devicenumber] = true;

                }

                #region Jahata in ke tanha 1 bar set shavad log 

                if (!Varibale.Status_Zero_Timer_Autoclav[devicenumber])
                {
                    Varibale.Status_Zero_Timer_Autoclav[devicenumber] = true;
                    Varibale.Status_Zero_Temp_Timer_Autoclav[devicenumber] = true;
                    //Varibale.Timer_Autoclav[devicenumber]++;
                }
                #endregion

                if (Varibale.Data_Pres_autoclav[devicenumber] < Varibale.Data_Pres_Min[devicenumber] && Varibale.Autoclav_perioding[devicenumber])
                {
                    LogManager log = new LogManager(devicenumber, Enum.EnumKindDevice.autoclav, Enum.EnumLevel.medium, Enum.EnumLOg.Period_Autoclav_Naghes, "*");
                    Period.PeriodAutoclavNaghes(devicenumber);
                    Varibale.Autoclav_perioding[devicenumber] = false;
                }

            }

            //=================================================================================
            //
            //              Timer Is Zero 
            //
            //=================================================================================


            else if (Varibale.Timer_Autoclav[devicenumber] == Varibale.Data_Timer_Max[devicenumber] && Varibale.Autoclav_perioding[devicenumber])
            {

                #region Vaghty ke true bashad vared mishavad va log jahat sefr bodan sabt mikonad;

                if (Varibale.Status_Zero_Timer_Autoclav[devicenumber])
                {
                    Varibale.Status_Zero_Timer_Autoclav[devicenumber] = false;

                    LogManager log = new LogManager(devicenumber, Enum.EnumKindDevice.autoclav, Enum.EnumLevel.high, Enum.EnumLOg.Autoclav_Timer_Zero, "*");

                    Period.PeriodAutoclavZeroTimer(devicenumber);
                }

                #endregion








                //Varibale.Timer_Autoclav[devicenumber]++;

            }



        }

        public static void Autoclav_check_pbx_timer(int devicenumber)
        {
            if (Varibale.Timer_Autoclav[devicenumber] >= Varibale.Data_Timer_Max[devicenumber])
            {
                Autoclav_Show_pbx_timer(devicenumber);
            }
            else
            {
                Autoclav_hidden_pbx_timer(devicenumber);
            }
        }

        public static void Autoclav_Show_pbx_timer(int devicenumber)
        {
            //Varibale.lst_autoclav_show_timer[devicenumber - 1].Visible = true;

            //Varibale.lst_autoclav_hidden_timer[devicenumber - 1].Visible = false;
        }

        public static void Autoclav_hidden_pbx_timer(int devicenumber)
        {
        //    Varibale.lst_autoclav_show_timer[devicenumber - 1].Visible = false;

        //    Varibale.lst_autoclav_hidden_timer[devicenumber - 1].Visible = false;
        }


        public static void DeactiveAutoclav(int devicenumber)
        {

            Varibale.Active_autoclav[devicenumber] = false;

            if (!Varibale.Active_autoclav[devicenumber])
            {
                //Varibale.lst_autoclav_btn_active[devicenumber - 1].Text = "غیر فعال";
                //Varibale.lst_autoclav_pres[devicenumber - 1].Enabled = false;
                //Varibale.lst_autoclav_temp[devicenumber - 1].Enabled = false;
                //Varibale.lst_autoclav_timer[devicenumber - 1].Enabled = false;
                //Varibale.lst_autoclav_btn_chart[devicenumber - 1].Enabled = false;

                LogManager log = new LogManager(devicenumber, Enum.EnumKindDevice.autoclav, Enum.EnumLevel.high, Enum.EnumLOg.Autoclav_Disable, "*");
            }
        }

        public static void ActiveAutoclav(int devicenumber)
        {
            Varibale.Active_autoclav[devicenumber] = true;
            Varibale.Error_autoclav[devicenumber - 1] = 0;
            if (Varibale.Active_autoclav[devicenumber])
            {
                //Varibale.lst_autoclav_btn_active[devicenumber - 1].Text = "فعال";
                //Varibale.lst_autoclav_pres[devicenumber - 1].Enabled = true;
                //Varibale.lst_autoclav_temp[devicenumber - 1].Enabled = true;
                //Varibale.lst_autoclav_timer[devicenumber - 1].Enabled = true;
                //Varibale.lst_autoclav_btn_chart[devicenumber - 1].Enabled = true;

                LogManager log = new LogManager(devicenumber, Enum.EnumKindDevice.autoclav, Enum.EnumLevel.high, Enum.EnumLOg.Autoclav_Enable, "*");
            }
        }

        public static bool DeactiveAllAutoclav()
        {
            try
            {
                for (int i = 1; i < Varibale.totalAutoclav; i++)
                {
                    DeactiveAutoclav(i);
                }
                return true;
            }
            catch (Exception ex)
            {
                //Enum.ClassEnumError.ShowErrorInMessageBox(Enum.EnumError.ProblemDeactiveAutoclav);
                log_system.saveLogSystem(ex, "autoclav-deactive-all");
                return false;
            }
        }

        public static bool ActiveAllAutoclav()
        {
            try
            {
                for (int i = 1; i < Varibale.totalAutoclav; i++)
                {
                    ActiveAutoclav(i);
                }
                return true;
            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "autoclav-active-all");
                //Enum.ClassEnumError.ShowErrorInMessageBox(Enum.EnumError.ProblemActiveAll);
                return false;
            }
        }

        public static void RedLightOn(int devicenumber)
        {
            //Varibale.lst_autoclav_red_light[devicenumber - 1].Visible = true;
            //Varibale.lst_autoclav_green_light[devicenumber - 1].Visible = false;
        }

        public static void RedLightOff(int devicenumber)
        {
            //Varibale.lst_autoclav_red_light[devicenumber - 1].Visible = false;
            //Varibale.lst_autoclav_green_light[devicenumber - 1].Visible = true;
        }

        public static void RedLightInvisible(int devicenumber)
        {
            //Varibale.lst_autoclav_red_light[devicenumber - 1].Visible = false;
            //Varibale.lst_autoclav_green_light[devicenumber - 1].Visible = false;
        }

    }
}
