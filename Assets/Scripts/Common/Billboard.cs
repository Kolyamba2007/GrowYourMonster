using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform _camera;

    void LateUpdate()
    {
        _camera ??= Camera.main!.transform;

        transform.LookAt(transform.position + _camera.forward);
    }
}
