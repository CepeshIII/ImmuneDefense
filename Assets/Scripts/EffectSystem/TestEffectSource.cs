using System;
using UnityEngine;



public class TestEffectSource : MonoBehaviour, ICollisionListener
{
    private ICollisionHandler collisionHandler;

    [SerializeField] 
    private SlowMovingEffect SlowMovingEffect = new(-1) 
    {
        isStackable = true,
        timer = 1f,
        slowFactor = 0.5f
    };



    void Start()
    {
        collisionHandler = GetComponent<CollisionHandler>();
        if (collisionHandler != null) 
        { 
            collisionHandler.ConnectCollisionListener(this);
        }
    }


    public void CollisionHandler_OnCollisionEnter(object obj, CollisionHandlerArgs args)
    {
        SetEffect(args.other);
    }


    public void CollisionHandler_OnCollisionStay(object obj, CollisionHandlerArgs args)
    {
        SetEffect(args.other);

    }


    public void CollisionHandler_OnCollisionExit(object obj, CollisionHandlerArgs args)
    {
        SetEffect(args.other);
    }


    private void SetEffect(GameObject gameObject)
    {
        var slowMovingEffect = new SlowMovingEffect(GetInstanceID())
        {
            isStackable = SlowMovingEffect.isStackable,
            timer = SlowMovingEffect.timer,
            slowFactor = SlowMovingEffect.slowFactor,
        };

        if (gameObject.TryGetComponent<IEffectsHandler>(out var effectHandler))
        {
            effectHandler.SetEffect(slowMovingEffect);
        }
    }
}
