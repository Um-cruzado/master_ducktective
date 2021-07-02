using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atirador : MonoBehaviour
{
    public GameObject target;
    public Transform objective;
    Animator Anima;
    public float distancetotrigger;
    // Start is called before the first frame update
    void Start()
    {
        Anima = gameObject.GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < distancetotrigger)
        {
            transform.LookAt(objective.position);
            Anima.SetBool("Tiro", true);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletPlayer"))
        {
            Anima.SetBool("Morte", true);
            Destroy(gameObject, 3);
        }
    }
}
