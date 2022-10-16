using System.Collections;
using System.Collections.Generic;
using strange.extensions.signal.impl;
using TMPro;
using UnityEngine;

namespace Contexts.MainContext
{
    public class MonsterView : IdentifiableView
    {
        public Signal<Collider> HitInfrastructureSignal { get; } = new Signal<Collider>();
        
        [SerializeField] private Rigidbody rb;
        [Space, SerializeField] private Animator animator;
        [SerializeField] private string walkAnimBool;
        [SerializeField] private string attackAnimBool;
        [Space, SerializeField] private LayerMask infrastructureLayer;
        [SerializeField] private SkinnedMeshRenderer meshRenderer;
        [Space, SerializeField] private TMP_Text scoreText;

        public MonsterData MonsterData { get; private set; }

        private Controls _controls;
        private readonly List<Collider> _temp = new List<Collider>();

        public void SetData(MonsterData monsterData, Controls controls)
        {
            MonsterData = monsterData;
            _controls = controls;
        }

        public void UpdateScore(int score)
        {
            scoreText.text = score.ToString();
        }
        
        public void GrowUp(float scale, float blendKeyValue)
        {
            StartCoroutine(Growth(scale, blendKeyValue));
        }
        
        public void RemoveFromTemp(Collider collider)
        {
            _temp.Remove(collider);

            if (_temp.Count == 0)
            {
                if (animator.GetBool(attackAnimBool))
                    animator.SetBool(attackAnimBool, false);
            }
        }
        
        public void StartMove()
        {
            StartCoroutine(Move());
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

                rb.velocity = new Vector3(movementVector.x, 0, movementVector.y) * (MonsterData.MovementSpeed +
                              transform.localScale.z);
                transform.LookAt(transform.position + new Vector3(movementVector.x, 0, movementVector.y));

                yield return null;
            }
        }
        
        private void OnTriggerEnter(Collider collider)
        {
            if ((infrastructureLayer.value & (1 << collider.gameObject.layer)) > 0)
            {
                if (!animator.GetBool(attackAnimBool))
                    animator.SetBool(attackAnimBool, true);
                
                _temp.Add(collider);
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if ((infrastructureLayer.value & (1 << collider.gameObject.layer)) > 0)
                RemoveFromTemp(collider);
        }

        private IEnumerator Growth(float scale, float blendKeyValue)
        {
            Vector3 startScale = transform.localScale;
            Vector3 endScale = new Vector3(scale, scale, scale);
            
            float startKeyValue = meshRenderer.GetBlendShapeWeight(0);
            
            float time = 0;

            while (time < 1)
            {
                time += Time.deltaTime;

                transform.localScale = Vector3.Lerp(startScale, endScale, time);
                meshRenderer.SetBlendShapeWeight(0, Mathf.Lerp(startKeyValue, blendKeyValue, time));
                
                yield return null;
            }
        }
        
        private void OnAttack()
        {
            for (int i = 0; i < _temp.Count; i++)
                HitInfrastructureSignal.Dispatch(_temp[i]);
        }
    }
}
