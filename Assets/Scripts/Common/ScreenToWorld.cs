using UnityEngine;

public class ScreenToWorld : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private RectTransform progressBarRect;
    
    private Camera _camera;

    private void Awake()
    {
        transform.parent = transform.parent.parent;
    }

    private void LateUpdate()
    {
        _camera ??= Camera.main!;
        
        progressBarRect.position = _camera.WorldToScreenPoint(parent.position);
    }
}
