# STAC X-Data Arduino Code

_For specifying SkySonde client plugins._

This is from an email sent by Lars, 21 March 2023. It is the code that
the STAC and Heated Inlet instruments use to generate the X-Data 
character stream. These data are delivered to the IMet radiosonde.

Three instrument configurations are supported:
  1. A single STAC
  2. A STAC and a Heated Inlet
  3. Two STACs and a Heated Inlet

```C++
void  SendXData(Stream &port) {
 //Instrument ID "41"  Again, just made this up
 String header = "xdata=4102";
 IMET_SERIAL.print(header);

 if (!XDataSTACInputAvailable)
 {
   /* Xdata format for stand alone STAC without a heated inlet or 2nd STAC
       xdata=4102  - instriment ID and daisy chain number
       AAAA - OPC_300 (2 bytes, 0 - 65535)
       BBBB - OPC_500 (2 bytes, 0 - 65535)
       CCCC - OPC_700 (2 bytes, 0 - 65535)
       DDDD - OPC_1000 (2 bytes, 0 - 65535)
       EEEE - OPC_2000 (2 bytes, 0 -65535)
       FFFF - OPC_2500 (2 bytes, 0 -65535)
       GGGG - OPC_3000 (2 bytes, 0 -65535)
       HHHH - OPC_5000 (2 bytes, 0 -65535)
       II - Pump 1 PWM drive (%) (0 - 100)
       JJ - Pump 2 PWM Drive (%) (0 - 100)
       KK - Pump 1 Current mA  (0 - 255mA)
       LL - Pump 2 Current mA  (0 - 255mA)
       MM - Battery Voltage - voltage multiplied by 10 then converted to unsigned short (0 - 25.5V)
       NN - Saturator Temperature degress C /100 = (0 - 655.53 C)
       OO - Ice Jacket Temp C + 100.0  (-100 - 155C)
       PP - PCB Temp (0 - 255)
       QQ - Pump1 Temp (-100 - 125)
       RR - Pump2 Temp (-100 - 125)
       SS - Satruator output %
       TT - Altitude status byte
       UU - Dilution ratio (1 - 16)
   */

   printFixedHex(port, OPC_300, 2); //300nm Bin
   printFixedHex(port, OPC_500, 2); //500nm Bin
   printFixedHex(port, OPC_700, 2); //700nm Bin
   printFixedHex(port, OPC_1000, 2); //1000nm Bin
   printFixedHex(port, OPC_2000, 2); //2000nm Bin
   printFixedHex(port, OPC_2500, 2); //2500nm Bin
   printFixedHex(port, OPC_3000, 2); //3000nm Bin
   printFixedHex(port, OPC_5000, 2); //5000nm Bin
   printFixedHex(port, byte(int(float(BEMF1_pwm) / 1024.0 * 100.0)), 1); //Pump1 PWM Drive
   printFixedHex(port, byte(int(float(BEMF2_pwm) / 1024.0 * 100.0)), 1); //Pump2 PWM Drive
   printFixedHex(port, byte(IPump1), 1); //Pump1 current in mA
   printFixedHex(port, byte(IPump2), 1); //Pump1 current in mA
   printFixedHex(port, byte(VBat * 10.0), 1); //Battery voltage *10
   printFixedHex(port, short(TempCN * 100.0), 2); //Saturator T
   printFixedHex(port, byte(TempIce + 100.0), 1); //Ice Jacket T
   printFixedHex(port, byte(TempPCB + 100.0), 1); //PCB T
   printFixedHex(port, byte(TempPump1 + 100.0), 1); //Pump 1 Temp
   printFixedHex(port, byte(TempPump2 + 100.0), 1); //Pump 2 Temp
   printFixedHex(port, byte(int(float(SaturatorOutput) / 1024.0 * 100.0)), 1); //Saturator heater output
   printFixedHex(port, byte(alt_status), 1); //Status byte for altitude source
   printFixedHex(port, byte(int(DilutionFactor)), 1); //Status byte for altitude source

   //If there is a Heated inlet connected to the upstream serial port, add that to the XData
   if (XDataHIInputAvailable)
   {
     printFixedHex(port, byte(T_UpStream), 1);  //temperaure closest to the inlet (0-255 in C)
     printFixedHex(port, byte(T_Middle), 1); //middle temperature (0-255 in C)
     printFixedHex(port, byte(T_DownStream), 1); //temperautre closest to outlet
     printFixedHex(port, byte(HI_V_Battery*10.0), 1); //Battery V * 10
     XDataHIInputAvailable = false;
     HI_Data_Available = true;
   }
 }
 /* If there is a second STAC and heated inlet connected to the upstream serial port,
    then we are short on iMet bytes so we need to reduce size of both 1st and 2nd STAC data
    packets before adding STAC and heated inlet data from 2nd STAC
 */
 if (XDataSTACInputAvailable)
 {
   /* 1st STAC with reduced data */
   printFixedHex(port, OPC_300, 2); //300nm Bin
   printFixedHex(port, OPC_300 - OPC_500, 1); //500nm difference
   printFixedHex(port, OPC_500 - OPC_700, 1); //700nm difference
   printFixedHex(port, OPC_700 - OPC_1000, 1); //1000nm difference
   printFixedHex(port, OPC_1000 - OPC_3000, 1); //3000nm Bin
   printFixedHex(port, byte(IPump1), 1); //Pump1 current in mA
   printFixedHex(port, byte(IPump2), 1); //Pump1 current in mA
   printFixedHex(port, byte((TempPump1 + TempPump2) / 2.0 + 100.0), 1);
   printFixedHex(port, byte(VBat * 10.0), 1); //Battery voltage *10
   printFixedHex(port, short(TempCN * 100.0), 2); //Saturator T
   printFixedHex(port, byte(TempIce + 10.0) * 10, 1); //Ice Jacket T
   printFixedHex(port, byte(alt_status), 1); //Status byte for altitude source

   /* 2nd STAC with reduced data */
   printFixedHex(port, CNC_300, 2); //300nm Bin
   printFixedHex(port, (CNC_300 - CNC_500), 1); //500nm Difference
   printFixedHex(port, (CNC_500 - CNC_700), 1); //700nm Diffenence
   printFixedHex(port, (CNC_700 - CNC_1000), 1); //1000nm Diffenence
   printFixedHex(port, (CNC_1000 - CNC_3000), 1); //3000nm Diffenence
   printFixedHex(port, byte(CN_IPump1), 1); //Pump1 current in mA
   printFixedHex(port, byte(CN_IPump2), 1); //Pump1 current in mA
   printFixedHex(port, byte((CN_TempPump1 + CN_TempPump2) / 2.0 + 100.0), 1);
   printFixedHex(port, byte(CN_VBat), 1); //Battery voltage *10
   printFixedHex(port, short(CN_TempCN * 100.0), 2); //Saturator T
   printFixedHex(port, byte((CN_TempIce + 10.0) * 10), 1); //Condenser T 1

   /* Heated Inlet */
   printFixedHex(port, byte(CN_T_UpStream), 1);
   printFixedHex(port, byte(CN_T_Middle), 1);
   printFixedHex(port, byte(CN_T_DownStream), 1);
   printFixedHex(port, byte(CN_HI_V_Battery*10.0), 1);

   DEBUG_SERIAL.println("Data from 2nd STAC:");
   DEBUG_SERIAL.print("CNC 300: "); DEBUG_SERIAL.println(CNC_300);
   DEBUG_SERIAL.print("CNC 500: "); DEBUG_SERIAL.println(CNC_500);
   DEBUG_SERIAL.print("CNC 700: "); DEBUG_SERIAL.println(CNC_700);
   DEBUG_SERIAL.print("CNC IPump1: "); DEBUG_SERIAL.println(CN_IPump1);
   DEBUG_SERIAL.print("CNC IPump2: "); DEBUG_SERIAL.println(CN_IPump2);
   DEBUG_SERIAL.print("CNC Battert: "); DEBUG_SERIAL.println(CN_VBat);
   DEBUG_SERIAL.print("CNC TempCN: "); DEBUG_SERIAL.println(CN_TempCN);
   DEBUG_SERIAL.print("HI Temp: "); DEBUG_SERIAL.println(CN_T_Middle);

   XDataSTACInputAvailable = false;

 }


 port.write('\r');
 port.write('\n');

}

void printFixedHex(Stream &port, unsigned short val, int bytes)
{
 if (bytes == 2)
 {
   if (val < 0x10)
     port.print("0");
   if (val < 0x100)
     port.print("0");
   if (val < 0x1000)
     port.print("0");
   port.print(val, HEX);
 }
 else if (bytes == 1)
 {
   if (val > 0xFF)
   {
     port.print("FF");
     return;
   }
   else if (val < 0x10)
     port.print("0");

   port.print(val, HEX);
 }
}
```
