using System;
using UnityEngine;

public class RotateAroundZ : MonoBehaviour, IRotatable
{
    [SerializeField] VectorDirection forwardDirection;
    [SerializeField] float rotationSpeed = 1f;

    public enum VectorDirection 
    { 
        Up, 
        Right,
        Left,
        Down,
    }

    private Vector3 targetDirection;

    public event EventHandler<IRotatable.OnTargetChangeArgs> OnTargetChange;

    public Vector3 Forward { 
        get {
            switch (forwardDirection)
            {
                default:
                case VectorDirection.Right:
                    return transform.right;
                case VectorDirection.Up:
                    return transform.up;
                case VectorDirection.Left:
                    return -transform.right;
                case VectorDirection.Down:
                    return -transform.up;
            }
        }
    }



    public void LookRotation(Vector3 direction)
    {
        Quaternion newRotation;
        targetDirection = direction;

        switch (forwardDirection)
        {
            default:
            case VectorDirection.Right:
                newRotation = Quaternion.LookRotation(Vector3.forward, new Vector3(-direction.y, direction.x));
                break;
            case VectorDirection.Up:
                newRotation = Quaternion.LookRotation(Vector3.forward, new Vector3(direction.x, direction.y));
                break;
            case VectorDirection.Left:
                newRotation = Quaternion.LookRotation(Vector3.forward, - new Vector3(-direction.y, direction.x));
                break;
            case VectorDirection.Down:
                newRotation = Quaternion.LookRotation(Vector3.forward, - new Vector3(direction.x, direction.y));
                break;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        OnTargetChange?.Invoke(this, new IRotatable.OnTargetChangeArgs
        {
            targetDirection = targetDirection,
        });
    }


    public float GetAngleToTarget()
    {
        return Vector3.SignedAngle(Forward, targetDirection, Vector3.forward);
    }
}
