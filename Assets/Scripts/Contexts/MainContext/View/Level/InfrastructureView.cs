using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Contexts.MainContext
{
    public class InfrastructureView : IdentifiableView
    {
        [HideInInspector, SerializeField] private string type;
        [SerializeField] private GameObject devidedPrefab;
        [Space, SerializeField] private Animator animator;
        [Space, SerializeField] private RectTransform healthBarRect;
        [SerializeField] private Image healthBarImage;
        [Space, SerializeField] private string triggerName;

        public InfrastructureData InfrastructureData { get; private set; }
        public string Type => type;

        private Coroutine _handleHealthBar;

        protected override void Awake()
        {
            base.Awake();

            healthBarRect.gameObject.SetActive(false);
        }

        public void SetData(InfrastructureData infrastructureData)
        {
            InfrastructureData = infrastructureData;
        }
        
        public void EnableDivide()
        {
            Instantiate(devidedPrefab, transform.position, transform.rotation, transform.parent);
            Destroy(gameObject);
        }

        public void Shake()
        {
            animator.SetTrigger(triggerName);
        }

        public void EnableHealthBar()
        {
            if (_handleHealthBar is null)
                _handleHealthBar = StartCoroutine(HandleHealthBar());
            else
            {
                StopCoroutine(_handleHealthBar);
                _handleHealthBar = StartCoroutine(HandleHealthBar());
            }
        }

        public void UpdateHealthBar(float ratio)
        {
            healthBarImage.fillAmount = ratio;
        }

        private IEnumerator HandleHealthBar()
        {
            Camera mainCamera = Camera.main!;
            healthBarRect.gameObject.SetActive(true);

            float time = 0;
            while (time < 1)
            {
                time += Time.deltaTime;
                healthBarRect.position = mainCamera.WorldToScreenPoint(transform.position);
                
                yield return null;
            }
            
            healthBarRect.gameObject.SetActive(false);
        }
    }
}
