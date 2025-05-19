using UnityEngine;

public class Macrophage : MonoBehaviour, ICollisionListener
{
    [SerializeField] private VisionZone zone;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Transform target;
    [SerializeField] private IAttackable attackLogic;

    private IRotatable rotateLogic;


    private void Start()
    {
        if(zone != null)
        {
            zone.ConnectCollisionListener(this);
        }
        rotateLogic = GetComponent<IRotatable>();
        attackLogic = GetComponentInChildren<IAttackable>();
    }

    public void Update()
    {
        if(target != null)
        {
            var directionToTarget = target.position - transform.position;
            rotateLogic.LookRotation(directionToTarget);
            attackLogic.Attack(directionToTarget);
        }
    }

    public void CollisionHandler_OnCollisionEnter(object obj, CollisionHandlerArgs args)
    {
        if(target == null || Vector3.Distance(target.position, transform.position) > Vector3.Distance(args.other.transform.position, transform.position) ) 
            target = args.other.transform;
        targetPosition = args.other.transform.position;
    }

    public void CollisionHandler_OnCollisionStay(object obj, CollisionHandlerArgs args)
    {
        if (target == null || Vector3.Distance(target.position, transform.position) > Vector3.Distance(args.other.transform.position, transform.position))
            target = args.other.transform;
    }

    public void CollisionHandler_OnCollisionExit(object obj, CollisionHandlerArgs args)
    {
        if (target == args.other.transform)
            target = null;
    }

    private void OnDestroy()
    {
        zone.DisconnectCollisionListener(this);
    }

}
