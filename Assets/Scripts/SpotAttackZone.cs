using UnityEngine;

public class SpotAttackZone : MonoBehaviour, ICollisionListener, IContinuesDamageSource
{
    [SerializeField] private float damagePerFrame = 0.01f;
    [SerializeField] private float maxSpeedToCenter = 1f;
    [SerializeField] private float radius = 1f;

    private ICollisionHandler collisionHandler;

    private void Start()
    {
        collisionHandler = GetComponent<ICollisionHandler>();
        if (collisionHandler != null)
            collisionHandler.ConnectCollisionListener(this);
    }

    public void CollisionHandler_OnCollisionEnter(object obj, CollisionHandlerArgs args)
    {
    }

    public void CollisionHandler_OnCollisionExit(object obj, CollisionHandlerArgs args)
    {
    }

    public void CollisionHandler_OnCollisionStay(object obj, CollisionHandlerArgs args)
    {
        var directionToCenter = transform.position - args.other.transform.position;
        var speed = Mathf.Lerp(maxSpeedToCenter, 0, directionToCenter.magnitude / radius);
        args.other.GetComponent<IMovable>().AddMove(directionToCenter, speed);
    }

    public float GetDamage()
    {
        return damagePerFrame;
    }

    private void OnDestroy()
    {
        if (collisionHandler != null)
            collisionHandler.DisconnectCollisionListener(this);
    }
}
