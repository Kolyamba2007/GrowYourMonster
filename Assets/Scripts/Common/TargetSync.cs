using UnityEngine;

public class TargetSync : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    
    private static readonly int PosID = Shader.PropertyToID("_Position");
    private static readonly int SizeID = Shader.PropertyToID("_Size");
    
    public Material ObjMaterial;
    public Camera Camera;
    public LayerMask Mask;
    
    void Update()
    {
        var dir = Camera.transform.position - targetTransform.position;
        var ray = new Ray(targetTransform.position, dir.normalized);
        
        if(Physics.Raycast(ray, 3000, Mask))
            ObjMaterial.SetFloat(SizeID, 1.2f);
        else
            ObjMaterial.SetFloat(SizeID, 0);
        
        var view = Camera.WorldToViewportPoint(targetTransform.position);
        ObjMaterial.SetVector(PosID, view);
    }
}
