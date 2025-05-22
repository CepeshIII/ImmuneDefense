using System;
using UnityEngine;

public class TileManagerInput : MonoBehaviour
{
    private GameInput gameInput;


    private void Awake()
    {
        gameInput = new GameInput();
    }


    private void OnEnable()
    {
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

        Builder.Instance.BuildByPosition(worldMousePosition);
    }

    public void CalculateGridPosition()
    {

    }

    private void OnDisable()
    {
        gameInput.Disable();
    }
}
