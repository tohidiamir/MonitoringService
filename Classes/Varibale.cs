using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO.Ports;
namespace MonitoringService.Classes
{
    class Varibale
    {
        #region Varible Master

        public static int level_Fetch = 0;

        public static bool reciveData_STATUS = true;

        public static string portname1;

        public static int buadrate1;

        public static Parity parity1;

        public static int frig_1_number;

        public static int frig_2_number;

        public static int oil_number;

        public static int totalAutoclav;
        //Tedad Dastgah haie Autoclav

        public static int totalBaking;
        //Tedad Dastgah Pokht

        public static Boolean portError;
        public static int portErrorNumber;

        public static int AutoclavdeviceProblem = 0;

        public static int BakingdevicProblem = 0;

        public static bool Status_Normal_Frig_1;

        public static bool Status_Normal_Frig_2;

        public static bool Status_Normal_Oil;

        public static bool Status_Active_Frig_1;

        public static bool Status_Active_Frig_2;

        public static bool Status_Active_Oil;

        public static int Save_count;

        public static int Save_count_frig;

        public static int save_count_autoclav_server;

        public static int save_count_baking_server;

        public static int i_for_autoclav = 0;
        public static int devicenumber_autoclav = 1;

        public static int i_for_baking = 0;
        public static int devicenumber_baking = 1;


        public static string port_counter = "";

        public static Dictionary<string, string> zarin = new Dictionary<string, string>();

        #endregion

        #region Varibale Array

        public static bool[] Active_autoclav;

        public static int[] Error_autoclav;

        public static int[] Error_baking;

        public static int[] Error_frig = new int[10];

        public static bool[] Active_baking;

        public static string[] Status_Autoclav;

        public static string[] Status_Baking;

        public static int[] Timer_Autoclav;

        public static int[] Timer_Baking;

        public static double[] Data_Temp_autoclav;
        public static double[] Data_Temp_autoclav_old;

        public static double[] Data_Temp_Baking;
        public static double[] Data_Temp_Baking_old;

        public static double[] Data_Pres_autoclav;
        public static double[] Data_Pres_autoclav_old;

        public static double[] Data_Pres_Baking;
        public static double[] Data_Pres_Baking_old;

        public static double[] Data_Pres_Min;
        public static double[] Data_Temp_Min;
        public static double[] Data_Temp_Max;
        public static int[] Data_Timer_Max;

        public static bool[] Autoclav_perioding;


        #region متغیر برای چک کردن غیر نرمال بودن شرایط دستگاه

        public static bool[] Status_Normal_Autoclav_Temp;

        public static bool[] Status_Normal_Autoclav_Pres;

        public static bool[] Status_Zero_Timer_Autoclav;

        public static bool[] Status_Zero_Temp_Timer_Autoclav;

        public static bool[] Status_Normal_Baking_Temp;

        public static bool[] Status_Normal_Baking_Pres;

        public static bool[] Status_Zero_Timer_Baking;
        #endregion

        #region کنترل های اتوکلاو

        public static double Autoclav_Temp_High;
        public static double Autoclav_Temp_Low;
        public static double Autoclav_Pres_high;
        public static double Autoclav_Pres_Low;
        public static int Autoclav_Timer_max;

        #endregion

        #region کنترل های پخت

        public static double Baking_Temp_High;
        public static double Baking_Temp_Low;
        public static double Baking_Pres_high;
        public static double Baking_Pres_Low;
        public static int Baking_Timer_max;

        #endregion

        #region کنترل های سردخانه و روغن ریز


        public static int[] Error_Frig = new int[10];
        public static double[] Frig_Temp = new double[10];
        public static double[] Frig_Temp_High = new double[10];
        public static double[] Frig_Temp_Low = new double[10];
        public static int[] frig_number = new int[10];
        public static bool[] Status_Active_Frig = new bool[11];
        public static bool[] Status_Normal_Frig = new bool[10];

        public static double Oil_Temp_High;
        public static double Oil_Temp_Low;
        public static double Oil_Temp;

        #endregion

        #region کنترل های کانتر

        public static bool Status_active_counter_bastebandi = true;
        public static int Error_counter_bastebandi = 0;
        public static bool Status_active_counter_ghoti = true;
        public static int Error_counter_ghoti = 0;

        #endregion 

        #endregion

        #region Array list


        #region baking

        //public static List<PictureBox> lst_baking_red_light = new List<PictureBox>();

        //public static List<PictureBox> lst_baking_green_light = new List<PictureBox>();

        //public static List<PictureBox> lst_baking_show_timer = new List<PictureBox>();

