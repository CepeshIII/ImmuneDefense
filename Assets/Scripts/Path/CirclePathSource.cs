using UnityEngine;

public class CirclePathSource : MonoBehaviour, IPathSource
{
    [SerializeField] private Vector3[] path;
    [SerializeField] float radius = 1f;
    [SerializeField] int pointCount = 144;

    Vector3 center = Vector3.zero;


    public void Awake()
    {
        path = new Vector3[pointCount];
        var angleOffset = 360f / pointCount;
        var PIDivide180 = Mathf.PI / 180f;
        for (int i = 0; i < pointCount; i++) 
        {
            var angle = (angleOffset * i) * PIDivide180;

            var position = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            path[i] = position;
        }
    }


    public Vector3[] GetPath()
    {
        return path;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach (var center in path) 
        { 
            Gizmos.DrawSphere(center, 0.05f);
        }
    }
}
