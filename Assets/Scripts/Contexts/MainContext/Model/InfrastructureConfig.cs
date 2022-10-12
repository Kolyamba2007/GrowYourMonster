using UnityEngine;

namespace Contexts.MainContext
{
    [CreateAssetMenu(fileName = "InfrastructureConfig", menuName = "Configurations/InfrastructureConfig", order = 2)]
    public class InfrastructureConfig : ScriptableObject
    {
        [SerializeField] private InfrastructureData infrastructureData;

        /// <summary>
        /// Return infrastructureData data
        /// </summary>
        public InfrastructureData InfrastructureData => infrastructureData;
    }
}
