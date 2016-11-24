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
        public DeviceEnumerator(IRawInputSource rawInputSource)
        {
            RawInputSource = rawInputSource;
        }

        private IRawInputSource RawInputSource { get; set; }

        public IEnumerable<Mouse> EnumerateMice()
        {
            var mice = GetRawInputDevices(RIM_TYPEMOUSE);
            return mice.Select(ridl => new Mouse(ridl.hDevice, RawInputSource));
        }

        public IEnumerable<Keyboard> EnumerateKeyboards()
        {
            var keyboards = GetRawInputDevices(RIM_TYPEKEYBOARD);
            return keyboards.Select(ridl => new Keyboard(ridl.hDevice, RawInputSource));
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
