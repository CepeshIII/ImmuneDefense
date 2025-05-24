using UnityEngine;

public class EndPoint : MonoBehaviour, ICollisionListener
{

    public CollisionHandler collisionHandler;



    public void Start()
    {
        if(TryGetComponent<CollisionHandler>(out var collisionHandler))
        {
            collisionHandler.ConnectCollisionListener(this);
        }
    }


    public void CollisionHandler_OnCollisionEnter(object obj, CollisionHandlerArgs args)
    {
        GameManager.Instance.TrigerOnMissEnemy();
        Destroy(args.other);
    }


    public void CollisionHandler_OnCollisionExit(object obj, CollisionHandlerArgs args)
    {
    }


    public void CollisionHandler_OnCollisionStay(object obj, CollisionHandlerArgs args)
    {
    }

    private void OnDestroy()
    {
        if(collisionHandler != null)
            collisionHandler.DisconnectCollisionListener(this);
    }
}
