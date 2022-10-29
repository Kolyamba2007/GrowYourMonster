using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Contexts.MainContext
{
    public class InfrastructureView : IdentifiableView
    {
        [HideInInspector, SerializeField] private string type;
        [SerializeField] private GameObject devidedPrefab;
        [SerializeField] private Animator animator;
        [SerializeField] private RectTransform healthBarRect;
        [SerializeField] private string triggerName;

        public InfrastructureData InfrastructureData { get; private set; }
        public string Type => type;

        private Slider _healthBarSlider;
        private Coroutine _handleHealthBar;

        public void SetData(InfrastructureData infrastructureData)
        {
            InfrastructureData = infrastructureData;

            _healthBarSlider = healthBarRect.GetComponent<Slider>();
            _healthBarSlider.maxValue = InfrastructureData.Health;
            _healthBarSlider.value = InfrastructureData.Health;
            
            healthBarRect.gameObject.SetActive(false);
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

        public void UpdateHealthBar(int value)
        {
            _healthBarSlider.value = value;
        }

        private IEnumerator HandleHealthBar()
        {
            healthBarRect.gameObject.SetActive(true);

            float time = 0;
            while (time < 1)
            {
                time += Time.deltaTime;
                healthBarRect.position = Camera.main.WorldToScreenPoint(transform.position);
                
                yield return null;
            }
            
            healthBarRect.gameObject.SetActive(false);
        }
    }
}
