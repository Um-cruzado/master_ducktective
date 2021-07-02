using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolAndKick : MonoBehaviour
{
    // Start is called before the first frame update

    public NavMeshAgent agent;
    public GameObject target;
    public Animator anim;
    public IaState currentState;
    public Vector3 patrolposition;
    public float stoppedTime;
    public float patrolDistance;
    public float timetowait;
    public float distancetotrigger;
    public float distancetoattack;
    bool playerdead = false;
    public int damage = 10;
    Animator Anima;

    public enum IaState
    {
        Stopped,
        Attack,
        Damage,
        Dying,
        Patrol,
    }

    void Start()
    {
        patrolposition = new Vector3(transform.position.x + Random.Range(-patrolDistance, patrolDistance), transform.position.y, transform.position.z + Random.Range(-patrolDistance, patrolDistance));
        Anima = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerdead == true)
        {
            Time.timeScale = 0;
            Application.Quit();
        }

        switch (currentState)
        {
            case IaState.Stopped:
                Stopped();
                break;
            case IaState.Attack:
                Attack();
                break;
           /* case IaState.Damage:
                Damage();
                break;*/
            case IaState.Dying:
                Dying();
                break;
            case IaState.Patrol:
                Patrol();
                break;
        }
    }

     void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.gameObject.GetComponent<ControlPlayer>().Dano(damage);
        }  
    }
    void Stopped()
    {
        agent.isStopped = true;
        Anima.SetBool("Patrulha", false);
        //play animation here
        if (target && Vector3.Distance(transform.position, target.transform.position) > distancetotrigger)
        {
            currentState = IaState.Patrol;
        }

    }
    void Attack()
    {
        agent.isStopped = true;
       Anima.SetBool("Ataque", true);
        Anima.SetBool("Patrulha", false);
        //se o jogador se afastar ele volta a perseguir

    }

    /* void Damage() 
     {
         agent.isStopped = true;
         //anim.SetBool("Attack", false);
         //anim.SetTrigger("Hit");
         currentState = IaState.Stopped;

     }*/
    void Dying()
    {
        agent.isStopped = true;
        Anima.SetBool("Ataque", false);
        Anima.SetBool("Patrulha", false);
        Anima.SetBool("Morte", true);
        Destroy(gameObject, 3);
    }
    void Patrol() 
    {
        Debug.Log(Vector3.Distance(transform.position, target.transform.position));
        agent.isStopped = false;
        agent.SetDestination(patrolposition);
        Anima.SetBool("Ataque", false);
        Anima.SetBool("Patrulha", true);
        //tempo parado
        if (agent.velocity.magnitude < 0.1f)
        {
            stoppedTime += Time.deltaTime;
        }
        //se for mais q timetowait segundos
        if (stoppedTime > timetowait)
        {
            stoppedTime = 0;
            patrolposition = new Vector3(transform.position.x + Random.Range(-patrolDistance, patrolDistance), transform.position.y, transform.position.z + Random.Range(-patrolDistance, patrolDistance));
        }
        //ditancia do jogador for menor q distancetotrigger
        if (Vector3.Distance(transform.position, target.transform.position) < distancetoattack)
        {
            Debug.Log(Vector3.Distance(transform.position, target.transform.position));
            currentState = IaState.Attack;
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletPlayer"))
        {
            Dying();
        }
    }


}
