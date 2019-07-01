using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using MonitoringService.Classes;
using MonitoringService.Enum;
using System.Data.SqlClient;
using MonitoringService.Classes;

namespace MonitoringService.Classes
{
    class zarin_autoclav
    {
        
        public void proccessData(string str)
        {
            
            //410 - 448 
            char[] strarray = str.ToCharArray();

            setDetailAutoclav(strarray, 1, 410, 411, 412, 430, 440, 443, 444);
            setDetailAutoclav(strarray, 2, 414, 415, 416, 432, 441, 445, 446);
            setDetailAutoclav(strarray, 3, 418, 419, 420, 434, 442, 447, 448);


        }

        private void setDetailAutoclav(char[] strarray, int i, int data_temp_num, int data_pres_num, int timer_num, int timer_max_num, int pres_min_num, int temp_min_num , int temp_max_num)
        { 
            double data_temp_a = createStringFromChar(data_temp_num, strarray);
            double data_pres_a = createStringFromChar(data_pres_num, strarray);
            double data_timer_min_a = createStringFromChar(timer_num, strarray);
            double data_timer_sec_a = createStringFromChar(timer_num + 1, strarray);
            double data_timer_max_a = createStringFromChar(timer_max_num, strarray);
            double pres_min  = createStringFromChar(pres_min_num, strarray);
            double temp_min = createStringFromChar(temp_min_num, strarray);
            double temp_max = createStringFromChar(temp_max_num, strarray);

            //check database save for offline system 
            setToDatabase(i, (double)((double)data_temp_a/10), Varibale.Data_Temp_autoclav[i] , "a_temp_"+i);
            setToDatabase(i, (double)((double)data_pres_a/10), Varibale.Data_Pres_autoclav[i], "a_pres_" + i);
            setToDatabase(i,  Convert.ToInt32(((data_timer_min_a) * 60) + (data_timer_sec_a / 10)), Varibale.Timer_Autoclav[i], "a_timer_" + i);

            setToDatabase(i, data_timer_max_a, Varibale.Data_Timer_Max[i], "a_timer_max_" + i);
            setToDatabase(i, (double)((double)pres_min / 10), Varibale.Data_Pres_Min[i], "a_pres_min_" + i);
            setToDatabase(i, (double)((double)temp_min/10), Varibale.Data_Temp_Min[i], "a_temp_min_" + i);
            setToDatabase(i, (double)((double)temp_max/10), Varibale.Data_Temp_Max[i], "a_temp_max_" + i);
            //end offline system

            Varibale.Data_Temp_autoclav[i] = (double)((double)data_temp_a / 10);
            Varibale.Data_Pres_autoclav[i] = (double)((double)data_pres_a / 10);
            Varibale.Timer_Autoclav[i] = Convert.ToInt32(((data_timer_min_a) * 60) + (data_timer_sec_a / 10));
           
            

            Varibale.Data_Timer_Max[i] = Convert.ToInt32((data_timer_max_a * 60));
            //frmIndex.st_lbl_autoclav_time_max[i].Text = PublicMehotd.show_time(Varibale.Data_Timer_Max[i]).ToString();


            Varibale.Data_Pres_Min[i] = (double)((double)pres_min / 10);
            //frmIndex.st_lbl_autoclav_pres_min[i].Text = Varibale.Data_Pres_Min[i].ToString("00.0");

            Varibale.Data_Temp_Min[i] = (double)((double)temp_min / 10);
            //frmIndex.st_lbl_autoclav_temp_min[i].Text = Varibale.Data_Temp_Min[i].ToString("00.0");
            
            Varibale.Data_Temp_Max[i] = (double)((double)temp_max / 10);
            //frmIndex.st_lbl_autoclav_temp_max[i].Text = Varibale.Data_Temp_Max[i].ToString("00.0");
        }

        private long createStringFromChar(int numberdata, char[] strarray)
        {
            //410 - 403 = 7 
            //411 - 400 = 11
            //412 - 397 = 15

            int num = numberdata - 410;

            int start =  7 +(4*num);
            int end = start + 3;
            //MessageBox.Show(start+" X "+end+" X "+strarray.Length);
            string str = "";
            for (int i = start; i <= end; i++)
            {
                str = str + strarray[i].ToString();
            }

            return PublicMehotd.convertHextoInt(str);
        }

        private void setToDatabase(int i , double new_val , double old_val , string updateVar)
        {
            if (new_val != old_val)
            {
                PublicMehotd.SetDataToDatabase(updateVar, new_val.ToString());
            }
        }
    }
}
