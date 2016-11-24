using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using static Pluralinput.Sdk.NativeFlags;

namespace Pluralinput.Sdk
{
    public class Keyboard : InputDevice
    {
        public event EventHandler<KeyInputEventArgs> KeyUp;
        public event EventHandler<KeyInputEventArgs> KeyDown;

        public Keyboard(IntPtr deviceHandle, IRawInputSource keyboardInputSource) : base(deviceHandle)
        {
            keyboardInputSource.KeyboardInput += KeyboardInputSource_KeyboardInput;
        }

        private void KeyboardInputSource_KeyboardInput(object sender, RawKeyboardInputEventArgs e)
        {
            // only evaluate this event if it is actually intended for this device
            //TODO: add some sort of conditional event invocation before this
            if (e.Header.hDevice != DeviceHandle)
                return;

            VirtualKeys key = (VirtualKeys)e.Data.VirtualKey;

            switch ((WM)e.Data.Message)
            {
                case WM.KEYDOWN:
                    {
                        KeyDown?.Invoke(this, new KeyInputEventArgs(key));
                        break;
                    }
                case WM.KEYUP:
                    {
                        KeyUp?.Invoke(this, new KeyInputEventArgs(key));
                        break;
                    }
                case WM.SYSKEYDOWN:
                    {
                        //TODO: syskeydown
                        break;
                    }
            }
        }
    }
}
