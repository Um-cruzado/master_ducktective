using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 100;
    public bool immortal;
    public int playerdamage = 10;
    Animator Anima;


    // Update is called once per frame


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletPlayer"))
        {
            Dano(playerdamage);
        }
    }
   public void Dano(int playerdamage)
    {
        if(health > 0)
        {
            if (!immortal)
            {
                health = health - playerdamage;
                gameObject.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
                StartCoroutine(BossPisca());
                Anima.SetTrigger("Dano-Boss");
                Anima.SetBool("Girando", false);
            }
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Destroy(gameObject);
        }
        
    }
    IEnumerator BossPisca()
    {
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<Renderer>().material.color = new Color(255, 255, 255);

        immortal = false;

    }
}
