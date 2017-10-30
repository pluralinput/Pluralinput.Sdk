using System;

namespace Pluralinput.Sdk
{
    public class InputManager : IDisposable
    {
        public InputManager()
        {
            Initialize();
        }

        public IDeviceEnumerator Devices { get; private set; }
        protected WindowCreator WindowCreator { get; set; }
        protected RawInputParser RawInputParser { get; set; }

        protected void Initialize()
        {
            RawInputParser = new RawInputParser();
            WindowCreator = new WindowCreator(RawInputParser);
            var windowHandle = WindowCreator.CreateWindow();

            Devices = new DeviceEnumerator(RawInputParser, windowHandle);
        }

        public void Dispose()
        {
            WindowCreator?.Dispose();
        }
    }
}
