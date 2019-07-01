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
    class zarinReport
    {
        public static void proccessCount(int num, Int64 number)
        {
            try
            {
                checkLastCounter(num, number);
            }
            catch (Exception ex){
                log_system.saveLogSystem(ex, "zarin report proccess count ");
            }
        }

        private static void checkLastCounter(int num, Int64 number)
        {
            try
            {
                string[] recordLast = getLastDataCounter(num);

                if (recordLast != null)
                {
                    int id = Convert.ToInt32(recordLast[0].ToString());
                    long lastNumber = Convert.ToInt64(recordLast[2]);

                    if (number >= lastNumber)
                    {
                        zarinReport.updateRecord(id, number, "counts");
                    }
                    else
                    {
                        zarinReport.updateRecord(id, lastNumber, "end");
                        zarinReport.newRecord(num, number);
                    }
                }
                else
                {
                    zarinReport.newRecord(num, number);
                }
            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "zarin report checkLastCounter");
            }
            
        }

        private static void updateRecord(int id , Int64 number ,string status)
        {
            string[] RecordData = getRecordReportProduct(id);
            string statusUpdate = RecordData[17];

            try { 

            if (status == "end")
            {
                endRecord( id,  number , RecordData);
            }
            else
            {
                string intTimeEnd = "0";
                string timeEnd = "00:00:00";
                string timeUpdateQuery = ", timeUpdate = '" + PublicMehotd.RetStringLocalTime() + "' ";
                string sql = "update zarin_report_counter set status = '" + status
               + "' , number = '" + number
               + "' , intTimeEnd = '" + intTimeEnd
               + "' , timeEnd = '" + timeEnd
               + "' " + timeUpdateQuery
               + " where id = " + id + " ";

                SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();
                SqlCommand cmd = ConnectToDatabase.NewSqlCommand(CommandType.Text, sql, cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

                    if (number > 1000 && statusUpdate == "0")
                    {
                        updateFirstData(id, RecordData);
                    }

            }
            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "zarin report update record ");
            }

        }


        private static void updateFirstData(int id, string[] RecordData)
        {
            
            string sql = "";
            try {
               
                 string[] FirstData = calculatefirstData(id, RecordData);
                string timeStart = FirstData[1];
                string intTimeStart = FirstData[0];
                string datep = FirstData[2];
                string yearp = FirstData[3];
                string monthp = FirstData[4];
                string dayp = FirstData[5];

                sql = "update zarin_report_counter set  intTimeStart = '" + intTimeStart
                + "' , timeStart = '" + timeStart
                + "' , datep = '" + datep
                + "' , yearp = '" + yearp
                + "' , monthp = '" + monthp
                + "' , dayp = '" + dayp
                + "' , updateStartSuccess = '1' where id = " + id + " ";

                    SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();
                    SqlCommand cmd = ConnectToDatabase.NewSqlCommand(CommandType.Text, sql, cn);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
               
            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "zarin report update   record first data =" + sql);
            }
        }


        private static string[] calculatefirstData(int id, string[] RecordData)
        {

            //string[] RecordData = getRecordReportProduct(id);
            string intTimeStart = RecordData[8];
            string Counter_num = RecordData[10];
            string timeStart = RecordData[11];
            string dayp = RecordData[13];
            string monthp = RecordData[14];
            string yearp = RecordData[15];
            string datep = RecordData[16];

            string sql = "";
            string[] x = new string[10];
            try
            {
                sql = "select top 1 * from Data  where type = 'count' and device_no = '" + Counter_num
                   + "' and intTime > '" + intTimeStart
                   + "' and id < (select top 1 id from Data where type = 'count' and device_no = '" + Counter_num
                   + "' and intTime > '" + intTimeStart
                   + "' and value != '0' order by id ) order by id desc";

                SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();
                SqlCommand cmd = ConnectToDatabase.NewSqlCommand(CommandType.Text, sql, cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    x[0] = dr["intTime"].ToString();
                    x[1] = dr["timep"].ToString();
                    x[2] = dr["datep"].ToString();
                    x[3] = dr["yearp"].ToString();
                    x[4] = dr["monthp"].ToString();
                    x[5] = dr["dayp"].ToString();


                    cn.Close();
                    return x;
                }
                else
                {
                    x[0] = intTimeStart;
                    x[1] = timeStart;
                    x[2] = datep;
                    x[3] = yearp;
                    x[4] = monthp;
                    x[5] = dayp;
                    cn.Close();
                    return x;
                }

            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "zarin report end  record first data =" + sql);
                return null;
            }


        }


        private static void endRecord(int id, Int64 number , string[] RecordData)
        {
            string sql = "";
            try { 

            string[] endRecord = calculateLastData(id, number , RecordData);
            string intTimeEnd = endRecord[0];
            string timeEnd = endRecord[1];
            string timeUpdate = timeEnd;

             sql = "update zarin_report_counter set status = 'end' , number = '" + number
                + "' , intTimeEnd = '" + intTimeEnd
                + "' , timeEnd = '" + timeEnd
                + "' , timeUpdate = '" + timeUpdate
                + "' where id = " + id + " ";

            SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();
            SqlCommand cmd = ConnectToDatabase.NewSqlCommand(CommandType.Text, sql, cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "zarin report end  record ="+sql);
            }
        }
        


        private static string[] calculateLastData(int id , Int64 number , string[] RecordData)
        {

            string NOWEND = PublicMehotd.time_php();

            //string[] RecordData = getRecordReportProduct(id);
            string intTimeEnd = RecordData[9];
            string Counter_num = RecordData[10];
            string timeEnd = RecordData[12];

            string[] x = new string[10];
            string sql = "";
            try
            {
                 sql = "select top 1  * from Data where type = 'count' and device_no = '"+ Counter_num 
                    + "' and intTime < '"+ NOWEND + "' and value in ( select  top 1 value from Data where type = 'count' and device_no = '"+ Counter_num 
                    + "'and intTime < '"+ NOWEND + "'  order by id desc ) order by id asc";

                SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();
                SqlCommand cmd = ConnectToDatabase.NewSqlCommand(CommandType.Text, sql, cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    x[0] = dr["intTime"].ToString();
                    x[1] = dr["timep"].ToString();


                    cn.Close();
                    return x;
                }
                else
                {
                    x[0] = PublicMehotd.time_php();
                    x[1] = PublicMehotd.RetStringLocalTime();
                    cn.Close();
                    return x;
                }

            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "zarin report end  record last data="+sql);
                return null;
            }
        }

        private static string[] getRecordReportProduct(int id )
        {

            string sql = "select * from zarin_report_counter where id ='" + id
                + "'  ";

            try
            {

                string[] x = new string[25];
                SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();
                SqlCommand cmd = ConnectToDatabase.NewSqlCommand(CommandType.Text, sql, cn);


                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    x[0] = dr["id"].ToString();
                    x[1] = dr["counter_num"].ToString();
                    x[2] = dr["number"].ToString();
                    x[3] = dr["zarin_product"].ToString();
                    x[4] = dr["brand"].ToString();
                    x[5] = dr["darb"].ToString();
                    x[6] = dr["vazn"].ToString();
                    x[7] = dr["baste_bandi"].ToString();
                    x[8] = dr["intTimeStart"].ToString();
                    x[9] = dr["intTimeEnd"].ToString();
                    x[10] = dr["counter_num"].ToString();
                    x[11] = dr["timeStart"].ToString();
                    x[12] = dr["timeEnd"].ToString();
                    x[13] = dr["dayp"].ToString();
                    x[14] = dr["monthp"].ToString();
                    x[15] = dr["yearp"].ToString();
                    x[16] = dr["datep"].ToString();
                    x[17] = dr["updateStartSuccess"].ToString();

                    cn.Close();
                    return x;
                }
                else
                {
                    cn.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "error get last data counter " + sql + " XX " + id);
                return null;
            }
        }

        private static void closeAllRcord(int num )
        {
            string sql = "update zarin_report_counter set status = 'end' ,timeEnd='" + PublicMehotd.RetStringLocalTime() 
                + "' , intTimeEnd = '" + PublicMehotd.time_php() + "'  where counter_num = " + num + " and  status = 'counts' ";

            SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();
            SqlCommand cmd = ConnectToDatabase.NewSqlCommand(CommandType.Text, sql, cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();

            removeNumberZero(num);

        }

        private static void removeNumberZero(int num)
        {
            string sql = "delete from zarin_report_counter " +
                "where counter_num = " + num + " and status = 'end' and number = 0 ";

            SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();
            SqlCommand cmd = ConnectToDatabase.NewSqlCommand(CommandType.Text, sql, cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        private static void newRecord(int num, Int64 number)
        {
            zarinReport.closeAllRcord(num);

            string type = "";
            if (num == 1 || num == 2)
            {
                type = "ghabl";
            }
            else
            {
                type = "bad";
            }

            string sql = "insert into zarin_report_counter"+
                " (status , intTimeStart , intTimeEnd, timeStart, timeEnd , datep  , yearp , monthp , dayp , number , zarin_product , brand , vazn , baste_bandi , darb , counter_num )" +
                " values "+
                " ('counts' , '" + 
                PublicMehotd.time_php() + "' , '0' , '" + 
                PublicMehotd.RetStringLocalTime() + "' , '00:00:00' , '"+
                PublicMehotd.RetStringPersianCalender()+"' , '"+
                PublicMehotd.retStringYearPersian()+"' , '"+
                PublicMehotd.retStringMonthPersian()+"' , '"+
                PublicMehotd.retStringDayPersian()+"' , '"+
                number.ToString()+"' , '"+
                ClassEnumProduct.getProductZarinNumber(Convert.ToInt64(Varibale.zarin["zarin_product_" + type]))
                +"' , '"+
                Varibale.zarin["brand_"+type]
                +"' , '"+
                Varibale.zarin["vazn_" + type]
                +"' ,  '"+
                Varibale.zarin["baste_bandi_" + type]
                +"' , '"+
                Varibale.zarin["darb_" + type]
                +"' , '"+
                num.ToString()
                +"')";
            
            SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();
            SqlCommand cmd = ConnectToDatabase.NewSqlCommand(CommandType.Text, sql, cn);
            cn.Open(); 
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        private static string[] getLastDataCounter(int num)
        {
            string type = "";
            if (num == 1 || num == 2)
            {
                type = "ghabl";
            }
            else
            {
                type = "bad";
            }

            string zarin_product = ClassEnumProduct.getProductZarinNumber(Convert.ToInt64(Varibale.zarin["zarin_product_" + type])).ToString();
            

            string sql = "select * from zarin_report_counter where counter_num ='" + num
                + "' and status = 'counts' "
                + " and zarin_product = '" + zarin_product
                + "' and brand = '" + Varibale.zarin["brand_" + type]
                + "' and vazn = '" + Varibale.zarin["vazn_" + type]
                + "' and darb = '" + Varibale.zarin["darb_" + type]
                + "' and baste_bandi = '" + Varibale.zarin["baste_bandi_" + type] + "' ";

            try
            {
               
                string[] x = new string[10];
                SqlConnection cn = ConnectToDatabase.NewConnectToDatabase();
                SqlCommand cmd = ConnectToDatabase.NewSqlCommand(CommandType.Text, sql, cn);


                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    x[0] = dr["id"].ToString();
                    x[1] = dr["counter_num"].ToString();
                    x[2] = dr["number"].ToString();
                    x[3] = dr["zarin_product"].ToString();
                    x[4] = dr["brand"].ToString();
                    x[5] = dr["darb"].ToString();
                    x[6] = dr["vazn"].ToString();
                    x[7] = dr["baste_bandi"].ToString();

                   // if (PublicMehotd.RetStringPersianCalender() == dr["datep"].ToString())
                   // {
                        cn.Close();
                        return x;
                   // }
                    //else
                    //{
                    //    cn.Close();
                        
                    //    return null;
                   // }
                    

                }
                else
                {
                    cn.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex , "error get last data counter "+sql+" XX "+zarin_product);
                return null;
            }

           
        }
    }
}