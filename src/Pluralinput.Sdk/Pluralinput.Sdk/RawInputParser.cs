using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using static Pluralinput.Sdk.NativeStructs;
using static Pluralinput.Sdk.NativeMethods;
using static Pluralinput.Sdk.NativeConst;
using static Pluralinput.Sdk.NativeFlags;

namespace Pluralinput.Sdk
{
    public class RawInputParser : IRawInputSource
    {
        public RawInputParser()
        { }

        public event EventHandler<RawMouseInputEventArgs> MouseInput;
        public event EventHandler<RawKeyboardInputEventArgs> KeyboardInput;

        public void Parse(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
        {
            uint dwSize = 0;
            uint cbSizeHeader = (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER));

            if (GetRawInputData(lParam,
                                RID_INPUT,
                                IntPtr.Zero,
                                ref dwSize,
                                cbSizeHeader) == unchecked((uint)-1))
            {
                throw new Win32Exception();
            }

            var idata_buffer = Marshal.AllocHGlobal((int)dwSize);

            if (GetRawInputData(lParam,
                                RID_INPUT,
                                idata_buffer,
                                ref dwSize,
                                cbSizeHeader) == unchecked((uint)-1))
            {
                throw new Win32Exception();
            }

            var ri = (RAWINPUT)Marshal.PtrToStructure(idata_buffer, typeof(RAWINPUT));

            Marshal.FreeHGlobal(idata_buffer);

            //TODO: currently works only on x64 (not Any CPU/x86), need to check structs
            switch (ri.header.dwType)
            {
                case RIM_TYPEKEYBOARD:
                    {
                        ProcessRawKeyboardInput(ri.data.keyboard, ri.header);
                        break;
                    }
                case RIM_TYPEMOUSE:
                    {
                        ProcessRawMouseInput(ri.data.mouse, ri.header);
                        break;
                    }
            }
        }

        private void ProcessRawMouseInput(RAWMOUSE mouse, RAWINPUTHEADER rh)
        {
            MouseInput?.Invoke(this, new RawMouseInputEventArgs(rh, mouse));            
        }

        //TODO: keystate and modifier keys.
        private void ProcessRawKeyboardInput(RAWKEYBOARD keyboard, RAWINPUTHEADER rh)
        {
            KeyboardInput?.Invoke(this, new RawKeyboardInputEventArgs(rh, keyboard));            
        }
    }
}
