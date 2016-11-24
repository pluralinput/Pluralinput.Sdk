using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pluralinput.Sdk
{
    public class MouseButtonInputEventArgs : EventArgs
    {
        public MouseButtonInputEventArgs(VirtualKeys button)
        {
            Button = button;
        }

        public VirtualKeys Button { get; private set; }
    }
}
