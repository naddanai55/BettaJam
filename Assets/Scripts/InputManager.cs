using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputAction toggleAction;
    private GameManager gameManager;

    void OnEnable()
    {
        if (toggleAction != null)
        {
            toggleAction.Enable();
        }
    }

    void OnDisable()
    {
        if (toggleAction != null)
        {
            toggleAction.Disable();
        }
    }
    void Start()
    {
        gameManager = GetComponent<GameManager>();

        var actionMap = inputActions.FindActionMap("Player2");
        toggleAction = actionMap.FindAction("Toggle");
    }


    void Update()
    {
        inputToggleMode();
    }

    private void inputToggleMode()
    {
        if (toggleAction.WasPressedThisFrame())
        {
            Debug.Log("Toggle Pressed Once");
            gameManager.toggleMode();
        }
    }

}
