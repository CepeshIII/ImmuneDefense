using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private IDamageHandler damageHandler;
    [SerializeField] private int reward;


    void Start()
    {
        damageHandler = GetComponent<IDamageHandler>();
        damageHandler.OnHealthIsZero += DamageHandler_OnHealthIsZero;
    }


    private void DamageHandler_OnHealthIsZero(object sender, System.EventArgs e)
    {
        GameManager.Instance.TrigerOnEnemyDied(reward);
    }


    void Update()
    {
        
    }
}
