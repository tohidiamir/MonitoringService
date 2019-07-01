using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonitoringService.Classes;
using System.Data.SqlClient;

namespace MonitoringService.Classes
{
    class DeviceCounter
    {

        public static int Save_data = 0;
        public static int save_max = 5;
        public static bool[] resetActive = new bool[5];

        public DeviceCounter()
        {
            resetActive[0] = false;
            resetActive[1] = false;
            resetActive[2] = false;
            resetActive[3] = false;
            resetActive[4] = false;
            resetActive[5] = false;
        }

        public static void deviceCounterConnect()
        {
            try
            {
                //getShostesho();

                getTypeProduct();

                connectDeviceCounter2();

                checkForSendReset();

            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "counter connect");
            }
        }

        public static void getShostesho()
        {
            try { 
                string x = PLC.Read("010108C80002", "shostesho");
                //sample return :010101C 9 34
                // shostesho mikonan 9
            
                char[] strarray = x.ToCharArray();
                string typenumber = strarray[7].ToString()+strarray[8].ToString();
                int value = Convert.ToInt16(PublicMehotd.convertHextoInt(typenumber));
                string binary = Convert.ToString(value, 2);

                char[] binaryArray = binary.ToCharArray();

                if (binary[6] == '0')
                {
                    PublicMehotd.SetDataToDatabase("shostesho_bad", "0");
                }
                else
                {
                    PublicMehotd.SetDataToDatabase("shostesho_bad", "1");
                }

                if (binary[7] == '0')
                {
                    PublicMehotd.SetDataToDatabase("shostesho_ghabl", "0");
                }
                else
                {
                    PublicMehotd.SetDataToDatabase("shostesho_ghabl", "1");
                }


                //if(typenumber == '')
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                log_system.saveLogSystem(ex, "shostesho");
            }
            
        }

