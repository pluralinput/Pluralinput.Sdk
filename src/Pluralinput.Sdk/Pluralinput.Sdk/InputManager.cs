using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using static Pluralinput.Sdk.NativeStructs;
using static Pluralinput.Sdk.NativeMethods;
using static Pluralinput.Sdk.NativeConst;
using System.ComponentModel;

namespace Pluralinput.Sdk
{
    public class InputManager
    {
        public InputManager()
        {
            Initialize();
        }

        public IDeviceEnumerator DeviceEnumerator { get; private set; }
        protected WindowCreator WindowCreator { get; set; }
        protected RawInputParser RawInputParser { get; set; }

        protected void Initialize()
        {
            RawInputParser = new RawInputParser();
            WindowCreator = new WindowCreator(RawInputParser);

            DeviceEnumerator = new DeviceEnumerator(RawInputParser);

            var windowHandle = WindowCreator.CreateWindow();
            RegisterInputDevices(windowHandle);
        }

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
    }
}
