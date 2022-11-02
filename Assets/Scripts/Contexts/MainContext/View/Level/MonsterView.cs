using System.Collections;
using System.Collections.Generic;
using strange.extensions.signal.impl;
using TMPro;
using UnityEngine;

namespace Contexts.MainContext
{
    public abstract class MonsterView : IdentifiableView
    {
        public Signal<Collider> HitInfrastructureSignal { get; } = new Signal<Collider>();
        public Signal<Collider> HitEnemySignal { get; } = new Signal<Collider>();
        
        [Space, SerializeField] protected Animator animator;
        [SerializeField] protected string walkAnimBool;
        [SerializeField] protected string attackAnimBool;
        [SerializeField] protected string attackAnimName;
        [SerializeField] protected string moveBlendParamName;
        [Space, SerializeField] private LayerMask infrastructureLayer;
        [SerializeField] private LayerMask monsterLayer;
        [SerializeField] private SkinnedMeshRenderer meshRenderer;
        [Space, SerializeField] private TMP_Text scoreText;
        [Space, SerializeField] private GameObject progressBar;

        public MonsterData MonsterData { get; protected set; }
        
        protected Coroutine AttackCoroutine;
        protected Coroutine MoveCoroutine;

        protected readonly List<Collider> Temp = new List<Collider>();

        public void UpdateScore(int score)
        {
            scoreText.text = score.ToString();
        }
        
        public void GrowUp(float scale, float blendShapeValue, float blendAnimationValue)
        {
            StartCoroutine(Growth(scale, blendShapeValue, blendAnimationValue));
        }

        public abstract void FinishAttack(Collider collider);
        
        public void StartMove()
        {
            MoveCoroutine = StartCoroutine(Move());
        }

        public void DestroyView()
        {
            Destroy(gameObject);
        }
        
        public void SetProgressBarActive()
        {
            progressBar.SetActive(true);
        }

        protected override void Start()
        {
            base.Start();

            progressBar.SetActive(false);
        }

        protected abstract IEnumerator Move();
        protected abstract IEnumerator Attack();
        
        private void OnTriggerEnter(Collider collider)
        {
            if ((infrastructureLayer.value & (1 << collider.gameObject.layer)) > 0)
            {
                if (AttackCoroutine is null)
                    AttackCoroutine = StartCoroutine(Attack());
                if (!animator.GetBool(attackAnimBool))
                    animator.SetBool(attackAnimBool, true);
                
                Temp.Add(collider);
            }
            else if ((monsterLayer.value & (1 << collider.gameObject.layer)) > 0)
            {
                HitEnemySignal.Dispatch(collider);
                animator.Play(attackAnimName);
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if ((infrastructureLayer.value & (1 << collider.gameObject.layer)) > 0)
                FinishAttack(collider);
        }

        private IEnumerator Growth(float scale, float blendShapeValue, float blendAnimationValue)
        {
            Vector3 startScale = transform.localScale;
            Vector3 endScale = new Vector3(scale, scale, scale);
            
            float startShapeValue = meshRenderer.GetBlendShapeWeight(0);
            float startMoveValue = animator.GetFloat(moveBlendParamName);
            
            float time = 0;
            
            while (time < 1)
            {
                time += Time.deltaTime;

                transform.localScale = Vector3.Lerp(startScale, endScale, time);
                meshRenderer.SetBlendShapeWeight(0, Mathf.Lerp(startShapeValue, blendShapeValue, time));
                animator.SetFloat(moveBlendParamName, Mathf.Lerp(startMoveValue, blendAnimationValue, time));
                
                yield return null;
            }
        }
    }
}
