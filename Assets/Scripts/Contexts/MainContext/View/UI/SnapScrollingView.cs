using System.Collections.Generic;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Contexts.MainContext
{
    public class SnapScrollingView : View
    {
        public Signal DestroyPreviousSignal { get; } = new Signal();

        [SerializeField] private List<MonsterStruct> monsterStructs;
        [SerializeField] private Transform instantiateParent;
        [SerializeField] private float spacing;
        [SerializeField] private float snapSpeed;

        private List<GameObject> _instPans;
        private List<Vector2> _pansPos;

        private RectTransform _contentRect;
        private Vector2 _contentVec;

        private int _selectedPanID;
        private bool _isScrolling;

        public void HandleScrollingEvent(bool scroll)
        {
            if (!scroll)
            {
                DestroyPreviousSignal.Dispatch();
                Instantiate(monsterStructs[_selectedPanID].MonsterPrefab, instantiateParent);
            }
            
            _isScrolling = scroll;
        }

        protected override void Start()
        {
            base.Start();

            _contentRect = GetComponent<RectTransform>();
            _instPans = new List<GameObject>();
            _pansPos = new List<Vector2>();

            for (int i = 0; i < monsterStructs.Count; i++)
            {
                _instPans.Add(Instantiate(monsterStructs[i].MonsterImage, transform, false));

                if (i == 0)
                {
                    _pansPos.Add(-_instPans[i].transform.localPosition);
                    continue;
                }

                _instPans[i].transform.localPosition = new Vector2(
                    _instPans[i - 1].transform.localPosition.x +
                    monsterStructs[i].MonsterImage.GetComponent<RectTransform>().sizeDelta.x + spacing,
                    _instPans[i].transform.localPosition.y);

                _pansPos.Add(-_instPans[i].transform.localPosition);
            }
        }

        private void FixedUpdate()
        {
            float nearestPos = float.MaxValue;
            for (int i = 0; i < _pansPos.Count; i++)
            {
                float distance = Mathf.Abs(_contentRect.anchoredPosition.x - _pansPos[i].x);
                if (distance < nearestPos)
                {
                    nearestPos = distance;
                    _selectedPanID = i;
                }
            }

            if (_isScrolling) return;
            _contentVec.x = Mathf.SmoothStep(_contentRect.anchoredPosition.x, _pansPos[_selectedPanID].x,
                snapSpeed * Time.fixedDeltaTime);
            _contentRect.anchoredPosition = _contentVec;
        }
    }
}