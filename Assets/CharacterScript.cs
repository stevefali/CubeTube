using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterScript : MonoBehaviour
{


    public float walkSpeed;

    public float jumpSpeed;
    public float lookSensitivity;
    private Vector3 playerMovement = Vector3.zero;
    private Vector3 playerRotation = Vector3.zero;
    private Vector3 cameraRotation;

    private static Vector3 startPos;


    void Start()
    {

    }


    void Update()
    {
        setStartPos();
        transform.Translate(Time.deltaTime * walkSpeed * playerMovement);

        transform.Rotate(playerRotation * lookSensitivity);
        GetComponentInChildren<Camera>().transform.Rotate(cameraRotation * lookSensitivity);

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
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

    public void Jump()
    {
        if (GetIsGrounded())
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }
    }

    private bool GetIsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 1.15f);
    }

    private void setStartPos()
    {
        if (BlockPlacingScript.GetIsReady())
        {
            transform.position = BlockPlacingScript.GetStartPos();
        }
    }
}
