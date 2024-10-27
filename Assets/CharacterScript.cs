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
    private Vector3 playerJump = Vector3.zero;


    CapsuleCollider collider;

    // private Vector3 playerFallMovement = Vector3.down;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * walkSpeed * playerMovement);

        transform.Rotate(playerRotation * lookSensitivity);

        transform.Translate(Time.deltaTime * jumpSpeed * playerJump);



        transform.Translate(Time.deltaTime * playerFallSpeed * GetFall());



    }
    public void OnMove(InputAction.CallbackContext context)
    {
        // Vector2 walkInput = context.ReadValue<Vector2>();
        Vector2 walkInput = context.ReadValue<Vector2>();

        playerMovement = new Vector3(walkInput.x, 0, walkInput.y);

        // print(transform.forward);
        // print("walkInput: " + walkInput);

        // Debug.Log("X: " + walkInput.x);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 lookInput = context.ReadValue<Vector2>();
        playerRotation = new Vector3(0, lookInput.x, 0);

        // print(lookInput);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // if (controller.isGrounded)
        // {
        float jumpInput = context.ReadValue<float>();
        playerJump = new Vector3(0, jumpInput, 0);
        // }

        // print(controller.);


        // print(jumpInput);
    }

    private void OnCollisionEnter(Collision collision)
    {

    }


    private Vector3 GetFall()
    {
        if (transform.position.y > 1)
        {
            return Vector3.down;
        }

        // if (!controller.isGrounded)
        // {
        //     return Vector3.down;
        // }
        return Vector3.zero;
    }
}
