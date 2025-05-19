using UnityEngine;

public class InfightingAttack : MonoBehaviour, IAttackable
{
    [SerializeField] private Transform hand;
    [SerializeField] private GameObject weapon;
    [SerializeField] private IRotatable rotationLogic;

    [SerializeField] private float attackThreshold = 1f;


    private void Start()
    {
        rotationLogic = GetComponent<IRotatable>();
    }

    public void Attack(Vector3 attackDirection)
    {
        if (rotationLogic != null) 
        {
            var angle = rotationLogic.GetAngleToTarget();

            if (Mathf.Abs(angle) < attackThreshold) 
            {
                Instantiate(weapon, hand.position, transform.rotation);
            }
        }


    }
}

