using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update


    public float speed = 8f;
    public Rigidbody rb;
    public float lifeDuration = 2f;
    private float lifeTimer;
    public GameObject Explosion;
    public GameObject ExplosionInitiate;
    public int playerdamage = 10;
    void Start()
    {
        lifeTimer = lifeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        // faz a bala se mover
        transform.Translate(0,0, speed * Time.deltaTime);
        

        //faz a bala desaparecer em 2f
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            Destroy(gameObject);
        }


    }

     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //explode, aqui codigo do que a bala faz com o inimigo 
            Instantiate(Explosion, ExplosionInitiate.transform.position, Quaternion.identity);
            Destroy(other.gameObject, 1);
            Destroy(gameObject);
            DestroyImmediate(Explosion.gameObject,true);
            
        }
        else if(other.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<HealthEnemy>().Dano(playerdamage);
            Instantiate(Explosion, ExplosionInitiate.transform.position, Quaternion.identity);
        }
        else
        {
            // se o objeto nao ter a tag "enemy", ele destroy a bala em colisão
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name != "Duck")
        {
            Destroy(gameObject);
            Instantiate(Explosion, ExplosionInitiate.transform.position, Quaternion.identity);
            Destroy(Explosion, 3);
        }
    }
}
