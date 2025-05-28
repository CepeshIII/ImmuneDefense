using UnityEngine;

public class SelfDestroyTimer : MonoBehaviour
{
    [SerializeField] private float timer = 3;

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            DestroyGameObject();
        }
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);

    }
}
