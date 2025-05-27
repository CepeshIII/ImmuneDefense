using System;
using UnityEngine;

public class BuilderInput : MonoBehaviour
{
    private GameInput gameInput;


    private void Awake()
    {
    }


    private void OnEnable()
    {
        gameInput = new GameInput();
        gameInput.Enable();
        gameInput.BuildingMode.MouseClick.performed += _ => MouseClick();
        gameInput.BuildingMode.TouchPress.performed += _ => TouchPress();
    }


    private void TouchPress()
    {
        var screenTouchPosition = gameInput.BuildingMode.TouchPosition.ReadValue<Vector2>();
        var worldMousePosition = Camera.main.ScreenToWorldPoint(screenTouchPosition);

        Builder.Instance.ClickOnBuilder(worldMousePosition);
    }


    private void MouseClick()
    {
        var screenMousePosition = gameInput.BuildingMode.MousePosition.ReadValue<Vector2>();
        var worldMousePosition = Camera.main.ScreenToWorldPoint(screenMousePosition);

        Builder.Instance.ClickOnBuilder(worldMousePosition);
    }


    public void CalculateGridPosition()
    {

    }


    private void OnDisable()
    {
        gameInput.Disable();
    }
}
