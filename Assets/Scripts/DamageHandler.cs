using UnityEngine;

public class DamageHandler : MonoBehaviour, IDamageHandler
{
    [SerializeField] private float maxHealth = 100f;


    public void ApplyDamage(float amount)
    {
        maxHealth -= amount;

        if (maxHealth < 0)
        {
            Destroy(gameObject);
        }
    }
}
