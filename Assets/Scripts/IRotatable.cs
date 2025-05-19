using UnityEngine;

public interface IRotatable
{
    public void LookRotation(Vector3 direction);
    public float GetAngleToTarget();
}
