using System.Collections.Generic;

namespace Contexts.MainContext
{
    public interface IMonsterState
    {
        Dictionary<ushort, int> Score { get; }
        Dictionary<ushort, float> Speed { get; }
    }
}
