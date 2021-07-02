using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //This is the player speed, set to be adjustable in the unity interface
    public float speed;
    bool control = true;
    public float mouseSensitivity = 2f;
    private float yRot;
    Animator Anima;




    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Anima = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        //Here I'm getting the raw axis for control
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        //This moves the player while normalizing the speed in the diagonal space
        Vector3 moveDir = new Vector3(h, 0, v);
        yRot += Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, yRot, transform.localEulerAngles.z);
        //moveDir.Normalize();
        //moveDir = transform.TransformDirection(moveDir);
        if (Input.GetButtonDown("Fire") && control == true)
        {
            Anima.SetTrigger("ataque");
            /*StartCoroutine(TempoBala());*/
        }




        transform.Translate(h*speed * Time.deltaTime,0,v*speed*Time.deltaTime);
        // This makes the player face the direction it's moving towards
        if (h != 0 || v != 0)
        {
            Anima.SetBool("Correndo", true);

        }
        else
        {
            Anima.SetBool("Correndo", false);

        }
    }
    /*IEnumerator TempoBala()
    {
    yield return new WaitForSeconds(2);
    control = true;
    }*/
}
