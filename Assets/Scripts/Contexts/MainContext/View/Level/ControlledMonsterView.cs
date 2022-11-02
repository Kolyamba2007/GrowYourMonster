using System.Collections;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Contexts.MainContext
{
    public class ControlledMonsterView : MonsterView
    {
        public Signal<Vector3> MoveToSignal { get; } = new Signal<Vector3>();
        
        [SerializeField] private Rigidbody rb;
        
        private Controls _controls;

        public void SetData(MonsterData monsterData, Controls controls)
        {
            MonsterData = monsterData;
            _controls = controls;
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
                }
                if (animator.GetBool(attackAnimBool))
                    animator.SetBool(attackAnimBool, false);
            }
        }

        public void MoveTo(Vector3 direction, float speed)
        {
            rb.velocity = new Vector3(direction.x, 0, direction.y) * speed;
        }

        protected override IEnumerator Move()
        {
            while (true)
            {
                Vector3 movementVector = Vector3.Normalize(-_controls.Character.Movement.ReadValue<Vector2>());
                
                if (movementVector != Vector3.zero)
                {
                    if (!animator.GetBool(walkAnimBool))
                        animator.SetBool(walkAnimBool, true);
                    
                    transform.LookAt(transform.position + new Vector3(movementVector.x, 0, movementVector.y));

                    MoveToSignal.Dispatch(movementVector);
                }
                else
                {
                    if (animator.GetBool(walkAnimBool))
                        animator.SetBool(walkAnimBool, false);
                    
                    rb.velocity = Vector3.zero;
                }

                yield return null;
            }
        }
        
        protected override IEnumerator Attack()
        {
            while (true)
            {
                for (int i = 0; i < Temp.Count; i++)
                    HitInfrastructureSignal.Dispatch(Temp[i]);

                yield return new WaitForSeconds(MonsterData.AttackSpeed);
            }
        }
    }
}
