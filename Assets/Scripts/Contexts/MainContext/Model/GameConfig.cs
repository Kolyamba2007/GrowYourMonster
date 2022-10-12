using UnityEngine;

namespace Contexts.MainContext
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configurations/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private string monsterConfig;
        [SerializeField] private string infrastructureConfig;

        /// <summary>
        /// Return monster config
        /// </summary>
        public MonsterConfig GetMonsterConfig => Resources.Load<MonsterConfig>(monsterConfig);
        
        /// <summary>
        /// Return infrastructure config
        /// </summary>
        public InfrastructureConfig GetInfrastructureConfig => Resources.Load<InfrastructureConfig>(infrastructureConfig);

        public static GameConfig Load()
        {
            var data = Resources.LoadAll<GameConfig>("");
            if (data.Length != 1)
            {
                Debug.LogError($"Can't find <b>{nameof(GameConfig)}</b> asset in Resource Folder!");
            }

            return data[0];
        }
    }
}
