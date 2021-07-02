using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlataform : MonoBehaviour
{
    public Vector3[] points;
    public float speed;
    private bool control = true;

    int targetIndex;


    

    void Update()
    {


        if (Vector3.Distance(transform.position, points[targetIndex]) > .5f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.position, points[targetIndex], Time.deltaTime * speed);

            
        }
        else
        {
            StartCoroutine(PingPong());
            
        }
    }

    IEnumerator PingPong()
    {
        if (control)
        {
            control = false;
            yield return new WaitForSeconds(1);
            targetIndex = targetIndex == points.Length - 1 ? 0 : targetIndex + 1;
            yield return new  WaitForSeconds(1);
            control = true;
                
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = transform;
    }
    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }
}
    

 

