using UnityEngine;

public class DamageZone : MonoBehaviour, IDamageSource, ICollisionListener
{
    [SerializeField] private float damage;
    [SerializeField] private ICollisionHandler collisionHandler;

    public void Start()
    {
        collisionHandler = GetComponent<ICollisionHandler>();

        if (collisionHandler != null)
        {
            collisionHandler.ConnectCollisionListener(this);
        }
    }

    public void CollisionHandler_OnCollisionEnter(object obj, CollisionHandlerArgs args)
    {
        if (args.other.TryGetComponent<IDamageHandler>(out var damageHandler))
        {
            damageHandler.ApplyDamage(damage);
        }
    }

    public void CollisionHandler_OnCollisionExit(object obj, CollisionHandlerArgs args)
    {
    }

    public void CollisionHandler_OnCollisionStay(object obj, CollisionHandlerArgs args)
    {
    }

    public float GetDamage()
    {
        return damage;
    }

    public void OnDestroy()
    {
        if (collisionHandler != null)
        {
            collisionHandler.DisconnectCollisionListener(this);
        }
    }
}
