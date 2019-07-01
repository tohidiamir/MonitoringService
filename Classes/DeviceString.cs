﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitoringService.Classes
{
    class DeviceString
    {
        #region Static Device String

        public static byte[][] WriteDevice = {
    
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 01 temp
  
        
         new byte[]{
                  0x3A,
                  0x30,
                  0x31,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x42,
                  0x33,
                  0x0D,
                  0x0A,

                },

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 02 pres
         new byte[]{
                  0x3A,
                  0x30,
                  0x32,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x42,
                  0x32,
                  0x0D,
                  0x0A,

                },
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 03 temp
         new byte[]{
                  0x3A,
                  0x30,
                  0x33,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x42,
                  0x31,
                  0x0D,
                  0x0A,

                },

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 04 pres
         new byte[]{
                  0x3A,
                  0x30,
                  0x34,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x42,
                  0x30,
                  0x0D,
                  0x0A,

                },
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 05 temp
         new byte[]{
                  0x3A,
                  0x30,
                  0x35,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x41,
                  0x46,
                  0x0D,
                  0x0A,

                },

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 06 pres
         new byte[]{
                  0x3A,
                  0x30,
                  0x36,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x41,
                  0x45,
                  0x0D,
                  0x0A,

                },
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 07 temp
        new byte[]{
                  0x3A,
                  0x30,
                  0x37,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x41,
                  0x44,
                  0x0D,
                  0x0A,

                },

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 08 pres
         new byte[]{
                  0x3A,
                  0x30,
                  0x38,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x41,
                  0x43,
                  0x0D,
                  0x0A,

                },
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 09 temp
        new byte[]{
                  0x3A,
                  0x30,
                  0x39,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x41,
                  0x42,
                  0x0D,
                  0x0A,

                },

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 10 pres
        new byte[]{
                  0x3A,
                  0x30,
                  0x41,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x41,
                  0x41,
                  0x0D,
                  0x0A,

                },
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 11 temp
        new byte[]{
                  0x3A,
                  0x30,
                  0x42,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x41,
                  0x39,
                  0x0D,
                  0x0A,

                },

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 12 pres
        new byte[]{
                  0x3A,
                  0x30,
                  0x43,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x41,
                  0x38,
                  0x0D,
                  0x0A,

                },
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 13 temp
        new byte[]{
                  0x3A,
                  0x30,
                  0x44,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x41,
                  0x37,
                  0x0D,
                  0x0A,

                },

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 14 pres
        new byte[]{
                  0x3A,
                  0x30,
                  0x45,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x41,
                  0x36,
                  0x0D,
                  0x0A,

                },
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 15 temp
        new byte[]{
                  0x3A,
                  0x30,
                  0x46,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x41,
                  0x35,
                  0x0D,
                  0x0A,

                },

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 16 pres
        new byte[]{
                  0x3A,
                  0x31,
                  0x30,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x41,
                  0x34,
                  0x0D,
                  0x0A,

                },
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 17 temp
        new byte[]{
                  0x3A,
                  0x31,
                  0x31,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x41,
                  0x33,
                  0x0D,
                  0x0A,

                },

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 18 pres
         new byte[]{
                  0x3A,
                  0x31,
                  0x32,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x41,
                  0x32,
                  0x0D,
                  0x0A,

                },
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 19 temp
        new byte[]{
                  0x3A,
                  0x31,
                  0x33,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x41,
                  0x31,
                  0x0D,
                  0x0A,

                },

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 20 pres
        new byte[]{
                  0x3A,
                  0x31,
                  0x34,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x41,
                  0x30,
                  0x0D,
                  0x0A,

                },
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 21 temp
         new byte[]{
                  0x3A,
                  0x31,
                  0x35,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x39,
                  0x46,
                  0x0D,
                  0x0A,

                },

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 22 pres
        new byte[]{
                  0x3A,
                  0x31,
                  0x36,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x39,
                  0x45,
                  0x0D,
                  0x0A,

                },
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 23 temp
        new byte[]{
                  0x3A,
                  0x31,
                  0x37,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x39,
                  0x44,
                  0x0D,
                  0x0A,

                },

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 24 pres
         new byte[]{
                  0x3A,
                  0x31,
                  0x38,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x39,
                  0x43,
                  0x0D,
                  0x0A,

                },
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 25 temp
         new byte[]{
                  0x3A,
                  0x31,
                  0x39,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x39,
                  0x42,
                  0x0D,
                  0x0A,

                },

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 26 pres
        new byte[]{
                  0x3A,
                  0x31,
                  0x41,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x39,
                  0x41,
                  0x0D,
                  0x0A,

                },
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 27 temp
        new byte[]{
                  0x3A,
                  0x31,
                  0x42,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x39,
                  0x39,
                  0x0D,
                  0x0A,

                },

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 28 pres
         new byte[]{
                  0x3A,
                  0x31,
                  0x43,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x39,
                  0x38,
                  0x0D,
                  0x0A,

                },
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 29 temp
        new byte[]{
                  0x3A,
                  0x31,
                  0x44,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x39,
                  0x37,
                  0x0D,
                  0x0A,

                },

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 30 pres
        new byte[]{
                  0x3A,
                  0x31,
                  0x45,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x39,
                  0x36,
                  0x0D,
                  0x0A,

                },
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 31 temp
        new byte[]{
                  0x3A,
                  0x31,
                  0x46,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x39,
                  0x35,
                  0x0D,
                  0x0A,

                },

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //device 32 pres
        new byte[]{
                  0x3A,
                  0x32,
                  0x30,
                  0x30,
                  0x33,
                  0x34,
                  0x37,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x30,
                  0x32,
                  0x39,
                  0x34,
                  0x0D,
                  0x0A,

                }
        };


        #endregion
    }
}