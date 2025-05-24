using UnityEngine;


public class ParticleSystemAttack : MonoBehaviour, IAttackable
{
    [SerializeField] private ParticleSystemWeapon _weapon;


    public void Start()
    {
        _weapon = GetComponentInChildren<ParticleSystemWeapon>();
    }


    public void Attack(Vector3 attackDirection)
    {
        if (_weapon != null && _weapon.IsReady) 
        { 
            _weapon.Shoot(attackDirection);
        }
    }
}
