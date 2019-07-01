using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace MonitoringService.Classes
{
    class PLC
    {

        private static string m_port = Varibale.portname1;
        //port 
        private static int m_baud = 38400;
        private static int m_dataBits = 7;


        public static string Read(string write , string type)
        {
            if (Varibale.reciveData_STATUS)
            {
                try
                {
                    string readBuffer = "";
                    string sendWord = write;

                    string hexTotal = checkSum(sendWord);

                    /* if (CreateSerialPort.serialPort_1.IsOpen)
                     {
                         CreateSerialPort.serialPort_1.Dispose();
                         CreateSerialPort.serialPort_1.Close();
                     }
                     */
                    CreateSerialPort.CLOSESERIAL("PLC CLASS", "PLCE READ NOT CLOSE");

                    string writeBuffer = string.Concat(":", sendWord, hexTotal, "\r");

                    CreateSerialPort.serialPort_1.Parity = Parity.Even;
                    CreateSerialPort.serialPort_1.StopBits = StopBits.One;
                    CreateSerialPort.serialPort_1.PortName = m_port;
                    CreateSerialPort.serialPort_1.BaudRate = m_baud;
                    CreateSerialPort.serialPort_1.DataBits = m_dataBits;
                    CreateSerialPort.serialPort_1.ReadTimeout = 200;
                    CreateSerialPort.serialPort_1.WriteTimeout = 200;

                    try
                    {

                        CreateSerialPort.serialPort_1.Open();
                        CreateSerialPort.serialPort_1.WriteLine(writeBuffer);

                        try
                        {
                            readBuffer = CreateSerialPort.serialPort_1.ReadLine();
                            CreateSerialPort.serialPort_1.Close();
                        }
                        catch (System.Exception ex)
                        {
                            log_system.saveLogSystem(ex, "plc read " + type);
                            CreateSerialPort.CLOSESERIAL("PLC CLASS catch", "PLCE READ NOT CLOSE 5");
                        }

                        if (CreateSerialPort.serialPort_1.IsOpen)
                        {
                            CreateSerialPort.CLOSESERIAL("PLC CLASS catch", "PLCE READ NOT CLOSE 56");
                        }
                    }
                    catch (Exception ex)
                    {
                        CreateSerialPort.CLOSESERIAL("PLC CLASS catch", "PLCE READ NOT CLOSE");
                        log_system.saveLogSystem(ex, "plc read master " + type);

                      

                        return "";

                    }

                    return readBuffer;
                
            
                }
                catch (Exception ex)
                {
                    CreateSerialPort.CLOSESERIAL("PLC CLASS catch", "PLCE READ  KOLL NOT CLOSE");
                    log_system.saveLogSystem(ex, "plc read kol " + type);

                    return "";
                }
            }
            else
            {
                CreateSerialPort.CLOSESERIAL("plc read "+type+" not close loop", "PLCE READ NOT CLOSE");
                return "";
            }
        }

        #region Convert to hex
        public static string toHex(int val)
        {
            int a, b;
            int decVal = val;
            string hexByte = "", hexTotal = "";
            double i;

            for (i = 0; decVal > 0; i++)
            {
                b = Convert.ToInt32(System.Math.Pow(16.0, i));
                a = decVal % 16;
                decVal /= 16;
                if (a <= 9)
                    hexByte = a.ToString();
                else
                {
                    switch (a)
                    {
                        case 10:
                            hexByte = "A";
                            break;
                        case 11:
                            hexByte = "B";
                            break;
                        case 12:
                            hexByte = "C";
                            break;
                        case 13:
                            hexByte = "D";
                            break;
                        case 14:
                            hexByte = "E";
                            break;
                        case 15:
                            hexByte = "F";
                            break;
                    }
                }
                hexTotal = String.Concat(hexByte, hexTotal);
            }
            for (; i < 4.0; i++)
                hexTotal = String.Concat("0", hexTotal);

            return hexTotal;
        }
        #endregion

        #region CheckSum
        public static string checkSum(string writeUncheck)
        {
            char[] hexArray = new char[writeUncheck.Length];
            hexArray = writeUncheck.ToCharArray();
            int decNum = 0, decNumMSB = 0, decNumLSB = 0;
            int decByte, decByteTotal = 0;

            bool msb = true;

            for (int t = 0; t <= hexArray.GetUpperBound(0); t++)
            {
                if ((hexArray[t] >= 48) && (hexArray[t] <= 57))

                    decNum = (hexArray[t] - 48);

                else if ((hexArray[t] >= 65) & (hexArray[t] <= 70))
                    decNum = 10 + (hexArray[t] - 65);

                if (msb)
                {
                    decNumMSB = decNum * 16;
                    msb = false;
                }
                else
                {
                    decNumLSB = decNum;
                    msb = true;
                }
                if (msb)
                {
                    decByte = decNumMSB + decNumLSB;
                    decByteTotal += decByte;
                }
            }

            decByteTotal = (255 - decByteTotal) + 1;
            decByteTotal = decByteTotal & 255;

            int a, b = 0;

            string hexByte = "", hexTotal = "";
            double i;

            for (i = 0; decByteTotal > 0; i++)
            {
                b = Convert.ToInt32(System.Math.Pow(16.0, i));
                a = decByteTotal % 16;
                decByteTotal /= 16;
                if (a <= 9)
                    hexByte = a.ToString();
                else
                {
                    switch (a)
                    {
                        case 10:
                            hexByte = "A";
                            break;
                        case 11:
                            hexByte = "B";
                            break;
                        case 12:
                            hexByte = "C";
                            break;
                        case 13:
                            hexByte = "D";
                            break;
                        case 14:
                            hexByte = "E";
                            break;
                        case 15:
                            hexByte = "F";
                            break;
                    }
                }
                hexTotal = String.Concat(hexByte, hexTotal);
            }


            return hexTotal;




        }


        #endregion

        #region To String

        public static double toDec(string ch, int start, int numOfBytes)
        {
            char[] hexArray = new char[ch.Length];
            hexArray = ch.ToCharArray();

            double decTotal = 0;
            int x = numOfBytes - 1;

            int upper = hexArray.GetUpperBound(0);
            if (upper >= start + x)
            {
                for (int i = 0; i < numOfBytes; i++)
                {
                    int j = start + i;
                    if ((ch[j] >= 48) && (ch[j] <= 57))
                        decTotal += (ch[j] - 48) * System.Math.Pow(16.0, Convert.ToDouble(x));
                    else if ((ch[j] >= 65) && (ch[j] <= 70))
                        decTotal += (ch[j] - 55) * System.Math.Pow(16.0, Convert.ToDouble(x));


                    x--;
                }
            }
            return decTotal;

        }



        #endregion

    }
}
