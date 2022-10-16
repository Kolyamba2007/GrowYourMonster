using UnityEngine;
using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionary;

namespace Contexts.MainContext
{
    [CreateAssetMenu(fileName = "InfrastructureConfig", menuName = "Configurations/InfrastructureConfig", order = 2)]
    public class InfrastructureConfig : ScriptableObject
    {
        [SerializeField] private SerializableDictionaryBase<string, InfrastructureData> infrastructureData;

        /// <summary>
        /// Return infrastructure data
        /// </summary>
        public IDictionary<string, InfrastructureData> InfrastructureData => infrastructureData;
    }
}
