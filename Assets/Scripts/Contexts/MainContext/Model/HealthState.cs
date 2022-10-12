using System.Collections.Generic;

namespace Contexts.MainContext
{
    public class HealthState : IHealthState
    {
        public Dictionary<ushort, int> Health { get; } = new Dictionary<ushort, int>();
    }
}
