using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using static Pluralinput.Sdk.NativeFlags;
using static Pluralinput.Sdk.NativeStructs;
using static Pluralinput.Sdk.NativeMethods;
using static Pluralinput.Sdk.NativeConst;
using System.Threading;

namespace Pluralinput.Sdk
{
    public class WindowCreator : IDisposable
    {
        private bool isDisposed = false;
        private static WndProc WndProcDelegate;
        private IntPtr windowHandle = IntPtr.Zero;
        private string windowClassName = "PluralinputSDKHiddenWindowClass-";

        public WindowCreator(RawInputParser rawInputParser)
        {
            RawInputParser = rawInputParser;
            WndProcDelegate = WndProc;

            windowClassName += Guid.NewGuid().ToString();
        }

        private RawInputParser RawInputParser { get; set; }

        private IntPtr WndProc(IntPtr hwnd, uint message, IntPtr wParam, IntPtr lParam)
        {
            switch ((WM)message)
            {
                case WM.DESTROY:
                    PostQuitMessage(0);
                    return IntPtr.Zero;
                case WM.INPUT:
                    RawInputParser.Parse(hwnd, wParam, lParam);
                    break;
            }

            return DefWindowProc(hwnd, (WM)message, wParam, lParam);
        }

        public IntPtr CreateWindow()
        {
            //TODO: possible race condition if CreateWindow is called from multiple threads.
            if (windowHandle != IntPtr.Zero)
                throw new InvalidOperationException("Only a single call to CreateWindow is allowed per WindowCreator instance.");

            var windowCreatedEvent = new ManualResetEvent(false);

            var windowThreadStart = new ThreadStart(() =>
            {
                // ideally, the HINSTANCE of this dll should be used, not the host application's.
                // we mitigate potential window class name collisions by 'randomizing' the name in the constructor.
                IntPtr instanceHandle = Process.GetCurrentProcess().Handle;

                WNDCLASS windowClass = new WNDCLASS();
                windowClass.style = 0;
                windowClass.lpfnWndProc = new WndProc(WndProcDelegate);
                windowClass.cbClsExtra = 0;
                windowClass.cbWndExtra = 0;
                windowClass.hInstance = instanceHandle;
                windowClass.hIcon = IntPtr.Zero; //LoadIcon(IntPtr.Zero, new IntPtr((int)SystemIcons.IDI_APPLICATION));
                windowClass.hCursor = IntPtr.Zero; //LoadCursor(IntPtr.Zero, (int)IdcStandardCursors.IDC_ARROW);
                windowClass.hbrBackground = IntPtr.Zero; //GetStockObject(StockObjects.WHITE_BRUSH);
                windowClass.lpszMenuName = null;
                windowClass.lpszClassName = windowClassName;

                ushort regResult = RegisterClass(ref windowClass);

                if (regResult == 0)
                {
                    throw new Win32Exception();
                }

                var hwnd = CreateWindowEx(
                    WindowStylesEx.WS_EX_NOACTIVATE | WindowStylesEx.WS_EX_TRANSPARENT,
                    new IntPtr((int)(uint)regResult),
                    "Pluralinput.Sdk Window",
                    WindowStyles.WS_DISABLED,
                    CW_USEDEFAULT,
                    CW_USEDEFAULT,
                    CW_USEDEFAULT,
                    CW_USEDEFAULT,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    instanceHandle,
                    IntPtr.Zero);

                if (hwnd == IntPtr.Zero)
                {
                    throw new Win32Exception();
                }

                windowHandle = hwnd;
                windowCreatedEvent.Set();

                // no need to show a hidden window.
                //ShowWindow(hwnd, (int)ShowWindowCommands.Hide);
                UpdateWindow(hwnd);

                MSG msg;
                while (GetMessage(out msg, IntPtr.Zero, 0, 0) != 0)
                {
                    TranslateMessage(ref msg);
                    DispatchMessage(ref msg);
                }
            });

            var windowThread = new Thread(windowThreadStart)
            {
                Name = "PluralinputSDKBackgroundWindowThread"
            };

            windowThread.Start();

            windowCreatedEvent.WaitOne();
            
            return windowHandle;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    // dispose managed resources
                }

                if (windowHandle != null && windowHandle != IntPtr.Zero)
                {
                    DestroyWindow(windowHandle);
                    windowHandle = IntPtr.Zero;
                }
            }
        }
    }
}
