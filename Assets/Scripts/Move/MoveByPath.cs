using System;
using UnityEngine;

public class MoveByPath : MonoBehaviour
{
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
            if(path.Length != 0)
                index = (index + 1) % path.Length;
        }
    }


    public void SetPath(Vector3[] newPath)
    {
        path = newPath;
    }

    public Vector3 GetTargetPosition()
    {
        if(path.Length > index)
            return path[index];
        else return transform.position;
    }

}

