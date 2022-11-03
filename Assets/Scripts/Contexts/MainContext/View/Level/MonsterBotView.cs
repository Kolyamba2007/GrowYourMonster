using System.Collections;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.AI;

namespace Contexts.MainContext
{
    public class MonsterBotView : MonsterView
    {
        public Signal<NavMeshAgent> UpdateSpeedSignal { get; } = new Signal<NavMeshAgent>();

        [SerializeField] private RectTransform pointerRect;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Transform map;

        private Transform _playerTransform;
        private Camera _camera;
        private float _maxX;
        private float _minX;
        private float _maxZ;
        private float _minZ;

        protected override void Awake()
        {
            base.Awake();

            pointerRect.gameObject.SetActive(false);
            
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

        public void UpdateData(Transform playerTransform)
        {
            _playerTransform = playerTransform;
            _camera ??= Camera.main!;
        }

        public void EnablePointer()
        {
            StartCoroutine(Pointer());
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
                if (transform.position == navMeshAgent.destination)
                    navMeshAgent.destination =
                        new Vector3(Random.Range(_minX, _maxX), transform.position.y, Random.Range(_minZ, _maxZ));

                yield return null;
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

        private IEnumerator Pointer()
        {
            pointerRect.gameObject.SetActive(true);
            
            while (true)
            {
                Vector3 fromPlayerToEnemy = transform.position - _playerTransform.position;
                Ray ray = new Ray(_playerTransform.position, fromPlayerToEnemy);

                Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

                float minDistance = Mathf.Infinity;
                for (int i = 0; i < 4; i++)
                {
                    if (planes[i].Raycast(ray, out float distance))
                    {
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                        }
                    }
                }

                if (minDistance < fromPlayerToEnemy.magnitude)
                {
                    if (!pointerRect.gameObject.activeSelf) pointerRect.gameObject.SetActive(true);
                    
                    Vector3 worldPos = ray.GetPoint(minDistance);
                    pointerRect.position = _camera.WorldToScreenPoint(worldPos);
                    
                    float angle = Mathf.Atan2(fromPlayerToEnemy.z, fromPlayerToEnemy.x) * Mathf.Rad2Deg;
                    pointerRect.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
                }
                else if (pointerRect.gameObject.activeSelf)
                    pointerRect.gameObject.SetActive(false);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}
