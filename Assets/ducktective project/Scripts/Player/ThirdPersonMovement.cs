using UnityEngine;
using System.Collections;

public class ThirdPersonMovement : MonoBehaviour
{

    public Transform cam;
    public float playerSpeed;
    public float walkSpeed = 0.1f;
    public float mouseSensitivity = 2f;
    private bool isMoving = false;
    private float yRot;

    private Rigidbody rigidBody;

    // Use this for initialization
    void Start()
    {

        playerSpeed = walkSpeed;
        rigidBody = GetComponent<Rigidbody>();

    }

        // Update is called once per frame

        void Update()
        {

            yRot += Input.GetAxis("Mouse X") * mouseSensitivity;
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, yRot, transform.localEulerAngles.z);

            isMoving = false;

            if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                //transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * playerSpeed);
                rigidBody.velocity += transform.right * Input.GetAxisRaw("Horizontal") * playerSpeed;
                isMoving = true;
            }
            if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
            {
                //transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * playerSpeed);
                rigidBody.velocity += transform.forward * Input.GetAxisRaw("Vertical") * playerSpeed;
                isMoving = true;
            }
        }
    }
