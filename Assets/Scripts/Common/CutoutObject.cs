using UnityEngine;

public class CutoutObject : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private Material objMaterial;
    [SerializeField] private LayerMask wallMask;

    private Camera _mainCamera;
    private static readonly int PosID = Shader.PropertyToID("_CutoutPos");
    private static readonly int SizeID = Shader.PropertyToID("_CutoutSize");

    private void LateUpdate()
    {
        _mainCamera ??= Camera.main!;

        var dir = _mainCamera.transform.position - targetObject.position;
        var ray = new Ray(targetObject.position, dir.normalized);

        if (Physics.Raycast(ray, dir.magnitude, wallMask))
            objMaterial.SetFloat(SizeID, 0.4f);
        else
            objMaterial.SetFloat(SizeID, 0);
        
        var view = _mainCamera.WorldToViewportPoint(targetObject.position);
        objMaterial.SetVector(PosID, view);
    }
}
