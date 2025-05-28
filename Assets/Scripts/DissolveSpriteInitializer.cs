using UnityEngine;

[RequireComponent(typeof(DamageHandler))]
public class DissolveSpriteInitializer : MonoBehaviour
{
    [SerializeField] private GameObject dissolveSpritePrefab;
    [SerializeField] private DamageHandler damageHandler;

    [SerializeField] private SpriteSource spriteSource;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite sprite;

    public enum SpriteSource
    {
        Sprite,
        SpriteRender
    }


    void Start()
    {
        if(TryGetComponent<DamageHandler>(out damageHandler))
        {
            damageHandler.OnHealthIsZero += DamageHandler_OnHealthIsZero;
        }
    }


    private void DamageHandler_OnHealthIsZero(object sender, System.EventArgs e)
    {
        if (spriteRenderer != null && dissolveSpritePrefab != null) 
        {
            var dissolveSpriteGameObject =  Instantiate(dissolveSpritePrefab, spriteRenderer.transform.position, spriteRenderer.transform.rotation);
            dissolveSpriteGameObject.transform.localScale = spriteRenderer.transform.lossyScale;

            if (dissolveSpriteGameObject.TryGetComponent<SpriteRenderer>(out var dissolveSpriteRenderer))
            {
                switch (spriteSource)
                {
                    case SpriteSource.Sprite:
                        dissolveSpriteRenderer.sprite = sprite;
                        break;
                    case SpriteSource.SpriteRender:
                        dissolveSpriteRenderer.sprite = spriteRenderer.sprite;
                        break;
                }
            }
        }
    }

}
