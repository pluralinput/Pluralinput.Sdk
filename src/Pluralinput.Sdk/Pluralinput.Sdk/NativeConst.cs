using System;
using System.Collections.Generic;
using System.Linq;

namespace Pluralinput.Sdk
{
    public class NativeConst
    {
        internal const uint RIDI_DEVICENAME = 0x20000007;
        internal const uint RIDI_DEVICEINFO = 0x2000000b;
        internal const uint RIDI_PREPARSEDDATA = 0x20000005;

        internal const int RIM_TYPEMOUSE = 0;
        internal const int RIM_TYPEKEYBOARD = 1;
        internal const int RIM_TYPEHID = 2;

        internal const int CW_USEDEFAULT = unchecked((int)0x80000000);

        internal const int RIDEV_EXINPUTSINK = 0x00001000;
        internal const int RIDEV_INPUTSINK = 0x00000100;
        internal const int RIDEV_CAPTUREMOUSE = 0x00000200;
        internal const int RIDEV_NOLEGACY = 0x00000030;

        internal const uint RID_INPUT = 0x10000003;
        internal const uint RID_HEADER = 0x10000005;



        internal const uint MOUSE_MOVE_RELATIVE = 0x00;
        internal const uint MOUSE_MOVE_ABSOLUTE = 0x01;
        internal const uint MOUSE_VIRTUAL_DESKTOP = 0x02;
        internal const uint MOUSE_ATTRIBUTES_CHANGED = 0x04;
        internal const uint MOUSE_MOVE_NOCOLASCE = 0x08;

        internal const uint RI_MOUSE_LEFT_BUTTON_DOWN = 0x0001;
        internal const uint RI_MOUSE_LEFT_BUTTON_UP = 0x0002;
        internal const uint RI_MOUSE_RIGHT_BUTTON_DOWN = 0x0004;
        internal const uint RI_MOUSE_RIGHT_BUTTON_UP = 0x0008;
        internal const uint RI_MOUSE_MIDDLE_BUTTON_DOWN = 0x0010;
        internal const uint RI_MOUSE_MIDDLE_BUTTON_UP = 0x0020;

        internal const uint RI_MOUSE_BUTTON_1_DOWN = RI_MOUSE_LEFT_BUTTON_DOWN;
        internal const uint RI_MOUSE_BUTTON_1_UP = RI_MOUSE_LEFT_BUTTON_UP;
        internal const uint RI_MOUSE_BUTTON_2_DOWN = RI_MOUSE_RIGHT_BUTTON_DOWN;
        internal const uint RI_MOUSE_BUTTON_2_UP = RI_MOUSE_RIGHT_BUTTON_UP;
        internal const uint RI_MOUSE_BUTTON_3_DOWN = RI_MOUSE_MIDDLE_BUTTON_DOWN;
        internal const uint RI_MOUSE_BUTTON_3_UP = RI_MOUSE_MIDDLE_BUTTON_UP;

        internal const uint RI_MOUSE_BUTTON_4_DOWN = 0x0040;
        internal const uint RI_MOUSE_BUTTON_4_UP = 0x0080;
        internal const uint RI_MOUSE_BUTTON_5_DOWN = 0x0100;
        internal const uint RI_MOUSE_BUTTON_5_UP = 0x0200;
        internal const uint RI_MOUSE_WHEEL = 0x0400;

        internal const uint RI_KEY_MAKE = 0x00;
        internal const uint RI_KEY_BREAK = 0x01;
        internal const uint RI_KEY_E0 = 0x02;
        internal const uint RI_KEY_E1 = 0x04;
        internal const uint RI_KEY_TERMSRV_SET_LED = 0x08;
        internal const uint RI_KEY_TERMSRV_SHADOW = 0x10;
    }
}
