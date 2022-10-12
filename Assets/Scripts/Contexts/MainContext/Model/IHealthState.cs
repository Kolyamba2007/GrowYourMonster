using System.Collections.Generic;

namespace Contexts.MainContext
{
    public interface IHealthState
    {
        Dictionary<ushort, int> Health { get; }
    }
}
