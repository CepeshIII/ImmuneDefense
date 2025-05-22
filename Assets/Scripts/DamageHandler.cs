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
            maxHealth -= damageSource.GetDamage();

            if (maxHealth < 0) 
            { 
                gameObject.SetActive(false);
            }
        }
    }

    public void CollisionHandler_OnCollisionExit(object obj, CollisionHandlerArgs args)
    {
    }

    public void CollisionHandler_OnCollisionStay(object obj, CollisionHandlerArgs args)
    {
    }

    private void OnDestroy()
    {
        if (collisionHandler != null)
        {
            collisionHandler.DisconnectCollisionListener(this);
        }
    }
}
