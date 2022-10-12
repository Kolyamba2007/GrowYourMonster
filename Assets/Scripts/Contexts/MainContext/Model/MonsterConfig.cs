using UnityEngine;

namespace Contexts.MainContext
{
    [CreateAssetMenu(fileName = "MonsterConfig", menuName = "Configurations/MonsterConfig", order = 1)]
    public class MonsterConfig : ScriptableObject
    {
        [SerializeField] private MonsterData monsterData;

        /// <summary>
        /// Return monster data
        /// </summary>
        public MonsterData MonsterData => monsterData;
    }
}
