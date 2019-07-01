using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using MonitoringService.Enum;
using MonitoringService;

namespace MonitoringService.Classes
{
    class CreateSerialPort
    {
        public static SerialPort serialPort_1;

        #region Construction Serial Port

        public CreateSerialPort()
        {

            CreateNewSerialPort_1();
        }

        #endregion

        #region Serial Port 1

        private void CreateNewSerialPort_1()
        {
            //MessageBox.Show(Varibale.parity1.ToString());
            serialPort_1 = new SerialPort();

            serialPort_1.BaudRate = Varibale.buadrate1;

            serialPort_1.Parity = Varibale.parity1;
            
            serialPort_1.PortName = Varibale.portname1;

            serialPort_1.ReadTimeout = 200;

            serialPort_1.WriteTimeout = 200;

            serialPort_1.DataBits = 8;

            serialPort_1.StopBits = StopBits.One;

            serialPort_1.ParityReplace = 63;

            serialPort_1.ReadBufferSize = 2048;

            serialPort_1.WriteBufferSize = 2048;

            serialPort_1.Handshake = Handshake.None;

            serialPort_1.DiscardNull = true;
        }

        public void disposeserial(object sender, EventArgs e)
        { 
        
        }

        public static bool OpenSerialPort_1()
        {
            try
            {
                CreateSerialPort.serialPort_1.Dispose(); 
                serialPort_1.Close();
                serialPort_1.Open();
                
                if (serialPort_1.IsOpen)
                {
                    return true;
                }
                else
                {
                    
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                ClassEnumError.ShowErrorInMessageBox(EnumError.openSerialPortProblem);
                log_system.saveLogSystem(ex, "serialport-open");
                return false;
            }
        }

        public static bool CloseSerialPort_1()
        {
            try
            {
                serialPort_1.Close();
                serialPort_1.Dispose();

                if (!(serialPort_1.IsOpen))
                {
                    PublicMehotd.DeactiveAllDevice();
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                ClassEnumError.ShowErrorInMessageBox(EnumError.closeportproblem);
                log_system.saveLogSystem(ex, "serialport-close");
                return false;
            }
        }
        #endregion

        public static void CLOSESERIAL(String part , String msg)
        {
            Varibale.reciveData_STATUS = false;
           


            CreateSerialPort.serialPort_1.Dispose();
            CreateSerialPort.serialPort_1.Close();

            if (CreateSerialPort.serialPort_1.IsOpen)
            {
                    log_system.saveLogSystem("no-close", part, msg, "");
              
                    CreateSerialPort.serialPort_1.Dispose();
                    CreateSerialPort.serialPort_1.Close();

            }

            if (CreateSerialPort.serialPort_1.IsOpen)
            {
                log_system.saveLogSystem("close serial end error", part, msg, "");
                Varibale.reciveData_STATUS = true;
            }
            else
            {
                Varibale.reciveData_STATUS = true;
                
            }
            
        }
    }
}
