using UnityEngine;

public class RotateAroundY : MonoBehaviour, IRotatable
{
    [SerializeField] private Vector3 targetDirection;

    public void LookRotation(Vector3 direction)
    {
        if (direction.x <= 0)
        {
            transform.rotation = Quaternion.LookRotation(-Vector3.forward);
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward);
        }
    }

    public float GetAngleToTarget()
    {
        return Vector3.SignedAngle(targetDirection, transform.right, Vector3.forward);
    }
}
