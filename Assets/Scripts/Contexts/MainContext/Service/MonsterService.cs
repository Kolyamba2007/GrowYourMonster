using System.Linq;

namespace Contexts.MainContext
{
    public class MonsterService : IMonsterService
    {
        [Inject] public IMonsterState MonsterState { get; set; }

        public void AddMonster(out ushort id)
        {
            id = GetID();
            
            MonsterState.Score.Add(id, 0);
        }

        public void Remove(ushort id)
        {
            if (MonsterState.Score.TryGetValue(id, out int _))
                MonsterState.Score.Remove(id);
        }

        public void RaiseScore(ushort id, int point)
        {
            if (MonsterState.Score.TryGetValue(id, out int _))
                MonsterState.Score[id] += point;
        }

        public int GetScore(ushort id)
        {
            if (MonsterState.Score.TryGetValue(id, out int score))
                return score;
            else
                return 0;
        }

        private ushort GetID()
        {
            var id = MonsterState.Score.Keys;

            if (id.Count != 0)
                return (ushort) (id.Max() + 1);
            else
                return 0;
        }
    }
}
