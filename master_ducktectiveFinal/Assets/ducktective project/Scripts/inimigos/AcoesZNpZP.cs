using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcoesZNpZP : MonoBehaviour
{
    public GameObject target;
    public Transform ponto1;
    public Transform ponto2;
    private Transform Chegada;
    public float speed;
    public float DistanciaAtaque;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        Chegada = ponto2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<Animator>().GetBool("Ataque") || !GetComponent<Animator>().GetBool("Morte"))
        {
            transform.position = Vector3.MoveTowards(transform.position, Chegada.position, speed * Time.deltaTime);

        }
        if (transform.position == ponto1.position)
        {
            Debug.Log("Ponto1");
            Chegada = ponto2;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (transform.position == ponto2.position)
        {
            Debug.Log("Ponto2");
            transform.rotation = Quaternion.Euler(0, 180, 0);
            Chegada = ponto1;
        }

        if (Vector3.Distance(transform.position, target.transform.position) < DistanciaAtaque)
        {
            GetComponent<Animator>().SetBool("Ataque", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Ataque", false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletPlayer"))
        {
            GetComponent<Animator>().SetBool("Morte", true);
        }
    }
}

