using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterScript : MonoBehaviour
{


    public float walkSpeed;
    public float playerFallSpeed;
    public float jumpSpeed;
    public float lookSensitivity;
    private Vector3 playerMovement = Vector3.zero;
    private Vector3 playerRotation = Vector3.zero;
    private Vector3 cameraRotation;
    private Vector3 playerJump = Vector3.zero;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * walkSpeed * playerMovement);

        transform.Rotate(playerRotation * lookSensitivity);
        GetComponentInChildren<Camera>().transform.Rotate(cameraRotation * lookSensitivity);

        // transform.Translate(Time.deltaTime * jumpSpeed * playerJump);

    }


    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 walkInput = context.ReadValue<Vector2>();
        playerMovement = new Vector3(walkInput.x, 0, walkInput.y);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 lookInput = context.ReadValue<Vector2>();
        playerRotation = new Vector3(0, lookInput.x, 0);
        cameraRotation = new Vector3(-lookInput.y, 0, 0);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (GetIsGrounded())
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }


    }

    private bool GetIsGrounded()
    {
        // Vector3.down
        return Physics.Raycast(transform.position, -Vector3.up, 1.15f);
    }

    private Vector3 GetFall()
    {
        if (transform.position.y > 1)
        {
            return Vector3.down;
        }
        return Vector3.zero;
    }
}
