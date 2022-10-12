using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Contexts.MainContext
{
    public class InfrastructureView : View
    {
        [SerializeField] private Collider mainCollider;
        [SerializeField] private List<Rigidbody> cells;

        public InfrastructureData InfrastructureData { get; private set; }
        
        public ushort ID { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            
            SetDivideActive(false);
        }

        public void SetData(InfrastructureData infrastructureData)
        {
            InfrastructureData = infrastructureData;
        }
        
        public void SetID(ushort id)
        {
            ID = id;
        }

        public void SetDivideActive(bool enabled)
        {
            mainCollider.enabled = !enabled;

            foreach (var cell in cells)
                cell.isKinematic = !enabled;
        }
    }
}
