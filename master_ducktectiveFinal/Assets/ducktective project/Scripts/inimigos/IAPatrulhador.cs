using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPatrulhador : MonoBehaviour
{
    public float speed;
    public float startWaitTime;
    public Transform[] moveSpots;
    private float waitTime;
    private int randomSpot;
    Animator Anima;

    void Start()
    {
        randomSpot = Random.Range(0, moveSpots.Length);
        waitTime = startWaitTime;
        Anima = gameObject.GetComponent<Animator>(); 
    }
   
   void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

       

           if (Vector3.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f) 
            {
            Anima.SetBool("Patrulha", true);
            if (waitTime <= 0)
                {
                    randomSpot = Random.Range(0, moveSpots.Length);
                    waitTime = startWaitTime;
                 Anima.SetBool("Patrulha",false);
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        //n sei onde ta o script de morte mas em todo caso use Anima.SetBool("Morte",true); claro se o animator funcionar.
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletPlayer"))
        {
            Anima.SetBool("Morte", true);
        }
    }
}