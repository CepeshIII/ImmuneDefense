using UnityEngine;

public class RenderLinePathSource : MonoBehaviour, IPathSource
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Vector3[] path;


    public void Start()
    {
        if(lineRenderer != null)
        {
            if(path == null || lineRenderer.positionCount != path.Length)
            {
                path = new Vector3[lineRenderer.positionCount];
            }
            lineRenderer.GetPositions(path);
        }
    }

    public Vector3[] GetPath()
    {
        return path;
    }
}
