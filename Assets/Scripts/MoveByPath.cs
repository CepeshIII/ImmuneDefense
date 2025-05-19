using UnityEngine;

public class MoveByPath : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Vector3[] path;
    [SerializeField] private int index = 0;

    [SerializeField] private float thresholdDistance = 0.1f;

    private IRotatable rotationLogic;
    private IMovable moveLogic;

    public void Start()
    {
        rotationLogic  = GetComponent<IRotatable>();
        moveLogic = GetComponent<IMovable>();
    }

    public void Update()
    {
        if (lineRenderer == null) return;

        var nextPoint = GetTargetPosition();
        var direction = (nextPoint - transform.position).normalized;

        if (moveLogic != null)
        {
            moveLogic.MoveToDirection(direction);
        }

        if (rotationLogic != null)
        {
            rotationLogic.LookRotation(direction);
        }

        if (Vector3.Distance(transform.position, nextPoint) < thresholdDistance)
        {
            index = (index + 1) % path.Length;
        }
    }


    public Vector3 GetTargetPosition()
    {
        if (path == null || lineRenderer.positionCount != path.Length)
        {
            path = new Vector3[lineRenderer.positionCount];
        }
        lineRenderer.GetPositions(path);

        return path[index];
    }
}

