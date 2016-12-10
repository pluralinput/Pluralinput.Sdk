using System;

namespace Pluralinput.Sdk
{
    public interface IRawInputSource
    {
        event EventHandler<RawMouseInputEventArgs> MouseInput;
        event EventHandler<RawKeyboardInputEventArgs> KeyboardInput;
    }
}
