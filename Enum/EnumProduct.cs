using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonitoringService.Enum
{

    
    #region Class Enum Product 

    class ClassEnumProduct
    {


     


        public static string GetNameProduct(Int64 typeProduct)
        {
            switch (typeProduct)
            { 
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    return TypeProduct[typeProduct];


                default:
                    return TypeProduct[0];

            }

        }


        public static int getProductZarinNumber(Int64 typeProduct)
        {
            switch (typeProduct)
            { 
                case 0:
                    return 121100;

                case 1:
                    return 121101;

                case 2:
                    return 121102;

                case 3:
                    return 121103;
 
                case 4:
                    return 121200;

                case 5:
                    return 121800;

                case 6:
                    return 120000;

                case 7:
                    return 121300;

                case 8:
                    return 121900;

                default:
                    return 0;
            }
        }

        #region String Array Error 

        private static string[] TypeProduct = 
        { 
            
            "لوبیا چیتی", //0
            "لوبیا چیتی با سس چیلی", //1
            "لوبیا چیتی با سیب زمینی" ,  //2
            "لوبیا چیتی با قارچ" ,  //3
            "نخودفرنگی", //4
            "بادمجان", // 5
            "نخودآبگوشتی", //6 
            "ذرت", //7 
            "عدسی" ,  // 8
            "نامشخص  ", //public
        };

        #endregion

    }

#endregion
}
