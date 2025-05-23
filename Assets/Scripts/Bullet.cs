using UnityEngine;

public class Bullet : MonoBehaviour, IDamageSource, ICollisionListener
{
    [SerializeField] private float damage;
    [SerializeField] private ICollisionHandler collisionHandler;
    [SerializeField] private float selfDestroyTimer = 5f;



    public void Start()
    {
        collisionHandler = GetComponent<ICollisionHandler>();

        if (collisionHandler != null) 
        {
            collisionHandler.ConnectCollisionListener(this);
        }
    }

    public void Update()
    {
        selfDestroyTimer -= Time.deltaTime;
        if(selfDestroyTimer < 0)
        {
            Destroy(gameObject);
        }
    }

    public void CollisionHandler_OnCollisionEnter(object obj, CollisionHandlerArgs args)
    {
        Destroy(gameObject);
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
