using UnityEngine;
using UnityEngine.InputSystem;

public class ExplorerScript : MonoBehaviour
{

    public float moveSpeed;
    public float lookSensitivity;
    private Vector3 explorerMovement = Vector3.zero;
    private Vector3 explorerRotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * explorerMovement);
        transform.Rotate(explorerRotation * lookSensitivity);
        GetComponentInChildren<Camera>().transform.Rotate(cameraRotation * lookSensitivity);
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        explorerMovement = new Vector3(moveInput.x, 0, moveInput.y);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 lookInput = context.ReadValue<Vector2>();
        explorerRotation = new Vector3(0, lookInput.x, 0);
        cameraRotation = new Vector3(-lookInput.y, 0, 0);
    }
}
