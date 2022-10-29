using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Contexts.MainContext
{
    public class CameraView : View
    {
        public Signal ReachedFinalPositionSignal { get; } = new Signal();
        
        [SerializeField] private Transform parentTransform;
        [SerializeField] private Transform targetTransform;
        [SerializeField] private Transform startState;
        [SerializeField] private Transform endState;

        private Vector3 _startPos;

        public void DestroyView()
        {
            Destroy(parentTransform.gameObject);
        }

        public void StartTransition()
        {
            StartCoroutine(Transition());
        }
        
        private IEnumerator Transition()
        {
            float time = 0;

            while (time < 1)
            {
                time += Time.deltaTime;

                transform.localPosition = Vector3.Lerp(startState.localPosition, endState.localPosition, time);
                transform.localRotation = Quaternion.Lerp(startState.localRotation, endState.localRotation, time);

                yield return null;
            }

            ReachedFinalPositionSignal.Dispatch();
            
            parentTransform.parent = parentTransform.parent.parent;
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