        //public static List<PictureBox> lst_baking_hidden_timer = new List<PictureBox>();

        //public static List<Label> lst_baking_temp = new List<Label>();

        //public static List<Label> lst_baking_timer = new List<Label>();

        //public static List<Button> lst_baking_btn_active = new List<Button>();

        //public static List<Button> lst_Baking_btn_chart = new List<Button>();

        #endregion


        #region Autoclav

        //public static List<PictureBox> lst_autoclav_red_light = new List<PictureBox>();

        //public static List<PictureBox> lst_autoclav_green_light = new List<PictureBox>();

        //public static List<PictureBox> lst_autoclav_show_timer = new List<PictureBox>();

        //public static List<PictureBox> lst_autoclav_hidden_timer = new List<PictureBox>();

        //public static List<Label> lst_autoclav_temp = new List<Label>();

        //public static List<Label> lst_autoclav_pres = new List<Label>();

        //public static List<Label> lst_autoclav_timer = new List<Label>();

        //public static List<Button> lst_autoclav_btn_active = new List<Button>();

        //public static List<Button> lst_autoclav_btn_chart = new List<Button>();

        #endregion


        #endregion

        public static void VaribleSetting()
        {
            portError = false;
            portErrorNumber = 0;
            SetFromDatabaseToVarible();
            SetArrayAutoclavCount();
            SetArrayBakingCount();
            SetVaribleOilAndFrig();
            SetFirestValueBaking();
            SetFirstValueAutoclav();
            SetTimerAutoclav();
            SetTimerBaking();
        }

