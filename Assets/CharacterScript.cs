using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterScript : MonoBehaviour
{


    public float walkSpeed;
    public float playerFallSpeed;

    public float lookSensitivity;
    private Vector3 playerMovement = Vector3.zero;
    private Vector3 playerRotation = Vector3.zero;



    // private Vector3 playerFallMovement = Vector3.down;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * walkSpeed * playerMovement);

        transform.Rotate(playerRotation * lookSensitivity);

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

        print(lookInput);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Vector2 jumpInput = context.ReadValue<Vector2>();
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
