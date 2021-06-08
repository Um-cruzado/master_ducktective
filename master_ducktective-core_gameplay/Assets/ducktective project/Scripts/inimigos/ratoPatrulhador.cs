using UnityEngine;
using System.Collections;

public class ratoPatrulhador : MonoBehaviour
{

    public float speed;
    public float startWaitTime;
    public Transform[] moveSpots;
    private float waitTime;
    private int randomSpot;

    void Start()
    {
        randomSpot = Random.Range(0, moveSpots.Length);
        waitTime = startWaitTime;
    }

  /*  void OnCollisionStay(Collision col)
    {
         if (col.compareTag("player"))
        {
            Damage;
        }
    } */

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, moveSpots[randomSpot].position) < 10f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
