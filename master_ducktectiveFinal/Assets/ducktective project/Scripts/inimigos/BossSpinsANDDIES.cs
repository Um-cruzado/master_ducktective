using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpinsANDDIES : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 RotateAmount;
    public float speed = 5f;
    public int damage = 10;
    Animator Anima;
    void Start()
    {
        Anima = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotateAmount * speed * Time.deltaTime);
        Anima.SetBool("Girando", true);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.gameObject.GetComponent<ControlPlayer>().Dano(damage);
        }
    }
}
