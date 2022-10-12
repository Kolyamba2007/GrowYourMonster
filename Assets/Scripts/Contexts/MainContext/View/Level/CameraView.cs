using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Contexts.MainContext
{
    public class CameraView : View
    {
        [SerializeField] private Transform targetTransform;

        private Vector3 _startPos;

        protected override void Start()
        {
            base.Start();

            _startPos = transform.position - targetTransform.position;
            StartCoroutine(MoveToTarget(targetTransform));
        }

        private IEnumerator MoveToTarget(Transform target)
        {
            while (true)
            {
                transform.position = targetTransform.position + _startPos;
            
                yield return null;
            }
        }
    }
}