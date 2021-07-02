using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public int life = 100;
    public bool immortal;
    public GameObject Respawn;
    public int damage = 10;
    Animator Anima;
    public AudioClip[] audios;

    // Update is called once per frame



    public void Dano(int damage)
    {
        Anima = GetComponent<Animator>();
        if (life > 0)
        {
            if (!immortal)
            {
                life = life - damage;
                immortal = true;
                gameObject.GetComponent<Renderer>().sharedMaterial.color = new Color32(255,0,0,0);
                StartCoroutine(PlayerPisca());
            }

        }
        else
        {
            gameObject.transform.position = Respawn.transform.position;
            Anima.SetTrigger("Morte");
            life = 100;
        }

    }

    IEnumerator PlayerPisca()
    {
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<Renderer>().sharedMaterial.color = new Color32(255, 255, 255, 255);

        immortal = false;
        
    }

    void OnTriggerEnter(Collider enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            Dano(damage);
        }
    }



} 
