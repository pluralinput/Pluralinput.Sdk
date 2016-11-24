using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Pluralinput.Sdk
{
    public class InputDevice
    {
        public InputDevice(IntPtr deviceHandle)
        {
            DeviceHandle = deviceHandle;
            DeviceName = GetDeviceName();
        }

        public IntPtr DeviceHandle { get; protected set; }
        public string DeviceName { get; protected set; }

        protected string GetDeviceName()
        {
            uint charCount = 0;

            NativeMethods.GetRawInputDeviceInfo(DeviceHandle, NativeConst.RIDI_DEVICENAME, null, ref charCount);

            var sb = new StringBuilder((int)charCount);
            NativeMethods.GetRawInputDeviceInfo(DeviceHandle, NativeConst.RIDI_DEVICENAME, sb, ref charCount);

            return sb.ToString();
        }

        public override string ToString()
        {
            return DeviceName;
        }
    }
}
