using System;
using UnityEngine;

public static class LayersMethods
{
    public static bool CheckInMask(LayerMask layerMask, int layer)
    {
        return (layerMask.value & (1 << layer)) > 0;
    }
}

public class CollisionHandler : MonoBehaviour, ICollisionHandler
{
    public LayerMask layerMask;
    public event EventHandler<CollisionHandlerArgs> onCollisionEnter;
    public event EventHandler<CollisionHandlerArgs> onCollisionStay;
    public event EventHandler<CollisionHandlerArgs> onCollisionExit;

    public void ConnectCollisionListener(ICollisionListener collisionListener)
    {
        onCollisionEnter += collisionListener.CollisionHandler_OnCollisionEnter;
        onCollisionExit += collisionListener.CollisionHandler_OnCollisionExit;
        onCollisionStay += collisionListener.CollisionHandler_OnCollisionStay;
    }

    public void DisconnectCollisionListener(ICollisionListener collisionListener)
    {
        onCollisionEnter -= collisionListener.CollisionHandler_OnCollisionEnter;
        onCollisionExit -= collisionListener.CollisionHandler_OnCollisionExit;
        onCollisionStay -= collisionListener.CollisionHandler_OnCollisionStay;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayersMethods.CheckInMask(layerMask, collision.otherCollider.gameObject.layer))
        {
            onCollisionEnter?.Invoke(this, new CollisionHandlerArgs { other = collision.otherCollider.gameObject });
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (LayersMethods.CheckInMask(layerMask, collider.gameObject.layer))
        {
            onCollisionEnter?.Invoke(this, new CollisionHandlerArgs { other = collider.gameObject });
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (LayersMethods.CheckInMask(layerMask, collision.otherCollider.gameObject.layer))
        {
            onCollisionStay?.Invoke(this, new CollisionHandlerArgs { other = collision.otherCollider.gameObject });
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (LayersMethods.CheckInMask(layerMask, collider.gameObject.layer))
        {
            onCollisionStay?.Invoke(this, new CollisionHandlerArgs { other = collider.gameObject });
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (LayersMethods.CheckInMask(layerMask, collision.otherCollider.gameObject.layer))
        {
            onCollisionExit?.Invoke(this, new CollisionHandlerArgs { other = collision.otherCollider.gameObject });
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (LayersMethods.CheckInMask(layerMask, collider.gameObject.layer))
        {
            onCollisionExit?.Invoke(this, new CollisionHandlerArgs { other = collider.gameObject });
        }
    }
}
