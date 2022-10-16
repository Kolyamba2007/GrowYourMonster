using System;
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
                transform.position = targetTransform.position + new Vector3(_startPos.x,
                    _startPos.y + (targetTransform.localScale.y - 1) * 10,
                    _startPos.z + (targetTransform.localScale.z - 1) * 10);
            
                yield return null;
            }
        }
    }
}