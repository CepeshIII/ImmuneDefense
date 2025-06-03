using UnityEngine;

public class SpotAttackZone : MonoBehaviour, ICollisionListener, IDamageSource
{
    [SerializeField] private float maxDamagePerFrame = 0.1f;
    [SerializeField] private float maxForceToCenter = 40f;
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isReady = true;

    private ICollisionHandler collisionHandler;

    private void Start()
    {
        collisionHandler = GetComponent<ICollisionHandler>();
        if (collisionHandler != null)
            collisionHandler.ConnectCollisionListener(this);

        animator = GetComponent<Animator>();
    }

    public void StartAttack()
    {
        if (isReady)
        {
            animator.SetTrigger("Attack");
            isReady = false;
        }
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
        var interpolationFactor = directionToCenter.magnitude / (radius * transform.lossyScale.x);

        if (args.other.TryGetComponent<IDamageHandler>(out var damageHandler))
        {
            var damage = Mathf.Lerp(maxDamagePerFrame, 0, interpolationFactor);
            damageHandler.ApplyDamage(damage);
        };

        if (args.other.TryGetComponent<IMovable>(out var moveLogic))
        {
            var speed = Mathf.Lerp(maxForceToCenter, 0, interpolationFactor);
            moveLogic.AddMove(directionToCenter, speed);
        };
    }

    public float GetDamage()
    {
        return maxDamagePerFrame;
    }

    private void OnDestroy()
    {
        if (collisionHandler != null)
            collisionHandler.DisconnectCollisionListener(this);
    }
}
