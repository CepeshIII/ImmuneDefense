using UnityEngine;

public class BuilderUI : MonoBehaviour
{

    public void Start()
    {
        Builder.Instance.OnBuilderEnable += Builder_OnBuilderEnable;
        Builder.Instance.OnBuilderDisable += Builder_OnBuilderDisable;

        gameObject.SetActive(false);
    }


    private void Builder_OnBuilderDisable(object sender, System.EventArgs e)
    {
        gameObject.SetActive(false);
    }


    private void Builder_OnBuilderEnable(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);

    }
}
