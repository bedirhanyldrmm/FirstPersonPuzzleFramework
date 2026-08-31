using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions inputActions;

    public Vector2 Move => inputActions.Player.Move.ReadValue<Vector2>();
    public Vector2 Look => inputActions.Player.Look.ReadValue<Vector2>();

    public bool Jump => inputActions.Player.Jump.WasPressedThisFrame();
    public bool Sprint => inputActions.Player.Sprint.IsPressed();
    public bool Interact => inputActions.Player.Interact.WasPressedThisFrame();

    public bool Save => inputActions.Player.Save.WasPressedThisFrame();
    public bool Load => inputActions.Player.Load.WasPressedThisFrame();
    public bool Pause => inputActions.Player.Pause.WasPressedThisFrame();
    public bool Inventory => inputActions.Player.Inventory.WasPressedThisFrame();

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerInputActions();
        }

        inputActions.Enable();
    }

    private void OnDisable()
    {
        if (inputActions != null)
        {
            inputActions.Disable();
        }
    }
}