using UnityEngine;

public class ShootAttack : MonoBehaviour, IAttackable
{
    [SerializeField] private GameObject weapon;
    [SerializeField] private IRotatable rotationLogic;

    [SerializeField] private float attackThreshold = 1f;
    [SerializeField] private float bulletForce = 100f;
    [SerializeField] private float startReloadingDelay = 1f;

    private float currentReloadingDelay;
    private bool isReloading = false;


    private void Start()
    {
        rotationLogic = GetComponent<IRotatable>();
    }

    private void Update()
    {
        if (isReloading)
        {
            currentReloadingDelay -= Time.deltaTime;
            if (currentReloadingDelay <= 0)
            {
                isReloading = false;
            }
        }
    }


    public void Attack(Vector3 attackDirection)
    {
        if (!isReloading && rotationLogic != null)
        {
            var angle = rotationLogic.GetAngleToTarget();

            if (Mathf.Abs(angle) < attackThreshold)
            {
                var bullet = Instantiate(weapon, transform.position, transform.rotation);
                bullet.GetComponent<Rigidbody2D>().AddForce(attackDirection * bulletForce) ;
                isReloading = true;
                currentReloadingDelay = startReloadingDelay;
            }
        }
    }
}
