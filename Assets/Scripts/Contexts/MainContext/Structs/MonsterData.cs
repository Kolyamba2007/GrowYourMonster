using System;
using UnityEngine;

namespace Contexts.MainContext
{
    [Serializable]
    public struct MonsterData
    {
        [SerializeField] private int attackDamage;
        [SerializeField] private float attackRange;
        [SerializeField] private float movementSpeed;

        public int AttackDamage => attackDamage;
        public float AttackRange => attackRange;
        public float MovementSpeed => movementSpeed;
    }
}
