using System.Collections.Generic;
using UnityEngine;

namespace Contexts.MainContext
{
    public class InfrastructureView : IdentifiableView
    {
        [HideInInspector, SerializeField] private string type;
        [SerializeField] private Collider mainCollider;
        [SerializeField] private List<Rigidbody> cells;

        public InfrastructureData InfrastructureData { get; private set; }
        public string Type => type;
        
        protected override void Awake()
        {
            base.Awake();
            
            SetDivideActive(false);
        }

        public void SetData(InfrastructureData infrastructureData)
        {
            InfrastructureData = infrastructureData;
        }
        
        public void SetDivideActive(bool enabled)
        {
            mainCollider.enabled = !enabled;

            foreach (var cell in cells)
                cell.isKinematic = !enabled;
        }
    }
}
