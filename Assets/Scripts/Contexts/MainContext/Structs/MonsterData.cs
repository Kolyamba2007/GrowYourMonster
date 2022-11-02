using System;
using UnityEngine;

namespace Contexts.MainContext
{
    [Serializable]
    public struct MonsterData
    {
        [SerializeField] private int attackDamage;
        [SerializeField] private float attackSpeed;
        [SerializeField] private float startSpeed;
        [SerializeField] private float endSpeed;
        [SerializeField] private int growthPointLimit;
        [SerializeField] private float maxScale;

        public int AttackDamage => attackDamage;
        public float AttackSpeed => attackSpeed;
        
        public float StartSpeed => startSpeed;
        public float EndSpeed => endSpeed;
        public int GrowthPointLimit => growthPointLimit;
        public float MaxScale => maxScale;
    }
}
