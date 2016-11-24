using System;
using System.Collections.Generic;
using System.Linq;
using static Pluralinput.Sdk.NativeStructs;

namespace Pluralinput.Sdk
{
    public class RawKeyboardInputEventArgs : EventArgs
    {
        public RawKeyboardInputEventArgs(RAWINPUTHEADER header, RAWKEYBOARD data)
        {
            Header = header;
            Data = data;
        }

        public RAWINPUTHEADER Header { get; private set; }
        public RAWKEYBOARD Data { get; private set; }
    }
}
