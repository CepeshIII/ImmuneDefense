using System.Collections.Generic;
using UnityEngine;

public class EffectsMoveLogic : MonoBehaviour, IMovable, IEffectsModifier, IEffectsHandler
{
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] float moveSpeed = 1f;


    readonly private EffectStorage effectStorage = new();

    private float speedModifier = 1;
    private Vector2 externalForce;
    private Vector2 velocity;


    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }


    public void AddForce(Vector3 force)
    {
        throw new System.NotImplementedException();
    }


    public void AddMove(Vector3 direction, float speed)
    {
        externalForce += ((Vector2)direction) * speed;
    }


    public void MoveToDirection(Vector3 direction)
    {
        //velocity += ((Vector2)direction).normalized * moveForce;

        moveDirection = direction;
    }


    public void SetEffect(Effect effect)
    {
        effectStorage.Add(effect);
    }


    public IEnumerable<Effect> GetEffects(int sourceId)
    {
        return effectStorage.GetEffects(sourceId);
    }



    private void FixedUpdate()
    {
        ResetModifier();
        effectStorage.EffectsProcess(this, this, Time.fixedDeltaTime);

        //rigidbody2D.MovePosition(rigidbody2D.position + Mathf.Min(maxSpeed, speed * speedModifier) * Time.fixedDeltaTime * moveDirection);

        velocity = moveSpeed * speedModifier * Time.fixedDeltaTime * moveDirection.normalized;
        rigidbody2D.MovePosition(rigidbody2D.position + velocity);
        rigidbody2D.AddForce(externalForce);
        velocity = Vector2.zero;
        externalForce = Vector2.zero;
    }


    public void UpdateModifier(float value)
    {
        speedModifier = Mathf.Min(speedModifier, value);
        //speedModifier *= value;
    }

    public void ResetModifier()
    {
        speedModifier = 1;
    }


    public EffectStorage GetStorage()
    {
        return effectStorage;
    }
}