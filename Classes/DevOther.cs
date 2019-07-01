using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonitoringService.Classes;
using System.Data.SqlClient;

namespace MonitoringService.Classes
{
    class DevOther
    {

        public static int Save_data_frig = 0;

        public static void DevOtherConnect()
        {
            if (Varibale.reciveData_STATUS) {

                try
                {
                    if (Save_data_frig == Varibale.Save_count_frig)
                    {
                        Save_data_frig = 0;
                    }
                    else
                    {
                        Save_data_frig++;
                    }

                   
                    CreateSerialPort.CLOSESERIAL("DEV OTHER 1", "PLCE READ NOT CLOSE");

                    CreateSerialPort.serialPort_1.BaudRate = Varibale.buadrate1;

                    CreateSerialPort.serialPort_1.Parity = Varibale.parity1;

                    CreateSerialPort.serialPort_1.PortName = Varibale.portname1;


                    CreateSerialPort.serialPort_1.DataBits = 8;


                    if (!CreateSerialPort.serialPort_1.IsOpen)
                    {
                        CreateSerialPort.serialPort_1.Open();
                    }

                    ReadFromFrig(1);
                    ReadFromFrig(2);

                    /*
                    if (CreateSerialPort.serialPort_1.IsOpen)
                    {
                        CreateSerialPort.serialPort_1.Close();
                    }

                    CreateSerialPort.serialPort_1.Dispose();
                    */
                    CreateSerialPort.CLOSESERIAL("DEV OTHER 2", "PLCE READ NOT CLOSE");
             

            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "dev other conntent");
                    CreateSerialPort.CLOSESERIAL("DEV OTHER 2 exception ", "PLCE READ NOT CLOSE");
                }

            }
            else
            {
                CreateSerialPort.CLOSESERIAL("DEV OTHER 3 not close loop", "PLCE READ NOT CLOSE");
            }

        }

        #region Frig 1

        public static void ReadFromFrig(int devicenumber)
        {
            int number = Varibale.frig_number[devicenumber]-1;
            try
            {
                if (Varibale.Status_Active_Frig[devicenumber])
                {
                        
                        CreateSerialPort.serialPort_1.Write(DeviceString.WriteDevice[number], 0, DeviceString.WriteDevice[number].Length);
                        
                        string readSTR = CreateSerialPort.serialPort_1.ReadLine();
                        
                        char[] arraySTR = readSTR.ToCharArray();

                        string address = arraySTR[1].ToString() + arraySTR[2].ToString();
                        int nn = number + 1;
                        int add = Int16.Parse(address, System.Globalization.NumberStyles.HexNumber);


                        if (nn.ToString() == add.ToString())
                        {
                            string reder = arraySTR[7].ToString() + arraySTR[8].ToString() + arraySTR[9].ToString() + arraySTR[10].ToString();
                            string greener = arraySTR[11].ToString() + arraySTR[12].ToString() + arraySTR[13].ToString() + arraySTR[14].ToString();

                            float fReadDevice = PublicMehotd.hextoFloat(reder);

                            if (fReadDevice != 0 && fReadDevice.ToString("00.0") != "00.0")
                            {

                                Varibale.Frig_Temp[devicenumber] = fReadDevice;


                                Check_temp_frig(fReadDevice, devicenumber);
                                Varibale.Error_frig[devicenumber] = 0;

                                if (Save_data_frig == 0)
                                {
                                    InsertToDatabase(devicenumber, Enum.EnumTypeData.temp, Enum.EnumKindDevice.frig, Varibale.Frig_Temp[devicenumber]);
                                }
                            }
                        }
                    
                    
                }
            }
            catch (Exception ex)
            {
                CreateSerialPort.CLOSESERIAL("frig_error_read", "frig");
                log_system.saveLogSystem(ex, "frig-error-read");
                Varibale.Error_frig[devicenumber]++;

                if (Varibale.Error_frig[devicenumber]%10 == 0)
                {
                    LogManager log = new LogManager(devicenumber, Enum.EnumKindDevice.frig, Enum.EnumLevel.high, Enum.EnumLOg.Frig_Connection_error, "خطا شماره " + (Varibale.Error_frig[devicenumber]/10));
                    
                }

                
            }
        }

        public static void Check_temp_frig( float data , int devicenumber)
        {
            if(data > -30 && data <  30)
            {
                if ((data >= Varibale.Frig_Temp_High[devicenumber]) && Varibale.Status_Normal_Frig[devicenumber])
                {
                    Varibale.Status_Normal_Frig[devicenumber] = false;
                    LogManager log = new LogManager(devicenumber, Enum.EnumKindDevice.frig, Enum.EnumLevel.high, Enum.EnumLOg.Frig_Temp_High, data.ToString("00.0"));

               }
                else if ((data <= Varibale.Frig_Temp_Low[devicenumber]) && Varibale.Status_Normal_Frig[devicenumber])
                {
                    Varibale.Status_Normal_Frig[devicenumber] = false;
                    LogManager log = new LogManager(devicenumber, Enum.EnumKindDevice.frig, Enum.EnumLevel.urgent, Enum.EnumLOg.Frig_Temp_Low, data.ToString("00.0"));
                }
                else if ((data < Varibale.Frig_Temp_High[devicenumber]) && (data > Varibale.Frig_Temp_Low[devicenumber]) && !Varibale.Status_Normal_Frig[devicenumber])
                {
                    Varibale.Status_Normal_Frig[devicenumber] = true;
                    LogManager log = new LogManager(devicenumber, Enum.EnumKindDevice.frig, Enum.EnumLevel.medium, Enum.EnumLOg.Frig_Temp_Normal, data.ToString("00.0"));
                }
            }
        }


