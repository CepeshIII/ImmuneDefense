using System;
using UnityEngine;


public class VisionZone: MonoBehaviour, ICollisionHandler
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
        if ((layerMask.value & (1 << collision.otherCollider.gameObject.layer)) > 0)
        {
            onCollisionEnter?.Invoke(this, new CollisionHandlerArgs { other = collision.otherCollider.gameObject });
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if ((layerMask.value & (1 << collider.gameObject.layer)) > 0)
        {
            onCollisionEnter?.Invoke(this, new CollisionHandlerArgs { other = collider.gameObject });
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((layerMask.value & (1 << collision.otherCollider.gameObject.layer)) > 0)
        {
            onCollisionStay?.Invoke(this, new CollisionHandlerArgs { other = collision.otherCollider.gameObject });
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if ((layerMask.value & (1 << collider.gameObject.layer)) > 0)
        {
            onCollisionStay?.Invoke(this, new CollisionHandlerArgs { other = collider.gameObject });
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((layerMask.value & (1 << collision.otherCollider.gameObject.layer)) > 0)
        {
            onCollisionExit?.Invoke(this, new CollisionHandlerArgs { other = collision.otherCollider.gameObject });
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if ((layerMask.value & (1 << collider.gameObject.layer)) > 0)
        {
            onCollisionExit?.Invoke(this, new CollisionHandlerArgs { other = collider.gameObject });
        }
    }
}
