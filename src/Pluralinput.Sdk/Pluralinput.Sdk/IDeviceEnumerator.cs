using System.Collections.Generic;

namespace Pluralinput.Sdk
{
    public interface IDeviceEnumerator
    {
        IEnumerable<Mouse> Mice { get; }
        IEnumerable<Keyboard> Keyboards { get; }
    }
}
