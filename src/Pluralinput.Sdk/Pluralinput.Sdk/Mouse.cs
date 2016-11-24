using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using static Pluralinput.Sdk.NativeConst;

namespace Pluralinput.Sdk
{
    public class Mouse : InputDevice
    {
        public event EventHandler<MouseButtonInputEventArgs> ButtonDown;
        public event EventHandler<MouseButtonInputEventArgs> ButtonUp;
        public event EventHandler<MouseMoveInputEventArgs> Move;
        public event EventHandler<MouseWheelInputEventArgs> Wheel;

        public Mouse(IntPtr deviceHandle, IRawInputSource mouseInputSource) : base(deviceHandle)
        {
            mouseInputSource.MouseInput += InputSource_InputAvailable;
        }

        public int LastX { get; set; }
        public int LastY { get; set; }

        private void InputSource_InputAvailable(object sender, RawMouseInputEventArgs e)
        {
            // only evaluate this event if it is actually intended for this device
            // TODO: implement smarter input dispatching
            if (e.Header.hDevice != DeviceHandle)
                return;

            if ((e.Data.ButtonFlags & RI_MOUSE_WHEEL) == RI_MOUSE_WHEEL)
            {
                Wheel?.Invoke(this, new MouseWheelInputEventArgs(e.Data.ButtonData));
            }

            if ((e.Data.ButtonFlags & RI_MOUSE_LEFT_BUTTON_DOWN) == RI_MOUSE_LEFT_BUTTON_DOWN)
            {
                ButtonDown?.Invoke(this, new MouseButtonInputEventArgs(VirtualKeys.LeftButton));
            }

            if ((e.Data.ButtonFlags & RI_MOUSE_LEFT_BUTTON_UP) == RI_MOUSE_LEFT_BUTTON_UP)
            {
                ButtonUp?.Invoke(this, new MouseButtonInputEventArgs(VirtualKeys.LeftButton));
            }

            if ((e.Data.ButtonFlags & RI_MOUSE_RIGHT_BUTTON_DOWN) == RI_MOUSE_RIGHT_BUTTON_DOWN)
            {
                ButtonDown?.Invoke(this, new MouseButtonInputEventArgs(VirtualKeys.RightButton));
            }

            if ((e.Data.ButtonFlags & RI_MOUSE_RIGHT_BUTTON_UP) == RI_MOUSE_RIGHT_BUTTON_UP)
            {
                ButtonUp?.Invoke(this, new MouseButtonInputEventArgs(VirtualKeys.RightButton));
            }

            if ((e.Data.ButtonFlags & RI_MOUSE_MIDDLE_BUTTON_DOWN) == RI_MOUSE_MIDDLE_BUTTON_DOWN)
            {
                ButtonDown?.Invoke(this, new MouseButtonInputEventArgs(VirtualKeys.MiddleButton));
            }

            if ((e.Data.ButtonFlags & RI_MOUSE_MIDDLE_BUTTON_UP) == RI_MOUSE_MIDDLE_BUTTON_UP)
            {
                ButtonUp?.Invoke(this, new MouseButtonInputEventArgs(VirtualKeys.MiddleButton));
            }

            // wheel and XBUTTON aren't possible at the same time
            if ((e.Data.ButtonFlags & RI_MOUSE_BUTTON_4_DOWN) == RI_MOUSE_BUTTON_4_DOWN)
            {
                ButtonDown?.Invoke(this, new MouseButtonInputEventArgs(VirtualKeys.ExtraButton1));
            }

            if ((e.Data.ButtonFlags & RI_MOUSE_BUTTON_4_UP) == RI_MOUSE_BUTTON_4_UP)
            {
                ButtonUp?.Invoke(this, new MouseButtonInputEventArgs(VirtualKeys.ExtraButton1));
            }

            if ((e.Data.ButtonFlags & RI_MOUSE_BUTTON_5_DOWN) == RI_MOUSE_BUTTON_5_DOWN)
            {
                ButtonDown?.Invoke(this, new MouseButtonInputEventArgs(VirtualKeys.ExtraButton2));
            }

            if ((e.Data.ButtonFlags & RI_MOUSE_BUTTON_5_UP) == RI_MOUSE_BUTTON_5_UP)
            {
                ButtonUp?.Invoke(this, new MouseButtonInputEventArgs(VirtualKeys.ExtraButton2));
            }

            if (e.Data.LastX != LastX || e.Data.LastY != LastY)
            {
                Move?.Invoke(this, new MouseMoveInputEventArgs(e.Data.LastX, e.Data.LastY));
            }

            LastX = e.Data.LastX;
            LastY = e.Data.LastY;
        }
    }
}
