using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonitoringService.Enum
{

    #region Enum Error 

    enum EnumError
    {
      
        publicError,
        
        openSerialPortProblem,

        openserialportsuccess,

        closeportproblem,

        closePortsuccess,

        opensqlconnectproblem,

        opensqlconnecsuccess,

        closesqlconnectproblem,

        closesqlconnectsuccess,

        readFromAutoclacProblem,

        readFromPokhtProblem,

        insertNewLogProbel,

        insertnewDatalogforautoclav,

        insertnewstartautoclavinsertdatabaseerror,

        updatePeriodAutoclavZeroError,

        updatePeriodAutoclavEndNaghes,

        updatePeriodAutoclavEndError,

        ProblemDeactiveAll,

        ProblemDeactiveAutoclav,

        ProblemDeactiveBaking , 
        
        ProblemActiveAll,

        ProblemActiveAutoclav,

        ProblemActiveBaking ,

        insertnewDatalogforDevOther


        
    }

    #endregion


    #region Class Enum Error 

    class ClassEnumError
    {


        public static void ShowErrorInMessageBox(Enum.EnumError error)
        {
            //MessageBox.Show(ShowError(error),"");
        }


        public static string ShowError(EnumError error)
        {
            switch (error)
            { 
                case EnumError.openSerialPortProblem:
                    return ErrorMessage[1];

                case EnumError.insertNewLogProbel:
                    return ErrorMessage[2];

                case EnumError.insertnewDatalogforautoclav:
                    return ErrorMessage[3];

                case EnumError.insertnewstartautoclavinsertdatabaseerror:
                    return ErrorMessage[4];

                case EnumError.updatePeriodAutoclavZeroError:
                    return ErrorMessage[5];

                case EnumError.updatePeriodAutoclavEndNaghes:
                    return ErrorMessage[6];

                case EnumError.updatePeriodAutoclavEndError:
                    return ErrorMessage[7];

                case EnumError.readFromAutoclacProblem:
                    return ErrorMessage[8];

                case EnumError.readFromPokhtProblem:
                    return ErrorMessage[9];

                case EnumError.ProblemDeactiveAll:
                    return ErrorMessage[10];
                    
                case EnumError.ProblemDeactiveAutoclav:
                    return ErrorMessage[11];

                case EnumError.ProblemDeactiveBaking:
                    return ErrorMessage[12];

                case EnumError.ProblemActiveAll:
                    return ErrorMessage[13];

                case EnumError.ProblemActiveAutoclav:
                    return ErrorMessage[14];

                case EnumError.ProblemActiveBaking:
                    return ErrorMessage[15];

                case EnumError.insertnewDatalogforDevOther:
                    return ErrorMessage[16];

                default:
                    return ErrorMessage[0];

            }

        }

        #region String Array Error 

        private static string[] ErrorMessage = 
        { 
            "Error ", //public
            
            "مشکل در برقراری ارتباط با دستگاه ها - کابل ها و سیستم اتصال به دستگاه بررسی شود", //open serial port
            
            "Problem Insert Log To Data Base", 
            
            "Problem Insert Data autoclav In Data Base ", 
            
            "Problem Insert Data to start autoclav" ,

            "Problem Update TIme Zero Autoclav", //zero autoclav
            
            "Problem Update Period Incorrect Autoclav", //End naghes auto
            
            "Problem Update Period correct Autoclav", // End Endauto
        
            "Read From Autoclav Problem",

            "Read From Baking Problem",

            "Probelm To Deactive All Device",

            "Problem To Deactive Autoclav",

            "Problem To Deactive Baking",

            "Probelm To Active All Device",

            "Problem To Active Autoclav",

            "Problem To Active Baking",

            "Problelm INsert Dev Other"
        };

        #endregion

    }

#endregion
}