        public static void SetFromDatabaseToVarible()
        {

            try
            {

                SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();

                SqlCommand cmd = ConnectToDatabase.NewSqlCommand(System.Data.CommandType.StoredProcedure, "Select_All_Varible", cn);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    try
                    {
                        switch (dr["name"].ToString())
                    {
                    
                        case "Autoclav_temp_max":
                            Varibale.Autoclav_Temp_High = Convert.ToDouble(dr["value"].ToString());
                        break;

                        case "Autoclav_temp_min":
                            Varibale.Autoclav_Temp_Low = Convert.ToDouble(dr["value"].ToString());
                        break;

                        case "Autoclav_pres_max":
                            Varibale.Autoclav_Pres_high = Convert.ToDouble(dr["value"].ToString());
                        break;

                        case "Autoclav_pres_min":
                            Varibale.Autoclav_Pres_Low = Convert.ToDouble(dr["value"].ToString());
                        break;

                        case "Autoclav_timer_max":
                            Varibale.Autoclav_Timer_max = Convert.ToInt32(dr["value"].ToString());
                        break;


                        case "Baking_temp_max":
                            Varibale.Baking_Temp_High = Convert.ToDouble(dr["value"].ToString());
                        break;

                        case "Baking_temp_min":
                            Varibale.Baking_Temp_Low = Convert.ToDouble(dr["value"].ToString());
                        break;

                        case "Baking_pres_max":
                            Varibale.Baking_Pres_high = Convert.ToDouble(dr["value"].ToString());
                        break;

                        case "Baking_pres_min":
                            Varibale.Baking_Pres_Low = Convert.ToDouble(dr["value"].ToString());
                        break;

                        case "Baking_timer_max":
                            Varibale.Baking_Timer_max = Convert.ToInt32(dr["value"].ToString());
                        break;

                        case "totalAutoclav":
                            Varibale.totalAutoclav = Convert.ToInt32(dr["value"].ToString()) + 1;
                        break;

                        case "totalBaking":
                            Varibale.totalBaking = Convert.ToInt32(dr["value"].ToString()) + 1;
                        break;

                        case "Frig_1_Temp_High":
                            Varibale.Frig_Temp_High[1] = Convert.ToDouble(dr["value"].ToString());
                        break;

                        case "Frig_1_Temp_Low":
                            Varibale.Frig_Temp_Low[1] = Convert.ToDouble(dr["value"].ToString());
                        break;

                        case "Frig_2_Temp_High":
                            Varibale.Frig_Temp_High[2] = Convert.ToDouble(dr["value"].ToString());
                        break;

                        case "Frig_2_Temp_Low":
                            Varibale.Frig_Temp_Low[2] = Convert.ToDouble(dr["value"].ToString());
                        break;

                        case "Frig_3_Temp_High":
                            Varibale.Frig_Temp_High[3] = Convert.ToDouble(dr["value"].ToString());
                        break;

                        case "Frig_3_Temp_Low":
                            Varibale.Frig_Temp_Low[3] = Convert.ToDouble(dr["value"].ToString());
                        break;

                        case "Frig_4_Temp_High":
                            Varibale.Frig_Temp_High[4] = Convert.ToDouble(dr["value"].ToString());
                        break;

                        case "Frig_4_Temp_Low":
                            Varibale.Frig_Temp_Low[4] = Convert.ToDouble(dr["value"].ToString());
                        break;

                        case "Frig_5_Temp_High":
                            Varibale.Frig_Temp_High[5] = Convert.ToDouble(dr["value"].ToString());
                        break;

                        case "Frig_5_Temp_Low":
                            Varibale.Frig_Temp_Low[5] = Convert.ToDouble(dr["value"].ToString());
                        break;

                        case "Oil_Temp_High":
                            Varibale.Oil_Temp_High = Convert.ToDouble(dr["value"].ToString());
                        break;

                        case "Oil_Temp_Low":
                            Varibale.Oil_Temp_Low = Convert.ToDouble(dr["value"].ToString());
                        break;

                        case "portname1":
                            Varibale.portname1 = dr["value"].ToString();
                        break;

                        case "buadrate1":
                            Varibale.buadrate1 = Convert.ToInt32(dr["value"].ToString());
                        break;

                        case "parity1":
                            Varibale.parity1 = PublicMehotd.ConvertStringToParity(dr["value"].ToString());
                        break;

                        case "Frig_1_number":
                            Varibale.frig_number[1] = Convert.ToInt32(dr["value"].ToString());
                        break;

                        case "Frig_2_number":
                            Varibale.frig_number[2] = Convert.ToInt32(dr["value"].ToString());
                        break;

                        case "Frig_3_number":
                            Varibale.frig_number[3] = Convert.ToInt32(dr["value"].ToString());
                        break;

                        case "Frig_4_number":
                            Varibale.frig_number[4] = Convert.ToInt32(dr["value"].ToString());
                        break;

                        case "Frig_5_number":
                            Varibale.frig_number[5] = Convert.ToInt32(dr["value"].ToString());
                        break;


                        case "Oil_number":
                            Varibale.oil_number = Convert.ToInt32(dr["value"].ToString());
                        break;

                      

                        case "save_count":
                            Varibale.Save_count = Convert.ToInt32(dr["value"].ToString());
                        break;

                        case "save_count_frig":
                            Varibale.Save_count_frig = Convert.ToInt32(dr["value"].ToString());
                        break;

                        case "save_count_baking_server":
                            Varibale.save_count_baking_server = Convert.ToInt32(dr["value"].ToString());
                        break;

                        case "save_count_autoclav_server":
                            Varibale.save_count_autoclav_server = Convert.ToInt32(dr["value"].ToString());
                        break;

                        case "port_counter":
                            Varibale.port_counter = dr["value"].ToString();
                        break;

                        case "zarin_product_ghabl":
                            Varibale.zarin.Add("zarin_product_ghabl", dr["value"].ToString());
                        break;

                        case "zarin_product_bad":
                            Varibale.zarin.Add("zarin_product_bad", dr["value"].ToString());
                        break;

                        case "brand_ghabl":
                            Varibale.zarin.Add("brand_ghabl", dr["value"].ToString());
                        break;

                        case "brand_bad":
                            Varibale.zarin.Add("brand_bad", dr["value"].ToString());
                        break;

                        case "vazn_ghabl":
                            Varibale.zarin.Add("vazn_ghabl", dr["value"].ToString());
                        break;

                        case "vazn_bad":
                            Varibale.zarin.Add("vazn_bad", dr["value"].ToString());
                        break;

                        case "baste_bandi_ghabl":
                            Varibale.zarin.Add("baste_bandi_ghabl", dr["value"].ToString());
                        break;

                        case "baste_bandi_bad":
                            Varibale.zarin.Add("baste_bandi_bad", dr["value"].ToString());
                        break;

                        case "darb_ghabl":
                            Varibale.zarin.Add("darb_ghabl", dr["value"].ToString());
                        break;

                        case "darb_bad":
                            Varibale.zarin.Add("darb_bad", dr["value"].ToString());
                        break;
                   
                    }
                    }
                    catch (Exception ex)
                    {
                        log_system.saveLogSystem(ex, "error read value =" + dr["value"].ToString());
                    }

                }

                cn.Close();

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.StackTrace);
                log_system.saveLogSystem(ex, "Variable Error " );
            }

        }

        public static void SetArrayAutoclavCount()
        {
            Status_Normal_Autoclav_Pres = new bool[totalAutoclav + 1];

            Status_Normal_Autoclav_Temp = new bool[totalAutoclav + 1];

            Status_Autoclav = new string[totalAutoclav + 1];

            Status_Zero_Timer_Autoclav = new bool[totalAutoclav + 1];
            Status_Zero_Temp_Timer_Autoclav = new bool[totalAutoclav + 1];

            Data_Pres_autoclav = new double[totalAutoclav + 1];
            Data_Pres_autoclav_old = new double[totalAutoclav + 1];

            Data_Temp_autoclav = new double[totalAutoclav + 1];
            Data_Temp_autoclav_old = new double[totalAutoclav + 1];

            Data_Pres_Min = new double[totalAutoclav + 1];
            Data_Temp_Min = new double[totalAutoclav + 1];
            Data_Temp_Max = new double[totalAutoclav + 1];
            Data_Timer_Max = new int[totalAutoclav + 1];
            Autoclav_perioding = new bool[totalAutoclav + 1];
            Timer_Autoclav = new int[totalAutoclav + 1];

            Active_autoclav = new bool[totalAutoclav + 1];

            Error_autoclav = new int[totalAutoclav + 1];
        }

        public static void SetArrayBakingCount()
        {
            Status_Normal_Baking_Pres = new bool[totalBaking];

            Status_Normal_Baking_Temp = new bool[totalBaking];

            Status_Baking = new string[totalBaking];

            Status_Zero_Timer_Baking = new bool[totalBaking];

            Data_Pres_Baking = new double[totalBaking];
            Data_Pres_Baking_old = new double[totalBaking];

            Data_Temp_Baking = new double[totalBaking];
            Data_Temp_Baking_old = new double[totalBaking];

            Timer_Baking = new int[totalBaking + 5];

            Active_baking = new bool[totalBaking + 5];

            Error_baking = new int[totalBaking + 5];
        }

        public static void SetVaribleOilAndFrig()
        {

            Status_Normal_Frig[1] = true;
            Status_Normal_Frig[2] = true;
            Status_Normal_Frig[3] = true;
            Status_Normal_Frig[4] = true;
            Status_Normal_Frig[5] = true;
            Status_Normal_Oil = true;

            Status_Active_Frig[1] = true;
            Status_Active_Frig[2] = true;
            Status_Active_Frig[3] = true;
            Status_Active_Frig[4] = true;
            Status_Active_Frig[5] = true;

            Status_Active_Oil = false;
            // false shode bekhater in ke oil nadare

        }

        public static void SetFirstValueAutoclav()
        {
            for (int i = 0; i < Status_Normal_Autoclav_Pres.Length; i++)
            {
                Status_Normal_Autoclav_Pres[i] = true;
            }

            for (int i = 0; i < Status_Normal_Autoclav_Temp.Length; i++)
            {
                Status_Normal_Autoclav_Temp[i] = true;
            }

            for (int i = 0; i < Status_Zero_Timer_Autoclav.Length; i++)
            {
                Status_Zero_Timer_Autoclav[i] = true;
            }

            for (int i = 0; i < Status_Zero_Temp_Timer_Autoclav.Length; i++)
            {
                Status_Zero_Temp_Timer_Autoclav[i] = true;
            }

            for (int i = 0; i < Active_autoclav.Length; i++)
            {
                Active_autoclav[i] = true;
            }

            for (int i = 0; i < Autoclav_perioding.Length; i++)
            {
                Autoclav_perioding[i] = false;
            }

        }

        public static void SetFirestValueBaking()
        {

            for (int i = 0; i < Status_Normal_Baking_Pres.Length; i++)
            {
                Status_Normal_Baking_Pres[i] = true;
            }

            for (int i = 0; i < Status_Normal_Baking_Temp.Length; i++)
            {
                Status_Normal_Baking_Temp[i] = true;
            }

            for (int i = 0; i < Status_Zero_Timer_Baking.Length; i++)
            {
                Status_Zero_Timer_Baking[i] = true;
            }

            for (int i = 0; i < Active_baking.Length; i++)
            {
                Active_baking[i] = true;
                //false shode bekhater ke baking nadare
            }
        }

        public static void SetTimerBaking()
        {
            for (int i = 1; i < totalBaking; i++)
            {
                Timer_Baking[i] = 0;

                //lst_baking_timer[i - 1].Text = PublicMehotd.show_time(Timer_Baking[i]);
            }
        }

        public static void SetTimerAutoclav()
        {
            for (int i = 1; i < totalAutoclav; i++)
            {
                Timer_Autoclav[i] = 0;
                //lst_autoclav_timer[i - 1].Text = PublicMehotd.show_time(Timer_Autoclav[i]);
            }
        }
    }
}
