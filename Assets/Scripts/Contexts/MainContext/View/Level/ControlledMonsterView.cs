using System.Collections;
using UnityEngine;

namespace Contexts.MainContext
{
    public class ControlledMonsterView : MonsterView
    {
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

        protected override IEnumerator Move()
        {
            while (true)
            {
                Vector3 movementVector = Vector3.Normalize(-_controls.Character.Movement.ReadValue<Vector2>());

                if (movementVector != Vector3.zero)
                    animator.SetBool(walkAnimBool, true);
                else
                    animator.SetBool(walkAnimBool, false);

                rb.velocity = new Vector3(movementVector.x, 0, movementVector.y) * (MonsterData.MovementSpeed +
                              transform.localScale.z);
                transform.LookAt(transform.position + new Vector3(movementVector.x, 0, movementVector.y));

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
