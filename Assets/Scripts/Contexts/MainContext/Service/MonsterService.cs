using System.Linq;
using UnityEngine;

namespace Contexts.MainContext
{
    public class MonsterService : IMonsterService
    {
        [Inject] public IMonsterState MonsterState { get; set; }

        public void AddMonster(out ushort id)
        {
            id = GetID();
            
            MonsterState.Score.Add(id, 0);
            MonsterState.Speed.Add(id, 0);
        }

        public void Remove(ushort id)
        {
            if (MonsterState.Score.ContainsKey(id))
                MonsterState.Score.Remove(id);
        }

        public void RaiseScore(ushort id, int point)
        {
            if (MonsterState.Score.ContainsKey(id))
                MonsterState.Score[id] += point;
        }

        public int GetScore(ushort id)
        {
            if (MonsterState.Score.ContainsKey(id))
                return MonsterState.Score[id];
            
            Debug.LogError($"Monster does not exist with ID: {id}");
            return 0;
        }

        public void ChangeSpeed(ushort id, float speed)
        {
            if (MonsterState.Score.ContainsKey(id))
                MonsterState.Speed[id] = speed;
        }

        private ushort GetID()
        {
            var id = MonsterState.Score.Keys;

            if (id.Count != 0)
                return (ushort) (id.Max() + 1);
            
            return 0;
        }
    }
}
