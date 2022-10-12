using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Contexts.MainContext
{
    public class MonsterView : View
    {
        public Signal<Collider> HitInfrastructureSignal { get; } = new Signal<Collider>();
        
        [SerializeField] private Rigidbody rb;
        [Space, SerializeField] private Animator animator;
        [SerializeField] private string walkAnimBool;
        [SerializeField] private string attackAnimBool;
        [Space, SerializeField] private LayerMask infrastructureLayer;

        public MonsterData MonsterData { get; private set; }

        private Controls _controls;
        private readonly RaycastHit[] _resultHit = new RaycastHit[1];
        private int _hit;

        public void SetData(MonsterData monsterData, Controls controls)
        {
            MonsterData = monsterData;
            _controls = controls;
        }

        public void StartMove()
        {
            StartCoroutine(Move());
        }
        
        public void StartInfrastructureDetect()
        {
            StartCoroutine(InfrastructureDetect());
        }
        
        public void StartAttack()
        {
            StartCoroutine(Attack());
        }

        private IEnumerator Move()
        {
            while (true)
            {
                Vector3 movementVector = Vector3.Normalize(-_controls.Character.Movement.ReadValue<Vector2>());

                if (movementVector != Vector3.zero)
                    animator.SetBool(walkAnimBool, true);
                else
                    animator.SetBool(walkAnimBool, false);

                rb.velocity = new Vector3(movementVector.x, 0, movementVector.y) * MonsterData.MovementSpeed;
                transform.LookAt(transform.position + new Vector3(movementVector.x, 0, movementVector.y));

                yield return null;
            }
        }
        
        private IEnumerator InfrastructureDetect()
        {
            while (true)
            {
                _hit = Physics.RaycastNonAlloc(transform.position + Vector3.up, transform.forward, _resultHit,
                    MonsterData.AttackRange, infrastructureLayer.value);

                yield return new WaitForFixedUpdate();
            }
        }
        
        private IEnumerator Attack()
        {
            while (true)
            {
                if (_hit != 0)
                {
                    if (!animator.GetBool(attackAnimBool))
                        animator.SetBool(attackAnimBool, true);
                }
                else
                {
                    if (animator.GetBool(attackAnimBool))
                        animator.SetBool(attackAnimBool, false);
                }

                yield return new WaitForFixedUpdate();
            }
        }

        private void OnAttack()
        {
            if (_hit != 0)
                HitInfrastructureSignal.Dispatch(_resultHit[0].collider);
        }
    }
}
