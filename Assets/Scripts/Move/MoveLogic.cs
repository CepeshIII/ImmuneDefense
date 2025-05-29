using UnityEngine;

public class MoveLogic : MonoBehaviour, IMovable
{
    [SerializeField] float speed = 1f;

    public void MoveToDirection(Vector3 direction)
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    public void AddMove(Vector3 direction, float speed)
    {
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    public void AddForce(Vector3 force)
    {
        throw new System.NotImplementedException();
    }
}
