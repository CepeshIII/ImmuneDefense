using UnityEngine;

public interface IMovable
{
    public void MoveToDirection(Vector3 direction);
    public void AddMove(Vector3 direction, float speed);
}
