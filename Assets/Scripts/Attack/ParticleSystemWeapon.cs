using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemWeapon : MonoBehaviour, IDamageSource
{
    [SerializeField] private ParticleSystem m_ParticleSystem;
    [SerializeField] private bool isReady;
    [SerializeField] private float timeForReloading = 1f;
    [SerializeField] private float damage = 1f;

    public bool IsReady => isReady;

    private float reloadingTimer;
    
    
    
    private void Start()
    {
        m_ParticleSystem = GetComponent<ParticleSystem>();
        isReady = true;
    }

    public void Update()
    {
        if (!isReady) 
        { 
            reloadingTimer -= Time.deltaTime;

            if (reloadingTimer <= 0f) 
            {
                isReady = true;
            }
        }
    }

    public void Shoot(Vector3 attackDirection)
    {
        var angle = Vector3.SignedAngle(Vector3.right, attackDirection, -Vector3.forward);
        //var shapeModule = m_ParticleSystem.shape;
        transform.rotation = Quaternion.Euler(new Vector3(angle, 90f, 90f));

        m_ParticleSystem.Play();
        isReady = false;
        reloadingTimer = timeForReloading;
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.TryGetComponent<DamageHandler>(out var damageHandler))
        {
            damageHandler.ApplyDamage(damage);
        }
    }

    public float GetDamage()
    {
        return damage;
    }
}
