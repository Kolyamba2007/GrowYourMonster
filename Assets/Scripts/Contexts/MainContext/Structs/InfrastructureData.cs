using System;
using UnityEngine;

namespace Contexts.MainContext
{
    [Serializable]
    public struct InfrastructureData : IHealth
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private int points;

        public int Health => maxHealth;
        public int Points => points;
    }
}
