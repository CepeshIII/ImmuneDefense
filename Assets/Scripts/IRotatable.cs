using System;
using UnityEngine;

public interface IRotatable
{
    public event EventHandler<OnTargetChangeArgs> OnTargetChange;

    public class OnTargetChangeArgs : EventArgs
    {
        public Vector3 targetDirection;
    }


    public void LookRotation(Vector3 direction);
    public float GetAngleToTarget();
}
