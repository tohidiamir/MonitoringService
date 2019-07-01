using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitoringService.Enum
{
    #region Enum Log 

    enum EnumLOg
    { 
        Baking_Temp_Low,
        Baking_Temp_High,
        Baking_Temp_Normal,

        Baking_Pres_Low,
        Baking_Pres_High,
        Baking_Pres_Normal,

        Baking_Timer_Zero,
        
        Baking_Enable,
        Baking_Disable,
        Baking_Connection_error,


        Autoclav_Temp_Low,
        Autoclav_Temp_High,
        Autoclav_Temp_Normal,

        Autoclav_Pres_Low,
        Autoclav_Pres_High,
        Autoclav_Pres_Normal,

        Autoclav_Timer_Zero,
        Autoclav_Cold,

        Autoclav_Enable,
        Autoclav_Disable,
        Autoclav_Connection_error,


        Frig_1_Temp_Low,
        Frig_1_Temp_High,
        Frig_1_Temp_Normal,
        Frig_1_active,
        Frig_1_deactive,

        Frig_2_Temp_Low,
        Frig_2_Temp_High,
        Frig_2_Temp_Normal,
        Frig_2_active,
        Frig_2_deactive,


        Frig_Temp_Low,
        Frig_Temp_High,
        Frig_Temp_Normal,
        Frig_active,
        Frig_deactive,

        Frig_Connection_error,

        Oil_Temp_Low,
        Oil_Temp_High,
        Oil_Temp_Normal,
        Oil_active,
        Oil_deactive,


        Period_Autoclav_Start,
        Period_Autoclav_Naghes,
        Period_Autoclav_End,

        Period_Baking_Start,
        Period_Baking_Naghes,
        Period_Baking_End,


        SerialPort_Connect,
        SerialPort_disconnect,

        Database_connect,
        Database_Not_connect , 

        exit_program , 
        start_program ,
        SerialPort_disconnect_operator ,
        SerialPort_connect_operator,
        SerialPort_request_connect_operator ,
        SerialPort_request_connect_ERROR,
        SerialPort_request_connect_Success , 


        reset_counter , 
        error_counter_bastebandi , 
        error_counter_ghoti ,
        deactive_counter_bastebandi,
        deactive_counter_ghoti,
        active_counter_bastebandi,
        active_counter_ghoti, 


        zarin_change_ghabl,
        zarin_change_bad

    }

    #endregion

    #region Class Enum Log 

    class classEnumLog
    {
        public static string ShowError(EnumLOg log)
        {
            switch (log)
            {
                case EnumLOg.Autoclav_Disable:
                    return LogMessage[1];

                case EnumLOg.Autoclav_Enable:
                    return LogMessage[2];

                case EnumLOg.Autoclav_Pres_High:
                    return LogMessage[3];

                case EnumLOg.Autoclav_Pres_Low:
                    return LogMessage[4];

                case EnumLOg.Autoclav_Pres_Normal:
                    return LogMessage[5];

                case EnumLOg.Autoclav_Temp_High:
                    return LogMessage[6];

                case EnumLOg.Autoclav_Temp_Low:
                    return LogMessage[7];

                case EnumLOg.Autoclav_Temp_Normal:
                    return LogMessage[8];

                case EnumLOg.Autoclav_Timer_Zero:
                    return LogMessage[9];

                case EnumLOg.Baking_Disable:
                    return LogMessage[10];

                case EnumLOg.Baking_Enable:
                    return LogMessage[11];

                case EnumLOg.Baking_Pres_High:
                    return LogMessage[12];

                case EnumLOg.Baking_Pres_Low:
                    return LogMessage[13];

                case EnumLOg.Baking_Pres_Normal:
                    return LogMessage[14];

                case EnumLOg.Baking_Temp_High:
                    return LogMessage[15];

                case EnumLOg.Baking_Temp_Low:
                    return LogMessage[16];

                case EnumLOg.Baking_Temp_Normal:
                    return LogMessage[17];

                case EnumLOg.Baking_Timer_Zero:
                    return LogMessage[18];

                case EnumLOg.Frig_1_Temp_High:
                    return LogMessage[19];

                case EnumLOg.Frig_1_Temp_Low:
                    return LogMessage[20];

                case EnumLOg.Frig_1_Temp_Normal:
                    return LogMessage[21];

                case EnumLOg.Frig_2_Temp_High:
                    return LogMessage[22];

                case EnumLOg.Frig_2_Temp_Low:
                    return LogMessage[23];

                case EnumLOg.Frig_2_Temp_Normal:
                    return LogMessage[24];

                case EnumLOg.Oil_Temp_High:
                    return LogMessage[25];

                case EnumLOg.Oil_Temp_Low:
                    return LogMessage[26];

                case EnumLOg.Oil_Temp_Normal:
                    return LogMessage[27];

                case EnumLOg.Period_Autoclav_End:
                    return LogMessage[28];

                case EnumLOg.Period_Autoclav_Naghes:
                    return LogMessage[29];

                case EnumLOg.Period_Autoclav_Start:
                    return LogMessage[30];

                case EnumLOg.Period_Baking_End:
                    return LogMessage[31];

                case EnumLOg.Period_Baking_Naghes:
                    return LogMessage[32];

                case EnumLOg.Period_Baking_Start:
                    return LogMessage[33];

                case EnumLOg.SerialPort_Connect:
                    return LogMessage[34];

                case EnumLOg.SerialPort_disconnect:
                    return LogMessage[35];

                case EnumLOg.Database_connect:
                    return LogMessage[36];

                case EnumLOg.Database_Not_connect:
                    return LogMessage[37];
                
                case Enum.EnumLOg.Oil_deactive:
                    return LogMessage[38];

                case EnumLOg.Oil_active:
                    return LogMessage[39];

                case EnumLOg.Frig_1_deactive:
                    return LogMessage[40];

                case EnumLOg.Frig_1_active:
                    return LogMessage[41];

                case EnumLOg.Frig_2_deactive:
                    return LogMessage[42];

                case EnumLOg.Frig_2_active:
                    return LogMessage[43];

                case EnumLOg.Autoclav_Connection_error:
                    return LogMessage[44];

                case EnumLOg.exit_program:
                    return LogMessage[45];

                case EnumLOg.start_program:
                    return LogMessage[46];

                case EnumLOg.SerialPort_disconnect_operator:
                    return LogMessage[47];

                case EnumLOg.SerialPort_connect_operator:
                    return LogMessage[48];

                case EnumLOg.SerialPort_request_connect_operator:
                    return LogMessage[49];

                case EnumLOg.Frig_active:
                    return LogMessage[50];

                case EnumLOg.Frig_deactive:
                    return LogMessage[51];

                case EnumLOg.Frig_Temp_High:
                    return LogMessage[52];

                case EnumLOg.Frig_Temp_Normal:
                    return LogMessage[53];

                case EnumLOg.Frig_Temp_Low:
                    return LogMessage[54];

                case EnumLOg.Frig_Connection_error:
                    return LogMessage[55];

                case EnumLOg.Autoclav_Cold:
                    return LogMessage[56];

                case EnumLOg.SerialPort_request_connect_ERROR:
                    return LogMessage[57];

                case EnumLOg.SerialPort_request_connect_Success:
                    return LogMessage[58];

                case EnumLOg.reset_counter:
                    return LogMessage[59];

                case EnumLOg.error_counter_bastebandi:
                    return LogMessage[60];

                case EnumLOg.error_counter_ghoti:
                    return LogMessage[61];

                case EnumLOg.deactive_counter_bastebandi:
                    return LogMessage[62];

                case EnumLOg.deactive_counter_ghoti:
                    return LogMessage[63];

                case EnumLOg.active_counter_bastebandi:
                    return LogMessage[64];

                case EnumLOg.active_counter_ghoti:
                    return LogMessage[65];

                case EnumLOg.zarin_change_ghabl:
                    return LogMessage[66];

                case EnumLOg.zarin_change_bad:
                    return LogMessage[67];


                default:
                    return LogMessage[0];

            }

        }

        #region String Array Error

        private static string[] LogMessage = 
        { 
            "نامعلوم", //public
            
            "غیر فعال شده است", //1
            
            "فعال شده است", //2
            
            "فشار بیش از حد مجاز داشته است", //3
            
            "فشار کمتر از حد مجاز داشته است", //4

            "دارای فشار نرمال شده است", //5

            "دما بیش از حد مجاز داشته است", //6

            "دما کمتر از حد مجاز داشته است", //7

            "دارای دما نرمال شده است", //8

            "عدد تایمر صفر شده است", //9

            "غیر فعال شده است", //10

            "فعال شده است", //11

            "فشار بیش از حد مجاز شده است", //12

            "فشار کمتر از حد مجاز داشته است", //13

            "دارای فشار نرمال شده است", //14

            "دما بیش از حد مجاز داشته است", //15

            "دما کمتر از حد مجاز داشته است", //16

            "دارای دما نرمال شده است", //17

            "عدد تایمر صفر شده است", //18

            "دما بیش از حد مجاز داشته است", //19

            "دما کمتر از حد مجاز داشته است", //20

            "دارای دما نرمال شده است", //21

            "دما بیش از حد مجاز داشته است", //22

            "دما کمتر از حد مجاز داشته است", //23

            "دارای دما نرمال شده است", //24

            "دما بیش از حد مجاز داشته است", //25

            "دما کمتر از حد مجاز داشته است", //26

            "دارای دما نرمال شده است", //27

            "استریلش با موفقیت به پایان رسیده است", //28

            "استریلش ناقص به پایان رسیده است", //29

            "استریل شروع به کار کرده است", //30

            "پخت با موفقیت به پایان رسیده است", //31

            "پخت ناقص به پایان رسیده است", //32

            "پخت شروع به کار کرده است", //33

            "ارتباط با موفقیت انجام شد", //34

            "ارتباط قطع شد", //35

            "ارتباط با بانک اطلاعاتی با موفقیت انجام شد", //36
        
            "قادر به برقراری ارتباط با بانک اطلاعاتی نمی باشد", //37
            
            "روغن ریز غیرفعال شد",//38
            
            "روغن ریز فعال شد",//39
            
            "دماسنج 1 غیر فعال شد",//40
            
            "دماسنج 1  فعال شد",//41
            
            "دماسنج 2 غیر فعال شد",//42
            
            "دماسنج 2  فعال شد",//43

            "مشکل در ارتباط با اتوکلاو",//44

            "خروج از نرم افزار",//45

            "شروع به کار نرم افزار",//46 

            "قطع ارتباط با پورت توسط اپراتور",//47

            "برقراری ارتباط با پورت توسط اپراتور",//48

            "درخواست ارتباط با پورت  توسط اپراتور",//49
            
            "دماسنج فعال شد." , //50

            "دماسنج غیر فعال شد" , //51

            "دماسنج دارای دمای بیش از حد شده است." , //52

            "دماسنج دارای دمای نرمال شده است" , //53

            "دماسنج دارای دمای کمتر از حد مجاز شده است" , //54

            "ارتباط با دماسنج دچار مشکل شده است." , //55

            "اتوکلاو در حال فرآیند خنک کردن." , //56

            "پورت دچار مشکل شده است" , //57

            "ارتباط مجدد برقرار شد" , //58

            "کانتر رست شد" , //59

            "خطا در کانتر بسته بندی" , //60 counter baste bandi

            "خطا در کانتر قوطی خالی", // 61 cunter ghoti 
            
            
            "کانتر بسته بندی غیر فعال شد" , //62 counter baste bandi

            "کانتر قوطی خالی غیرفعال شد", // 63 cunter ghoti 

            
            "کانتر بسته بندی فعال شد." , //64 counter baste bandi

            "کانتر قوطی خالی فعال شد.", // 65 cunter ghoti 

            "تغییر اطلاعات قبل استریل" ,// 66 change zarin ghabl

            "تغییر اطلاعات بعد استریل" ,// 67 change zarin bad 
        };

        #endregion


    }

    #endregion

}
