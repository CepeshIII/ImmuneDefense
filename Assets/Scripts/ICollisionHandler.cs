using System;
using UnityEngine;

public class CollisionHandlerArgs : EventArgs
{
    public GameObject other;
}

public interface ICollisionHandler
{
    public void ConnectCollisionListener(ICollisionListener collisionListener);
    public void DisconnectCollisionListener(ICollisionListener collisionListener);
}
