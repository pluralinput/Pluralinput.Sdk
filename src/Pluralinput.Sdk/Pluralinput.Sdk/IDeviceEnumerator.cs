using System.Collections.Generic;

namespace Pluralinput.Sdk
{
    public interface IDeviceEnumerator
    {
        IEnumerable<Mouse> EnumerateMice();
        IEnumerable<Keyboard> EnumerateKeyboards();
    }
}
