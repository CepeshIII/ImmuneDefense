using System;
using UnityEngine;

public class NoneRotation : MonoBehaviour, IRotatable
{
    public event EventHandler<IRotatable.OnTargetChangeArgs> OnTargetChange;

    public float GetAngleToTarget()
    {
        return 0;
    }

    public void LookRotation(Vector3 direction)
    {
        OnTargetChange?.Invoke(this, new IRotatable.OnTargetChangeArgs
        {
            targetDirection = direction,
        });
    }
}
