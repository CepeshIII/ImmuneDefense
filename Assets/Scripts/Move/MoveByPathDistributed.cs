using UnityEngine;
using UnityEngine.UIElements;

public class MoveByPathDistributed : MonoBehaviour, IMoveByPath
{
    [SerializeField] private Vector3[] path;
    [SerializeField] private int index = 0;

    [SerializeField] private float thresholdDistance = 0.1f;
    [SerializeField] private float pathRadius = 1f;

    private IRotatable rotationLogic;
    private IMovable moveLogic;



    public void Start()
    {
        rotationLogic = GetComponent<IRotatable>();
        moveLogic = GetComponent<IMovable>();
    }


    public void Update()
    {
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
        CheckDistanceToNexPathPoint();
    }


    public void CheckDistanceToNexPathPoint()
    {
        if ((path[index] -GetProjection()).magnitude < thresholdDistance)
        {
            if (path.Length != 0)
                index = (index + 1) % path.Length;
        }
    }


    public void SetPath(Vector3[] newPath)
    {
        path = newPath;
    }


    public void SetThresholdDistance(float newThresholdDistance)
    {
        thresholdDistance = newThresholdDistance;
    }


    public void SetPathRadius(float newPathRadius)
    {
        pathRadius = newPathRadius;
    }


    public Vector3 GetTargetPosition()
    {
        if (path.Length > index)
        {
            if(path.Length >= 2)
            {
                return GetProjectedTargetPosition();
            }

            return path[index];
        }
        else
        {
            return transform.position;
        }
    }


    public Vector3 GetProjection()
    {
        Vector3 pathDirection = GetPathDirection();
        if (pathDirection == Vector3.zero) return path[index];

        //var projectedPosition = Vector3.Project(transform.position, pathDirection);
        var projectedPosition =
            Vector3.Project(transform.position - path[index], pathDirection) + path[index];
        return projectedPosition;
    }

    public Vector3 GetPathDirection()
    {
        if(path.Length < 2)
        {
            return Vector3.zero;
        }

        var currentPathPoint = path[index];
        var nextIndex = index + 1;
        var previouslyIndex = index - 1;

        Vector3 pathDirection;
        if (previouslyIndex >= 0)
        {
            Vector3 previouslyPathPoint = path[previouslyIndex];
            pathDirection = (currentPathPoint - previouslyPathPoint).normalized;
        }
        else if (nextIndex < path.Length)
        {
            Vector3 nextPathPoint = path[nextIndex];
            pathDirection = (nextPathPoint - currentPathPoint).normalized;
        }
        else
        {
            return path[nextIndex];
        }

        return pathDirection;
    }


    public Vector3 GetProjectedTargetPosition()
    {
        var vectorToProjection = transform.position - GetProjection();
        var projectedTargetPosition = vectorToProjection + path[index];

        if (vectorToProjection.magnitude <= pathRadius)
        {
            return projectedTargetPosition;
        }
        return path[index];
    }

    
    public int GetIndex()
    {
        return index;
    }

    public Vector3[] GetPath()
    {
        return path;
    }
}

