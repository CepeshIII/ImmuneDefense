using UnityEngine;

public class DamageHandler : MonoBehaviour, ICollisionListener
{
    [SerializeField] private float maxHealth = 100f;
    private ICollisionHandler collisionHandler;

    public void Start()
    {
        collisionHandler = GetComponent<CollisionHandler>();
        if(collisionHandler != null)
        {
            collisionHandler.ConnectCollisionListener(this);
        }
    }

    public void CollisionHandler_OnCollisionEnter(object obj, CollisionHandlerArgs args)
    {
        if(args.other.gameObject.TryGetComponent<IDamageSource>(out var damageSource)){
            DamageProcess(damageSource.GetDamage());
        }
    }

    public void CollisionHandler_OnCollisionStay(object obj, CollisionHandlerArgs args)
    {
        if (args.other.gameObject.TryGetComponent<IContinuesDamageSource>(out var damageSource))
        {
            DamageProcess(damageSource.GetDamage());
        }
    }

    public void CollisionHandler_OnCollisionExit(object obj, CollisionHandlerArgs args)
    {

    }

    public void DamageProcess(float damage)
    {
        maxHealth -= damage;

        if (maxHealth < 0)
        {
            Destroy(gameObject);
        }
    }



    private void OnDestroy()
    {
        if (collisionHandler != null)
        {
            collisionHandler.DisconnectCollisionListener(this);
        }
    }
}
