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
    }


    void Start()
    {
        gameInput.BuildingMode.MouseClick.performed += _ => MouseClick();
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