        public static void getTypeProduct()
        {
            try
            {
                //0103 11AE 0001

                //ghabl 11b8 440
                string ghabl = PLC.Read("010311B80005", "type product");
                //MessageBox.Show();

                if (ghabl != "")
                {
                    zarin zarinClass = new zarin();
                    zarinClass.ProccessData("ghabl", ghabl);
                }


                //bad 11b8 430
                string bad = PLC.Read("010311AE0005", "type product");

                if (bad != "")
                {
                    zarin zarinclass_bad = new zarin();
                    zarinclass_bad.ProccessData("bad", bad);
                }

            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex,"type product");
            }

        }

        public static void connectDeviceCounter2()
        {
            //string x = PLC.Read("020311980008" , "counter ");
            

            if (Varibale.Status_active_counter_ghoti == true || Varibale.Error_counter_ghoti % 20 == 0)
            {
                string x = PLC.Read("0103119A0008", "counter ghoti");
                
                if (x == "")
                {

                    Varibale.Error_counter_ghoti++;

                    if (Varibale.Error_counter_ghoti == 2)
                    {
                        LogManager log = new LogManager(1, Enum.EnumKindDevice.counter, Enum.EnumLevel.high, Enum.EnumLOg.error_counter_ghoti, frmIndex.counter[1].Text.ToString());

                    }
                    else if (Varibale.Error_counter_ghoti == 5)
                    {
                        Varibale.Status_active_counter_ghoti = false;
                        LogManager log2 = new LogManager(1, Enum.EnumKindDevice.counter, Enum.EnumLevel.high, Enum.EnumLOg.deactive_counter_ghoti, frmIndex.counter[1].Text.ToString());
                    }

                    //frmIndex.counter[1].Text = Varibale.Error_counter_ghoti + "خطا";
                    //frmIndex.counter[2].Text = Varibale.Error_counter_ghoti + "خطا";
                    //frmIndex.counter[3].Text = Varibale.Error_counter_ghoti + "خطا";
                    //frmIndex.counter[4].Text = Varibale.Error_counter_ghoti + "خطا";

                }
                else
                {
                    processData(x);

                    if (Varibale.Error_counter_ghoti > 0)
                    {
                        Varibale.Error_counter_ghoti = 0;
                        Varibale.Status_active_counter_ghoti = true;
                        LogManager log = new LogManager(5, Enum.EnumKindDevice.counter, Enum.EnumLevel.high, Enum.EnumLOg.active_counter_ghoti, Varibale.Error_counter_ghoti + "خطا");
                    }
                }
            }
            else
            {
                Varibale.Error_counter_ghoti++;
                if (Varibale.Error_counter_ghoti > 100)
                {
                    Varibale.Error_counter_ghoti = 11;
                }
            }            
        }

    
        public static void processData(string element)
        {
            
            // :020310 000C000100020000002B0001000F0001A0
            //:010310C9FF3B9A01F4000000130000CD15075B03
            char[] strarray = element.ToCharArray();

            //c1
            string counter11 = strarray[7].ToString() + strarray[8].ToString() + strarray[9].ToString() + strarray[10].ToString();
            string counter12 = strarray[11].ToString() + strarray[12].ToString() + strarray[13].ToString() + strarray[14].ToString();
            Int64 c12 = PublicMehotd.convertHextoInt(counter12 + counter11);

            //c2
            string counter21 = strarray[15].ToString() + strarray[16].ToString() + strarray[17].ToString() + strarray[18].ToString();
            string counter22 = strarray[19].ToString() + strarray[20].ToString() + strarray[21].ToString() + strarray[22].ToString();
            Int64 c22 = PublicMehotd.convertHextoInt(counter22 + counter21);
            
            //c3
            string counter31 = strarray[23].ToString() + strarray[24].ToString() + strarray[25].ToString() + strarray[26].ToString();
            string counter32 = strarray[27].ToString() + strarray[28].ToString() + strarray[29].ToString() + strarray[30].ToString();
            Int64 c32 = PublicMehotd.convertHextoInt(counter32 + counter31);
           
            //c4
            string counter41 = strarray[31].ToString() + strarray[32].ToString() + strarray[33].ToString() + strarray[34].ToString();
            string counter42 = strarray[35].ToString() + strarray[36].ToString() + strarray[37].ToString() + strarray[38].ToString();
            Int64 c42 = PublicMehotd.convertHextoInt(counter42 + counter41);


            //MessageBox.Show(c12.ToString() + " " + c22.ToString() + " " + c32.ToString() + " " + c42.ToString());

            //frmIndex.counter[1].Text = c12.ToString();
            zarinReport.proccessCount(1,Convert.ToInt64(c12));
            
            //frmIndex.counter[2].Text = c22.ToString();
            zarinReport.proccessCount(2, Convert.ToInt64(c22));
            
            //frmIndex.counter[3].Text = c32.ToString();
            zarinReport.proccessCount(3, Convert.ToInt64(c32));
            
            //frmIndex.counter[4].Text = c42.ToString();
            zarinReport.proccessCount(4, Convert.ToInt64(c42));

            if (DeviceCounter.Save_data == DeviceCounter.save_max)
            {
                DeviceCounter.InsertToDatabase(1, Enum.EnumTypeData.count, Enum.EnumKindDevice.counter, c12);
                DeviceCounter.InsertToDatabase(2, Enum.EnumTypeData.count, Enum.EnumKindDevice.counter, c22);
                DeviceCounter.InsertToDatabase(3, Enum.EnumTypeData.count, Enum.EnumKindDevice.counter, c32);
                DeviceCounter.InsertToDatabase(4, Enum.EnumTypeData.count, Enum.EnumKindDevice.counter, c42);
                DeviceCounter.Save_data = 0;

            }
            else
            {
                DeviceCounter.Save_data = DeviceCounter.Save_data + 1;
            }
            

        }

        public static void checkForSendReset()
        {
            for (int i = 1; i < 5; i++)
            {
                if (resetActive[i] == true)
                {
                    resetWithDelay(i);
                    resetActive[i] = false;
                }
            }
        }

        public static void resetDeviceCounter(int number)
        {
            resetActive[number] = true;
        }

        public static void resetWithDelay(int number)
        {
            try
            {

                string[] n = new String[10];

                n[1] = "010508A0FF00";
                n[2] = "010508A1FF00";

                //MessageBox.Show(n[number]);
                string x = PLC.Read(n[number], "reset device counter");
                //MessageBox.Show(x);
                LogManager log = new LogManager(number, Enum.EnumKindDevice.counter, Enum.EnumLevel.medium, Enum.EnumLOg.reset_counter, "*");
                

            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "reset counter " + number.ToString());
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
                    //cmd.Parameters.AddWithValue("tolid", Varibale.change_product);
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

    }
}
