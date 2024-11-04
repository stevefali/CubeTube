using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnMove(InputAction.CallbackContext context)
    {

        Vector2 moveInput = context.ReadValue<Vector2>();
        Vector3 movement = new(moveInput.x, 0, moveInput.y);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 lookInput = context.ReadValue<Vector2>();
        Vector3 horizontalRotation = new(0, lookInput.x, 0);
        Vector3 verticalRotation = new(-lookInput.y, 0, 0);
    }
}
