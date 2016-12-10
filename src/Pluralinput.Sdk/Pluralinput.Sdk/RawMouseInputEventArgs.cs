using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Pluralinput.Sdk.NativeStructs;

namespace Pluralinput.Sdk
{
    public class RawMouseInputEventArgs : EventArgs
    {
        public RawMouseInputEventArgs(RAWINPUTHEADER header, RAWMOUSE data)
        {
            Header = header;
            Data = data;
        }

        public RawMouseInputEventArgs()
        { }

        public RAWINPUTHEADER Header { get; set; }
        public RAWMOUSE Data { get; set; }
    }
}
