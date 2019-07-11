using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using MonitoringService.Classes;

namespace MonitoringService
{
    public partial class MasterService : ServiceBase
    {
        Timer timer = new Timer();
        
        public MasterService()
        {
            InitializeComponent();
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            //eventLog1 = new System.Diagnostics.EventLog();
            //if (!System.Diagnostics.EventLog.SourceExists("MySource"))
            //{
            //System.Diagnostics.EventLog.CreateEventSource(
            //      "MySource", "MyNewLog");
            //}
            //eventLog1.Source = "MySource";
            //eventLog1.Log = "MyNewLog";
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
                customCulture.NumberFormat.NumberDecimalSeparator = ".";
                System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

                Varibale.VaribleSetting();

                CreateSerialPort cs = new CreateSerialPort();

                log_system.saveLogMsg("Start Log ");

                timer.Interval = 1000; // 1 seconds
                timer.Elapsed += Timer_Elapsed;
                timer.Start();
            }
            catch (Exception ex)
            {
                log_system.saveLogSystem(ex, "on start");
            }
            
        }

       

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (this)
            {
                try
                {
                

                    Varibale var = new Varibale();


                    if (Varibale.level_Fetch == 0)
                    {
                        //log_system.saveLogMsg("counter ");
                        Varibale.level_Fetch++;
                        DeviceCounter.deviceCounterConnect();

                    }
                    else if (Varibale.level_Fetch == 1)
                    {
                        //log_system.saveLogMsg("autoclav");
                        Varibale.level_Fetch++;
                        Autoclav.ReadFromAutoclav();

                    }
                    else if (Varibale.level_Fetch == 2)
                    {
                        //log_system.saveLogMsg("frig");
                        Varibale.level_Fetch = 0;
                        DevOther.DevOtherConnect();

                    }
                    else
                    {
                        Varibale.level_Fetch = 0;
                    }


                    PublicMehotd.SetDataToDatabase("last_recive", PublicMehotd.RetStringPersianCalender() + " " + PublicMehotd.RetStringLocalTime());
                    PublicMehotd.SetDataToDatabase("last_recive_int", PublicMehotd.time_php());


                    //    //frmIndex.st_lbl_comment.Text = "ارتباط برقرار است.";

                    if (Varibale.portError)
                    {
                        LogManager log = new LogManager(1, Enum.EnumKindDevice.system, Enum.EnumLevel.high, Enum.EnumLOg.SerialPort_request_connect_Success, "*");
                        Varibale.portError = false;
                        Varibale.portErrorNumber = 0;
                        //frmIndex.st_pbx_DisconnectDevice.Visible = false;
                        //frmIndex.st_pbx_connectDevice.Visible = true;
                    }

                    CreateSerialPort.CLOSESERIAL("TIMER 1", "PLCE READ NOT CLOSE");


                }
                catch (Exception ex)
                {
                    log_system.saveLogSystem(ex, "serial port timer connect " + Varibale.devicenumber_autoclav);
                    Varibale.portErrorNumber++;

                    if (!Varibale.portError)
                    {
                        Varibale.portError = true;
                        LogManager log = new LogManager(1, Enum.EnumKindDevice.system, Enum.EnumLevel.high, Enum.EnumLOg.SerialPort_request_connect_ERROR, "*");
                        //frmIndex.st_pbx_DisconnectDevice.Visible = true;
                        //frmIndex.st_pbx_connectDevice.Visible = false;
                    }
                    //frmIndex.st_lbl_comment.Text = "پورت مشکل دارد" + " خطا " + Varibale.portErrorNumber.ToString();



                    CreateSerialPort.CLOSESERIAL("TIMER 2", "PLCE READ NOT CLOSE");


                    if (Varibale.portErrorNumber > 5)
                    {
                        //PublicMehotd.DeactiveAllDevice();
                        //PublicMehotd.DisconnectToDeviceSerialPort(frmIndex.st_tstrip_message);

                        //LogManager log = new LogManager(0, Enum.EnumKindDevice.system, Enum.EnumLevel.high, Enum.EnumLOg.SerialPort_disconnect_operator, "*");
                        //frmIndex.st_pbx_DisconnectDevice.Visible = true;
                        //frmIndex.st_pbx_connectDevice.Visible = false;
                        log_system.saveLogSystem(ex, "serial port error a lot of  ");
                    }
                }
            }
        }

        protected override void OnStop()
        {
            log_system.saveLogSystem("","" ,"", "stop service  ");
            CreateSerialPort.CLOSESERIAL("on stop serialport ", "bastan serial port  on stop ");
        }

        private void eventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }
    }
}
