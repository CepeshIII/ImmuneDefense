using UnityEngine;

public class MoveLogic : MonoBehaviour, IMovable
{
    [SerializeField] float speed = 1f;

    public void MoveToDirection(Vector3 direction)
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
