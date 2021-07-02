using UnityEngine;
using System.Collections;

public class playerJump : MonoBehaviour
{
    private Vector3 jump;
    public float jumpForce = 2.0f;
    Animator Anima;
    public bool isGrounded;
    Rigidbody rb;
    void Start()
    {
        Anima = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGrounded = true;
            Anima.SetBool("Grounded", true);
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Anima.SetTrigger("Pulo");
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
}
