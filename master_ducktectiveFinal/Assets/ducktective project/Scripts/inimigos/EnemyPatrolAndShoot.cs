using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolAndShoot : MonoBehaviour
{
    //Private variables
    private NavMeshAgent agent; //You can make variables like this private if you have no need to use them anywhere else except directly in this class
    Animator Anima;
    //AI States
    private enum AIStates
    {
        Idle, //Stopped
        Patroling, //Patroling around
        ChasingAttack, //Chasing the player and attacking
        IdleAttack, //Not moving and attacking
        Damaged, //If the AI is hit, pause all actions
        Dying //Ded
    }
    private AIStates currentState;

    [Header("Target")] //You can use this attribute to put the variables blow into a catagory in the inspector
    public Transform target;
    private float distFromTarget; //Constantly keeping track of player's distance from AI

    [Header("Patrol")]
    public float patrolSpeed; //How fast the AI moves when patroling
    public float minPatrolDistance; //Min patrol distance to prevent the AI from having a patrol point too close to the AI
    public float maxPatrolDistance; //Max patrol distance
    private Vector3 patrolPosition; //Position the patrolling AI moves to
    private bool needPatrolPosition = true; //Used to get a new patrol position if the AI needs a new position
    public float patrolWaitCooldown; //Cooldown after arriving at patrol position
    private bool doingPatrolCooldown; //Is the AI cooling down before finding new patrol pos

    [Header("Attack")]
    public float fireRate; //How many seconds before the AI can fire again
    private float fireWaitTime; //Used to save the time when the AI can fire next
    public Transform projectileOrigin; //Where the projectiles spawn
    public float gunLookSpeed; //How fast the projectileOrigin rotates towards player
    public GameObject projectilePrefab; //Projectile prefab

    [Header("Chase Attack")]
    public float chaseSpeed; //How fast the AI chases the player
    public float chaseAttackTriggerDistance; //Distance from player to change current state to chase attacking

    [Header("Idle Attack")]
    public float idleAttackTriggerDistance; //Distance from player to change current state to idle attacking

    [Header("Damaged")]
    public float damageCooldown; //How long until the AI can resume actions
    private AIStates stateBeforeDamage; //Saves the AI state before the AI was damaged

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>(); //You can use GetComponent to get any existing components on that gameobject
        Anima = gameObject.GetComponent<Animator>();
        currentState = AIStates.Patroling; //Setting current state to patroling so the AI can instantly start moving
    }

    private void Update()
    {
        distFromTarget = Vector3.Distance(transform.position, target.position); //Getting distance from player

        switch (currentState)
        {
            case AIStates.Idle:
                Idle();
                break;
            case AIStates.ChasingAttack:
                ChaseAttack();
                agent.speed = chaseSpeed;
                break;
            case AIStates.IdleAttack:
                IdleAttack();
                break;
            case AIStates.Damaged:
                Damaged();
                break;
            case AIStates.Dying:
                Dying();
                break;
            default:
                print("You broke this code somehow...");
                break;
        }
    }

    //Idle is used to "stop" the AI from doing any actions or movements
    private void Idle()
    {
        agent.isStopped = true;
    }

    private void Patrol()
    {
        agent.isStopped = false;

        //If the AI does not need a patrol position and is not cooling down
        if (!needPatrolPosition && !doingPatrolCooldown)
            agent.SetDestination(patrolPosition);

        //If the AI needs a patrol position
        if (needPatrolPosition)
            patrolPosition = GetNewPatrolPosition();

        //Getting distance from the patrol destination
        float distFromDest = Vector3.Distance(transform.position, patrolPosition);

        //If the distance is less than 1.25 and is not doing cooldown and does not need a new patrol position
        if (distFromDest <= 1.25f && !doingPatrolCooldown && !needPatrolPosition)
        {
            doingPatrolCooldown = true;
            currentState = AIStates.Idle;
            Invoke(nameof(PatrolCooldown), patrolWaitCooldown); //You can use Invoke to call a method and have it run in x amount of seconds. Using nameof allows me to get the name of the method without using Foo()
        }


        //If the player's distance is less than the chase attack trigger limit
        if (distFromTarget <= chaseAttackTriggerDistance)
        {
            CancelInvoke(); //Cancel the invoke of the patrol if it exists
            doingPatrolCooldown = false; //Set to false for next time using patrol
            needPatrolPosition = true; //Set to true for next time using patrol
            currentState = AIStates.ChasingAttack; //Change to chasing attack
        }

    }

    //Used for patrol cooldown, one this method is called, the AI can find a new position
    private void PatrolCooldown()
    {
        doingPatrolCooldown = false;
        needPatrolPosition = true;
        currentState = AIStates.Patroling;
    }

    private Vector3 GetNewPatrolPosition()
    {
        Vector3 pos;

        //Using a Do-While loop to make sure the randomly chosen patrol position is not too close to the player and is not behind a wall
        do
        {
            pos = transform.position + Random.onUnitSphere * maxPatrolDistance;
            pos.y = transform.position.y;
        }
        while (Vector3.Distance(transform.position, pos) < minPatrolDistance || Physics.Linecast(transform.position, pos));
        //While this statement is true, the do part of the loop will run again until the while statement is false
        //https://www.tutorialspoint.com/csharp/csharp_do_while_loop.htm

        needPatrolPosition = false;
        return pos;
    }

    private void ChaseAttack()
    {
        agent.isStopped = false;

        agent.SetDestination(target.position);

        //Calls method for shooting weapon. USed between this method and IdleAttack method
        WeaponControls();

        if (distFromTarget >= chaseAttackTriggerDistance)
            currentState = AIStates.Patroling;

        if (distFromTarget <= idleAttackTriggerDistance)
            currentState = AIStates.IdleAttack;
    }

    private void IdleAttack()
    {
        agent.isStopped = true;

        if (distFromTarget >= idleAttackTriggerDistance)
            currentState = AIStates.ChasingAttack;

        WeaponControls();
    }

    //Used to prevent code from being used twice by making it into a method
    private void WeaponControls()
    {
        //I really should not try to explain Quaternions yet. But long story short, we're getting the angle between the player and the AI and making the projectileOriign look towards that angle
        Quaternion lookRot = Quaternion.LookRotation(target.position - projectileOrigin.position);
        projectileOrigin.rotation = Quaternion.Slerp(projectileOrigin.rotation, lookRot, Time.deltaTime * gunLookSpeed);

        //If the fireWaitTime varaible is less than Time.time
        //Time.time is the time elapsed from the start of the game
        if (fireWaitTime <= Time.time)
        {
            fireWaitTime = Time.time + fireRate; //Adding the current Time.time to my fireRate variable gets the next time the player can fire thier gun
            Instantiate(projectilePrefab, projectileOrigin.position, projectileOrigin.rotation); //Spawn in the projectile
            Anima.SetTrigger("Tiro");
        }
    }

    private void Damaged()
    {
        stateBeforeDamage = currentState;
        currentState = AIStates.Idle;
        Invoke(nameof(DamagedCooldown), damageCooldown);
    }

    private void DamagedCooldown()
    {
        currentState = stateBeforeDamage;
    }

    private void Dying()
    {
        currentState = AIStates.Idle;
        Anima.SetBool("Morte", true);
        Destroy(gameObject, 3);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletPlayer"))
        {
            Dying();
        }
    }

}