        #endregion

        #region Oil 

        public static void ReadFromOil()
        {
            int number = Varibale.oil_number-1;
            try
            {
                if (Varibale.Status_Active_Oil)
                {
                    //MessageBox.Show(number.ToString());
                    CreateSerialPort.serialPort_1.Write(DeviceString.WriteDevice[number], 0, DeviceString.WriteDevice[number].Length);
                    string readSTR = CreateSerialPort.serialPort_1.ReadLine();
                    char[] arraySTR = readSTR.ToCharArray();

                    string address = arraySTR[1].ToString() + arraySTR[2].ToString();
                    string reder = arraySTR[7].ToString() + arraySTR[8].ToString() + arraySTR[9].ToString() + arraySTR[10].ToString();
                    string greener = arraySTR[11].ToString() + arraySTR[12].ToString() + arraySTR[13].ToString() + arraySTR[14].ToString();

                    float fReadDevice = PublicMehotd.hextoFloat(reder);

                    Check_temp_oil(fReadDevice);
                    
                }
            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "roghan-error-read");
                Oil_deactive();
                if (CreateSerialPort.serialPort_1.IsOpen)
                {
                    CreateSerialPort.serialPort_1.Close();
                    CreateSerialPort.serialPort_1.Dispose();
                }

            }
        }

        public static void Check_temp_oil( float data )
        { 
            if( ( data >= Varibale.Oil_Temp_High ) && Varibale.Status_Normal_Oil)
            {
                Varibale.Status_Normal_Oil = false;
                LogManager log = new LogManager(1 , Enum.EnumKindDevice.oil , Enum.EnumLevel.high , Enum.EnumLOg.Oil_Temp_High , data.ToString("00.0"));

            }
            else if ( ( data <= Varibale.Oil_Temp_Low ) && Varibale.Status_Normal_Oil)
            {
                Varibale.Status_Normal_Oil = false;
                LogManager log = new LogManager(1 , Enum.EnumKindDevice.oil  , Enum.EnumLevel.urgent , Enum.EnumLOg.Oil_Temp_Low , data.ToString("00.0"));
            }
            else if ( ( data < Varibale.Oil_Temp_High) && ( data > Varibale.Oil_Temp_Low) && ! Varibale.Status_Normal_Oil )
            {
                Varibale.Status_Normal_Oil = true;
                LogManager log = new LogManager(1 , Enum.EnumKindDevice.oil , Enum.EnumLevel.medium , Enum.EnumLOg.Oil_Temp_Normal , data.ToString("00.0"));
            }
        }

        public static void Oil_deactive()
        {
            Varibale.Status_Active_Oil = false;
            if (!Varibale.Status_Active_Oil)
            {
                LogManager log = new LogManager(0, Enum.EnumKindDevice.oil, Enum.EnumLevel.urgent, Enum.EnumLOg.Oil_deactive, "*");
            }
            else
            {
            }
        }

        public static void Oil_active()
        {
            Varibale.Status_Active_Oil = true;
            if (Varibale.Status_Active_Oil)
            {
                LogManager log = new LogManager(0, Enum.EnumKindDevice.oil, Enum.EnumLevel.high, Enum.EnumLOg.Oil_active, "*");
            }
            else
            {
            }
        }

        #endregion


        public static bool InsertToDatabase(int devicenumber, Enum.EnumTypeData type, Enum.EnumKindDevice kind, double value)
        {
            try
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

                   
                    return true;
               
            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "autoclav-insert database");
                Enum.ClassEnumError.ShowErrorInMessageBox(Enum.EnumError.insertnewDatalogforDevOther);

                if (CreateSerialPort.serialPort_1.IsOpen)
                {
                    CreateSerialPort.serialPort_1.Close();
                    CreateSerialPort.serialPort_1.Dispose();
                }

                return false;
            }
        }

       

        public static void activeAll()
        {
            Frig_active(1);
            Frig_active(2);
            //Frig_active(3);
            //Frig_active(4);
            //Frig_active(5);
        }

        public static void deactiveAll()
        {
            Frig_deactive(1);
            Frig_deactive(2);
            //Frig_deactive(3);
            //Frig_deactive(4);
            //Frig_deactive(5);
        }

        public static void Frig_deactive(int devicenumber)
        {
            Varibale.Status_Active_Frig[devicenumber] = false;
            if (!Varibale.Status_Active_Frig[devicenumber])
            {
                LogManager log = new LogManager(1, Enum.EnumKindDevice.frig, Enum.EnumLevel.urgent, Enum.EnumLOg.Frig_deactive, "*");
            }
            else
            {
            }
        }

        public static void Frig_active(int devicenumber)
        {
            Varibale.Status_Active_Frig[devicenumber] = true;
            if (Varibale.Status_Active_Frig[devicenumber])
            {
                Varibale.Error_frig[devicenumber] = 0;
                LogManager log = new LogManager(1, Enum.EnumKindDevice.frig, Enum.EnumLevel.high, Enum.EnumLOg.Frig_active, "*");
            }
            else
            {
            }
        }

    }
}
