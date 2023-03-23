# HiStac2Plugin

SkySonde plugin for the LASP instrument configuration 
consisting of a Heated Inlet and 2x STACs:

Heated Inlet -> STAC2 -> STAC1

|||
|-|-|
|Instrument ID & Daisy Chain Number | 4102 |

## XData Format

| Type | Quantity | Descrption |
|------|----------|------------|
||  **1st STAC with reduced data** ||
| uint16 |OPC_300 |300nm Bin |
| uint8  |OPC_300 - OPC_500| 500nm difference |
| uint8  |OPC_500 - OPC_700| 700nm difference |
| uint8  |OPC_700 - OPC_1000| 1000nm difference | 
| uint8  |OPC_1000 - OPC_3000| 3000nm Bin |
| uint8  |IPump1| Pump1 current in mA |
| uint8  |IPump2)| Pump1 current in mA |
| uint8  |(TempPump1 + TempPump2) / 2.0 + 100.0)|
| uint8  |VBat* 10.0)| Battery voltage * 10 |
| uint16 |short(TempCN* 100.0) | Saturator T |
| uint8  |TempIce + 10.0) * 10 | Ice Jacket T |
| uint8  |alt_status| Status byte for altitude source |
||  **2nd STAC with reduced data** ||
| uint16 |CNC_300| 300nm Bin |
| uint8  |(CNC_300 - CNC_500)| 500nm Difference |
| uint8  |(CNC_500 - CNC_700)| 700nm Diffenence |
| uint8  |(CNC_700 - CNC_1000)| 1000nm Diffenence |
| uint8  |(CNC_1000 - CNC_3000)| 3000nm Diffenence |
| uint8  |CN_IPump1| Pump1 current in mA |
| uint8  |CN_IPump2| Pump1 current in mA |
| uint8  |(CN_TempPump1 + CN_TempPump2) / 2.0 + 100.0)| |
| uint8  |CN_VBat |Battery voltage * 10 |
| uint16 |CN_TempCN* 100.0) |Saturator T |
| uint8  |(CN_TempIce + 10.0) * 10)| Condenser T 1 |
||  **Heated Inlet** ||
| uint8  |CN_T_UpStream| |
| uint8  |CN_T_Middle| |
| uint8  |CN_T_DownStream| |
| uint8  |CN_HI_V_Battery * 10.0)| |
