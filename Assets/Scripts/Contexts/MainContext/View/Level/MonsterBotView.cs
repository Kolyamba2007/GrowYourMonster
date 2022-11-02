using System.Collections;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.AI;

namespace Contexts.MainContext
{
    public class MonsterBotView : MonsterView
    {
        public Signal<NavMeshAgent> UpdateSpeedSignal { get; } = new Signal<NavMeshAgent>();
        
        [SerializeField] private Rigidbody rb;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Transform map;

        private float _maxX;
        private float _minX;
        private float _maxZ;
        private float _minZ;

        protected override void Awake()
        {
            base.Awake();

            _maxX = map.position.x + map.localScale.x / 2;
            _minX = map.position.x - map.localScale.x / 2;
            _maxZ = map.position.z + map.localScale.z / 2;
            _minZ = map.position.z - map.localScale.z / 2;
        }

        public void SetData(MonsterData monsterData)
        {
            MonsterData = monsterData;
        }

        public void UpdateSpeed()
        {
            UpdateSpeedSignal.Dispatch(navMeshAgent);
        }
        
        public override void FinishAttack(Collider collider)
        {
            Temp.Remove(collider);

            if (Temp.Count == 0)
            {
                if (AttackCoroutine != null)
                {
                    StopCoroutine(AttackCoroutine);
                    AttackCoroutine = null;
                    StartMove();
                }
                animator.SetBool(attackAnimBool, false);
            }
        }
        
        protected override IEnumerator Move()
        {
            animator.SetBool(walkAnimBool, true);
            while (true)
            {
                navMeshAgent.destination =
                    new Vector3(Random.Range(_minX, _maxX), transform.position.y, Random.Range(_minZ, _maxZ));

                yield return new WaitForSeconds(Random.Range(8, 11));
            }
        }
        
        protected override IEnumerator Attack()
        {
            animator.SetBool(walkAnimBool, false);
            if (MoveCoroutine != null)
            {
                StopCoroutine(MoveCoroutine);
                MoveCoroutine = null;
                navMeshAgent.destination = transform.position;
            }
            
            while (true)
            {
                for (int i = 0; i < Temp.Count; i++)
                {
                    HitInfrastructureSignal.Dispatch(Temp[i]);
                    UpdateSpeed();
                }

                yield return new WaitForSeconds(MonsterData.AttackSpeed);
            }
        }
    }
}
