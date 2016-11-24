using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pluralinput.Sdk
{
    public class KeyInputEventArgs : EventArgs
    {
        public KeyInputEventArgs(VirtualKeys key)
        {
            Key = key;
        }

        public VirtualKeys Key { get; set; }
    }
}
