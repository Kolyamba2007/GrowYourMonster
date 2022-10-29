using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform _camera;

    private void Awake() =>
        _camera = Camera.main.transform;

    void LateUpdate() =>
        transform.LookAt(transform.position + _camera.forward);
}
