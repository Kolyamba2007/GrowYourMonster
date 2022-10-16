using System.Collections.Generic;

namespace Contexts.MainContext
{
    public class MonsterState : IMonsterState
    {
        public Dictionary<ushort, int> Score { get; } = new Dictionary<ushort, int>();
    }
}
