# HiStac2Plugin

SkySonde plugin for the LASP instrument configuration 
consisting of a Heated Inlet and 2x STACs:

Heated Inlet -> STAC2 -> STAC1

|||
|-|-|
|Instrument ID & Daisy Chain Number | 4102 |

## XData Format

| n|Bytes|  Type | Parameter | Descrption         |
|--|-----|-------|-----------|--------------------|
|  |     |       |**1st STAC with reduced data** ||
| 1| 0-1 |uint16 |OPC_300 |300nm Bin |
| 2|  2  |uint8  |OPC_300 - OPC_500| 500nm difference |
| 3|  3  |uint8  |OPC_500 - OPC_700| 700nm difference |
| 4|  4  |uint8  |OPC_700 - OPC_1000| 1000nm difference | 
| 5|  5  |uint8  |OPC_1000 - OPC_3000| 3000nm Bin |
| 6|  6  |uint8  |IPump1| Pump1 current in mA |
| 7|  7  |uint8  |IPump2)| Pump1 current in mA |
| 8|  8  |uint8  |(TempPump1 + TempPump2) / 2.0 + 100.0)|
| 9|  9  |uint8  |VBat* 10.0| Battery voltage * 10 |
|10|10-11|uint16 |short(TempCN* 100.0) | Saturator T |
|11| 12  |uint8  |(TempIce + 10.0) * 10 | Ice Jacket T |
|12| 13  |uint8  |alt_status| Status byte for altitude source |
|  |     |       | **2nd STAC with reduced data** ||
|13|14-15|uint16 |CNC_300| 300nm Bin |
|14|  16 |uint8  |(CNC_300 - CNC_500)| 500nm Difference |
|15|  17 |uint8  |(CNC_500 - CNC_700)| 700nm Diffenence |
|16|  18 |uint8  |(CNC_700 - CNC_1000)| 1000nm Diffenence |
|17|  19 |uint8  |(CNC_1000 - CNC_3000)| 3000nm Diffenence |
|18|  20 |uint8  |CN_IPump1| Pump1 current in mA |
|19|  21 |uint8  |CN_IPump2| Pump1 current in mA |
|20|  22 |uint8  |(CN_TempPump1 + CN_TempPump2) / 2.0 + 100.0)| |
|21|  23 |uint8  |CN_VBat |Battery voltage * 10 |
|22|24-25|uint16 |CN_TempCN* 100.0) |Saturator T |
|23|  26 |uint8  |(CN_TempIce + 10.0) * 10)| Condenser T 1 |
|  |     |       |**Heated Inlet** ||
|24|  27 |uint8  |CN_T_UpStream| |
|25|  28 |uint8  |CN_T_Middle| |
|26|  29 |uint8  |CN_T_DownStream| |
|27|  30 |uint8  |CN_HI_V_Battery * 10.0)| |
