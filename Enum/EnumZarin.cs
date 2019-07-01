using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonitoringService.Enum
{

    
    #region Class Enum Zarin 

    class ClassEnumZarin
    {
        public static string getNameVar(string str)
        {

            switch (str)
            {
                case "zarin_product":
                    return typeVarName[0];

                case "darb":
                    return typeVarName[1];

                case "baste_bandi":
                    return typeVarName[2];

                case "brand":
                    return typeVarName[3];

                case "vazn":
                    return typeVarName[4];

                default:
                    return "نامشخص";

            }
        }



        #region String Array Error 

        private static string[] typeVarName = 
        { 
            "نوع محصول  ", //0
            "درب", //1
            "بسته بندی", //2
            "برند" ,  //3
            "وزن" ,  //4
        };

        #endregion

        public static string getNameDetail(string type, string val)
        {
            switch (type)
            { 
                case "zarin_product":
                    return ClassEnumProduct.GetNameProduct(Convert.ToInt32(val));

                case "brand":
                    return val;

                case "vazn":
                    return val;

                case "baste_bandi":
                    return val;

                case "darb":
                    return ClassEnumZarin.getDarbName(val);

                default:
                    return "نامشخص";

            }
        }

        public static string getDarbName(string val)
        {
            if(Convert.ToInt32(val) == 0)
            {
                return "درب معمولی";
            }
            else if (Convert.ToInt32(val) == 1)
            {
                return "درب ایزی اپن";
            }
            else
            {
                return "نامشخص";
            }
        }
    }

#endregion
}
