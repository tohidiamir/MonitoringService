using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonitoringService.Classes;
using MonitoringService.Enum;
using System.Data.SqlClient;

namespace MonitoringService.Classes
{
    class zarin
    {
        /*
         ghabl
         * 440 type product
         * 441 brand
         * 442 vazn 
         * 443baste bandi 
         * 444 darb 
         * 
         bad 
         * 430 type product
         * 431 brand
         * 432 vazn 
         * 433baste bandi 
         * 434 darb 
         * 7 8 9 10
         * 11 12 13 14
         * 15 16 17 18
         * 19 20 21 22
         */

        private long? zarin_product = null;
        private long? darb = null;
        private long? baste_bandi = null;
        private long? brand = null;
        private long? vazn = null;

        public bool zeroSet = false;

        public void ProccessData(string type , string data)
        {
            stringProccess(data);
            checkChangeData(type);
            checkZeroSet(type);
        }

        #region proccess data va estekhraj dadeha 

        private  void stringProccess(string str)
        {
            char[] strarray = str.ToCharArray();

            zarin_product = createStringFromChar(7, 10, strarray);
            
            brand = createStringFromChar(11, 14, strarray);
            vazn = createStringFromChar(15, 18,strarray);
            baste_bandi = createStringFromChar(19, 22, strarray);
            darb = createStringFromChar(23, 26, strarray);
        }

        private long createStringFromChar(int start , int end , char[] strarray)
        {
            string str = "";
            for (int i = start; i <= end; i++)
            {
                str = str + strarray[i].ToString();
            }

            return PublicMehotd.convertHextoInt(str);
        }

        #endregion

        private void checkChangeData(string type )
        {
            checkChange(type, "zarin_product", zarin_product.ToString());
            //frmIndex.label_zarin[type][0].Text = ClassEnumProduct.GetNameProduct(Convert.ToInt64(zarin_product));

            checkChange(type, "darb", darb.ToString());
            //frmIndex.label_zarin[type][1].Text = ClassEnumZarin.getDarbName(darb.ToString());

            checkChange(type, "baste_bandi", baste_bandi.ToString());
            //frmIndex.label_zarin[type][2].Text = baste_bandi.ToString();

            checkChange(type, "brand", brand.ToString());
            //frmIndex.label_zarin[type][3].Text = brand.ToString();

            checkChange(type, "vazn", vazn.ToString());
            //frmIndex.label_zarin[type][4].Text = " گرم "+vazn.ToString();

        }

        private void checkChange(string type , string var , string val)
        {
            try
            {
                if (Varibale.zarin[var + "_" + type] != val)
                {
                    sendLogChangeData(type, var, Varibale.zarin[var + "_" + type], val);
                    PublicMehotd.SetDataToDatabase(var + "_" + type, val);
                    Varibale.zarin[var + "_" + type] = val;
                    zeroSet = true;
                }
            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "zarin check change");
            }
        }

        private void checkZeroSet(string type)
        {
            if (zeroSet)
            {
                string type_name = "";
                int type_id = 0;

                if (type == "ghabl")
                {
                    type_id = 1;
                    type_name = "قبل استریل";
                }
                else if (type == "bad")
                {
                    type_id = 2;
                    type_name = "بعد استریل";
                }

                LogManager log2 = new LogManager(type_id, Enum.EnumKindDevice.counter, Enum.EnumLevel.medium, Enum.EnumLOg.reset_counter, "کانترهای "+type_name+" صفر شدند.");
            }
        }

        private void sendLogChangeData(string type, string var , string oldData , string newData)
        {
            if (type == "ghabl")
            {
                LogManager log = new LogManager(1, Enum.EnumKindDevice.system, Enum.EnumLevel.high, Enum.EnumLOg.zarin_change_ghabl , ClassEnumZarin.getNameVar(var) + " از " + ClassEnumZarin.getNameDetail(var , oldData) + " به " + ClassEnumZarin.getNameDetail(var ,newData));
            }
            else
            {
                LogManager log = new LogManager(2, Enum.EnumKindDevice.system, Enum.EnumLevel.high, Enum.EnumLOg.zarin_change_bad, ClassEnumZarin.getNameVar(var) + " از " + ClassEnumZarin.getNameDetail(var, oldData) + " به " + ClassEnumZarin.getNameDetail(var, newData));
            }
            
        }
    }
}
