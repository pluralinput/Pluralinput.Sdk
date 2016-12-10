using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using static Pluralinput.Sdk.NativeStructs;
using static Pluralinput.Sdk.NativeMethods;
using static Pluralinput.Sdk.NativeConst;

namespace Pluralinput.Sdk
{
    public class DeviceEnumerator : IDeviceEnumerator
    {
        public DeviceEnumerator(IRawInputSource rawInputSource, IntPtr windowHandle)
        {
            RawInputSource = rawInputSource;

            RegisterInputDevices(windowHandle);
        }

        private IRawInputSource RawInputSource { get; set; }

        public IEnumerable<Mouse> Mice
        {
            get
            {
                var mice = GetRawInputDevices(RIM_TYPEMOUSE);
                return mice.Select(ridl => new Mouse(ridl.hDevice, RawInputSource));
            }
        }

        public IEnumerable<Keyboard> Keyboards
        {
            get
            {
                var keyboards = GetRawInputDevices(RIM_TYPEKEYBOARD);
                return keyboards.Select(ridl => new Keyboard(ridl.hDevice, RawInputSource));
            }
        }

        //TODO: ideally, this is also done in IRawInputSource.
        protected void RegisterInputDevices(IntPtr targetWindowHandle)
        {
            var flags = RIDEV_INPUTSINK;

            var devices = new RAWINPUTDEVICE[]
            {
                new RAWINPUTDEVICE()
                {
                     usUsagePage = 0x01,
                     usUsage = 0x02,
                     dwFlags = flags,
                     hwndTarget = targetWindowHandle
                },
                new RAWINPUTDEVICE()
                {
                     usUsagePage = 0x01,
                     usUsage = 0x06,
                     dwFlags = flags,
                     hwndTarget = targetWindowHandle
                }
            };

            if (!RegisterRawInputDevices(devices, devices.Length, Marshal.SizeOf(typeof(RAWINPUTDEVICE))))
            {
                throw new Win32Exception();
            }
        }

        private IEnumerable<RAWINPUTDEVICELIST> GetRawInputDevices(uint type)
        {
            uint deviceCount = 0;
            uint dwSize = (uint)Marshal.SizeOf(typeof(RAWINPUTDEVICELIST));

            uint retValue = GetRawInputDeviceList(null, ref deviceCount, dwSize);

            if (0 != retValue)
                return null;

            RAWINPUTDEVICELIST[] deviceList = new RAWINPUTDEVICELIST[deviceCount];

            retValue = GetRawInputDeviceList(deviceList, ref deviceCount, dwSize);

            return deviceList.Where(ridl => ridl.dwType == type);
        }
    }
}
