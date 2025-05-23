using UnityEngine;

public class SpotAttack : MonoBehaviour, IAttackable
{
    [SerializeField] private SpotAttackZone spotAttackZone;


    public void Start()
    {
        GetComponentInChildren<SpotAttackZone>();
    }

    public void Attack(Vector3 attackDirection)
    {
        spotAttackZone.StartAttack();
    }
}
