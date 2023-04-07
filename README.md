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
|  |     |       |**1st STAC (unheated) with reduced data** ||
| 1| 0-1 |uint16 |OPC_300 |300nm Bin |
| 2|  2  |uint8  |OPC_300 - OPC_500| 500nm difference |
| 3|  3  |uint8  |OPC_500 - OPC_700| 700nm difference |
| 4|  4  |uint8  |OPC_700 - OPC_1000| 1000nm difference | 
| 5|  5  |uint8  |OPC_1000 - OPC_3000| 3000nm Bin |
| 6|  6  |uint8  |IPump1| Pump1 current in mA |
| 7|  7  |uint8  |IPump2| Pump1 current in mA |
| 8|  8  |uint8  |(TempPump1 + TempPump2) / 2.0 + 100.0)|
| 9|  9  |uint8  |VBat* 10.0| Battery voltage * 10 |
|10|10-11|uint16 |short(TempCN* 100.0) | Saturator T |
|11| 12  |uint8  |(TempIce + 10.0) * 10 | Ice Jacket T |
|12| 13  |uint8  |Alt_Status| Status byte for altitude source |
|  |     |       | **2nd STAC (heated) with reduced data** ||
|13|14-15|uint16 |CNC_300| 300nm Bin |
|14|  16 |uint8  |(CNC_300 - CNC_500)| 500nm Difference |
|15|  17 |uint8  |(CNC_500 - CNC_700)| 700nm Diffenence |
|16|  18 |uint8  |(CNC_700 - CNC_1000)| 1000nm Diffenence |
|17|  19 |uint8  |(CNC_1000 - CNC_3000)| 3000nm Diffenence |
|18|  20 |uint8  |CNC_IPump1| Pump1 current in mA |
|19|  21 |uint8  |CNC_IPump2| Pump1 current in mA |
|20|  22 |uint8  |(CNC_TempPump1 + CN_TempPump2) / 2.0 + 100.0)| |
|21|  23 |uint8  |CNC_VBat |Battery voltage * 10 |
|22|24-25|uint16 |CNC_TempCN* 100.0) |Saturator T |
|23|  26 |uint8  |(CNC_TempIce + 10.0) * 10)| Condenser T 1 |
|  |     |       |**Heated Inlet** ||
|24|  27 |uint8  |CN_T_UpStream| Upstream T |
|25|  28 |uint8  |CN_T_Middle| Midstream T |
|26|  29 |uint8  |CN_T_DownStream| Downstream T|
|27|  30 |uint8  |CN_HI_V_Battery * 10.0)| Battery Voltage * 10 |

_XDATA from the instruments_

This is a sample of what is transmitted to the iMet from the three instruments:

```
xdata=4102001E14020008534486A10DA700020019110401035130E9A35730646AB4718C
xdata=4102001B11040105574187A10DA60002002922010204512EE9A352E4646BB4711C
xdata=410200130E020102524187A10D9C0002001B140303014B2EE9A352E4646BB4711C
xdata=4102002014050601534487A10DA700020015110100035231E9A359EC646BB5711C
xdata=4102001C130201065A4687A10DA50002001910040301522AE9A35730646BB4711C
xdata=4102001F14030107504186A10DAA00020017120102024D2DE9A35794646BB4711C
xdata=4102001D14050004544186A10DAE0002001B16030101542EE9A35BE0646BB3711C
xdata=4102002616040309513D86A10DA90002001B120402034F2DE9A358C0646AB3718C
xdata=4102001A140301024F4086A10DAD000200150F0302014C2EE9A354D8646AB2718C
xdata=410200110E000201534387A10DA60002001C12050401552EE9A357F8646AB2718C
xdata=410200261D020205594787A10DAD000200231D0300034F2AE9A357F8646AB2718C
xdata=4102001E0F03010A534287A10DA300020020180401034F2EE9A35988646AB2718C
xdata=4102001D14020106534187A10DA300020022130506044F2CE9A35730646AB3718C
xdata=41020023150202094F4287A10DA50002002E280401014C2BE9A35CA8646AB3718C
xdata=410200180E0204044F4087A10DA60002002C230301054F2DE9A34F60646AB3718C
xdata=4102001810040103534487A10DA50002001B120602015431E9A35794646AB4718C
xdata=410200170D030006544187A10DAE0002002E230303054C2BE9A353AC646BB4711C
xdata=41020019110102054E3F87A10D93000200150F0102034F30E9A352E4646BB4711C
```
