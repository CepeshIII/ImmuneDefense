using UnityEngine;


public class EyesManager : MonoBehaviour
{
    [SerializeField] private Transform pupils;
    [SerializeField] private float radius = 0.1f;

    private Vector2 lookDirection;

    public void Start()
    {
        var parent = GetComponentInParent<IRotatable>();

        if(parent != null)
        {
            parent.OnTargetChange += IRotatable_OnTargetChange;
        }
    }

    private void IRotatable_OnTargetChange(object sender, IRotatable.OnTargetChangeArgs e)
    {
        lookDirection = e.targetDirection;
    }

    public void Update()
    {
        if (lookDirection != Vector2.zero)
            pupils.localPosition = lookDirection.normalized * radius;
        else
            pupils.localPosition = Vector2.zero;

    }
}
