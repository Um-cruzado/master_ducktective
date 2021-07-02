using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroInimigo : MonoBehaviour
{
    public GameObject target;
    public Transform objective;
    public float distancetotrigger;
    public GameObject Projectile;
    public GameObject ponta;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
  
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 2)
        {
            time = 0;
            if (Vector3.Distance(transform.position, target.transform.position) < distancetotrigger)
            {
                transform.LookAt(objective.position);
                float distance = Vector3.Distance(objective.transform.position, target.transform.position);
                GameObject clone = Instantiate(Projectile, ponta.transform.position, ponta.transform.rotation);

            }
        }
    }
}
