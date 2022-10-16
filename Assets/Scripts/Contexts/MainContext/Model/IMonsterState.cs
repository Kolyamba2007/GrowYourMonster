using System.Collections.Generic;

namespace Contexts.MainContext
{
    public interface IMonsterState
    {
        Dictionary<ushort, int> Score { get; }
    }
}
