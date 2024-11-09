using UnityEngine;
using UnityEngine.InputSystem;

public class ExplorerScript : MonoBehaviour
{

    public float moveSpeed;
    public float lookSensitivity;
    public float jumpSpeed;
    private Vector3 explorerMovement = Vector3.zero;

    private Vector3 flyDownMovement = Vector3.zero;
    private Vector3 explorerRotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;

    // private bool isFlying = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // transform.Translate(Time.deltaTime * moveSpeed * explorerMovement);
        if (!GetComponent<Rigidbody>().useGravity)
        {
            MoveFlying();
            FlyDown();
        }

        transform.Rotate(explorerRotation * lookSensitivity);
        GetComponentInChildren<Camera>().transform.Rotate(cameraRotation * lookSensitivity);
    }

    void FixedUpdate()
    {
        if (GetComponent<Rigidbody>().useGravity)
        {
            MoveWithGravity();
        }
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


    public void OnJump(InputAction.CallbackContext context)
    {
        print("Type: " + context.interaction.GetType() + " Value: " + context.ReadValue<float>());
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
    }

    public void OnFlyDown(InputAction.CallbackContext context)
    {
        float flyDownInput = context.ReadValue<float>();
        flyDownMovement = new Vector3(0, -flyDownInput, 0);
    }

    private void FlyDown()
    {
        transform.Translate(Time.deltaTime * moveSpeed * flyDownMovement);
    }


    // Triggered by the 'K' key
    public void OnToggleFly()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = !rigidbody.useGravity;
        print("grav");
    }

    private void MoveFlying()
    {
        transform.Translate(Time.deltaTime * moveSpeed * explorerMovement);
    }

    private void MoveWithGravity()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        Vector3 moveThrust = rigidbody.rotation * explorerMovement * moveSpeed * 10;

        rigidbody.AddForce(moveThrust.x, 0, moveThrust.z);
    }
}
