using System;
using UnityEngine;

public class DamageHandler : MonoBehaviour, IDamageHandler
{
    [SerializeField] private float maxHealth = 100f;

    public event EventHandler OnHealthIsZero;

    public void ApplyDamage(float amount)
    {
        maxHealth -= amount;

        if (maxHealth < 0)
        {
            OnHealthIsZero?.Invoke(gameObject, new EventArgs());
            Destroy(gameObject);
        }
    }
}
