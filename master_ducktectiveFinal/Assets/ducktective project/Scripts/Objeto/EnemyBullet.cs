using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    
    private Rigidbody rb;

    public float shotForce = 50f;
    private float lifeTimer;
    public int damage = 10;
    public float lifeDuration = 8f;
    public float speed = 8f;

    public GameObject explosionPrefab; //This is your explosion object that you spawn in

    void Start()
    {
        lifeTimer = lifeDuration;
    }
    void Update()
    {
        // faz a bala se mover
        transform.Translate(0, 0, speed * Time.deltaTime);


        //faz a bala desaparecer em 2f
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            Destroy(gameObject);
        }


    }

    //I'm switching from OnTriggerEnter to OnCollisionEnter
    private void OnTriggerEnter(Collider other)
    {
        //Your original issue was that you were checking to see if the player existed
        //so I changed it to if the projectile has collided with the player
        if (other.transform.CompareTag("Player"))
        {
            
                other.gameObject.GetComponent<ControlPlayer>().Dano(damage);
                
            
        }

        //So, You were using a GameObject as the position for explosion, so I changed it to where the projectile is when it collided with something
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        DestroyImmediate(explosionPrefab.gameObject, true);
        //This will destroy the gameobject if it collides with something
        Destroy(gameObject);
    }
}
