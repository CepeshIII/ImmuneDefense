using UnityEngine;

public class PhysicsMoveLogic : MonoBehaviour, IMovable
{
    [SerializeField] float moveForce = 1f;
    [SerializeField] Rigidbody2D rigidbody2D;
    [SerializeField] Vector2 velocity;
    [SerializeField] Vector2 externalForce;

    public void MoveToDirection(Vector3 direction)
    {
        //rigidbody2D.AddForce(direction * moveForce);
        velocity += ((Vector2)direction).normalized * moveForce;
        //transform.position += direction.normalized * moveForce * Time.deltaTime;
    }

    public void AddMove(Vector3 direction, float speed)
    {
        externalForce += ((Vector2)direction) * speed ;
        //velocity += ((Vector2)direction).normalized * moveForce;

        //transform.position += direction.normalized * speed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + velocity * Time.fixedDeltaTime);
        rigidbody2D.AddForce(externalForce);
        velocity = Vector2.zero;
        externalForce = Vector2.zero;
    }

    public void AddForce(Vector3 force)
    {
        throw new System.NotImplementedException();
    }
}
