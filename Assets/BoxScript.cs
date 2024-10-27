using UnityEngine;

public class BoxScript : MonoBehaviour
{

    public Rigidbody boxRigidBody;

    public float walkingSpeed;
    public float jumpingSpeed;

    public float runningSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        // boxRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     boxRigidBody.linearVelocity = Vector3.up * jumpingSpeed;
        // }

        // if (Input.GetKey(KeyCode.W))
        // {
        //     boxRigidBody.linearVelocity = transform.forward * walkingSpeed;
        // }



    }
}
